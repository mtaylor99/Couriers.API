using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    [Table("JobStatus")]
    public partial class JobStatus
    {
        public JobStatus()
        {
            Jobs = new HashSet<Job>();
        }

        [Key]
        public int JobStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty(nameof(Job.JobStatus))]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
