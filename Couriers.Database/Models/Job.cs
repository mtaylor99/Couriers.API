using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    [Table("Job")]
    public partial class Job
    {
        public Job()
        {
            JobLines = new HashSet<JobLine>();
        }

        [Key]
        public int JobId { get; set; }
        [Required]
        public string Goods { get; set; }
        [Required]
        [StringLength(50)]
        public string Customer { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int JobStatusId { get; set; }

        [ForeignKey(nameof(JobStatusId))]
        [InverseProperty("Jobs")]
        public virtual JobStatus JobStatus { get; set; }
        [InverseProperty(nameof(JobLine.Job))]
        public virtual ICollection<JobLine> JobLines { get; set; }
    }
}
