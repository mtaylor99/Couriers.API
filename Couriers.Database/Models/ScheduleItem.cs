using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    public partial class ScheduleItem
    {
        [Key]
        public int ScheduleItemId { get; set; }
        public int ScheduleId { get; set; }
        public int JobLineId { get; set; }
        public bool IsCollection { get; set; }
        public int DisplayOrder { get; set; }
        public bool Completed { get; set; }

        [ForeignKey(nameof(JobLineId))]
        [InverseProperty("ScheduleItems")]
        public virtual JobLine JobLine { get; set; }
        [ForeignKey(nameof(ScheduleId))]
        [InverseProperty("ScheduleItems")]
        public virtual Schedule Schedule { get; set; }
    }
}
