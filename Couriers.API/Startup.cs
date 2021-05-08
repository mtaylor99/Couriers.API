using Couriers.BusinessService.Domain;
using Couriers.BusinessService.Interfaces;
using Couriers.Database;
using Couriers.Database.Interfaces;
using Couriers.Database.Repositories;
using Couriers.ModelService.Domain;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Couriers.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowsSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CouriersDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CouriersDatabase")));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IJobStatusRepository, JobStatusRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IScheduleItemRepository, ScheduleItemRepository>();
            services.AddTransient<IScheduleStatusRepository, ScheduleStatusRepository>();
            services.AddTransient<IJobBusinessService, JobBusinessService>();
            services.AddTransient<IDriverBusinessService, DriverBusinessService>();
            services.AddTransient<IScheduleBusinessService, ScheduleBusinessService>();
            services.AddTransient<IJobModelService, JobModelService>();
            services.AddTransient<IDriverModelService, DriverModelService>();
            services.AddTransient<IScheduleModelService, ScheduleModelService>();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                           ForwardedHeaders.XForwardedProto
            });

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
