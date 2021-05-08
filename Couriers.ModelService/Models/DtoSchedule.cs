using Couriers.Database.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public class DtoSchedule
    {
        public DtoSchedule()
        {
            ScheduleItems = new List<DtoScheduleItem>();
        }

        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public DtoDriver Driver { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required] 
        public EScheduleStatus ScheduleStatus { get; set; }

        public List<DtoScheduleItem> ScheduleItems { get; set; }
    }
}
