using AutoMapper;
using Couriers.Database.Enums;
using Couriers.Database.Models;
using Couriers.ModelService.Models;

namespace Couriers.ModelService.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ConfigureMappings();
        }
        private void ConfigureMappings()
        {
            CreateMap<Address, DtoAddress>().ReverseMap();

            CreateMap<Driver, DtoDriver>().ReverseMap();

            CreateMap<Job, DtoJob>()
                .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => GetJobStatus(src)))
                .ReverseMap()
                .ForMember(dest => dest.JobStatusId, opt => opt.MapFrom(src => GetJobStatusId(src)))
                .ForMember(dest => dest.JobStatus, opt => opt.Ignore());

            CreateMap<JobLine, DtoJobLine>().ReverseMap();

            CreateMap<JobStatus, DtoJobStatus>();

            CreateMap<Schedule, DtoSchedule>()
                .ForMember(dest => dest.ScheduleStatus, opt => opt.MapFrom(src => GetScheduleStatus(src)))
                .ReverseMap()
                .ForMember(dest => dest.ScheduleStatusId, opt => opt.MapFrom(src => GetScheduleStatusId(src)))
                .ForMember(dest => dest.ScheduleStatus, opt => opt.Ignore());

            CreateMap<ScheduleItem, DtoScheduleItem>().ReverseMap();

            CreateMap<ScheduleStatus, DtoScheduleStatus>().ReverseMap();
        }

        private EJobStatus GetJobStatus(Job src)
        {
            return (EJobStatus)src.JobStatusId;
        }

        private int GetJobStatusId(DtoJob src)
        {
            return (int)src.JobStatus;
        }

        private EScheduleStatus GetScheduleStatus(Schedule src)
        {
            return (EScheduleStatus)src.ScheduleStatusId;
        }

        private int GetScheduleStatusId(DtoSchedule src)
        {
            return (int)src.ScheduleStatus;
        }
    }
}
