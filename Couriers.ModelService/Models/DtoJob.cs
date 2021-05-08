using Couriers.Database.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

 namespace Couriers.ModelService.Models
{
    public class DtoJob
    {
        public DtoJob()
        {
            JobLines = new List<DtoJobLine>();
        }

        [Key]
        public int JobId { get; set; }

        [Required]
        [StringLength(50)]
        public string Goods { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderedBy { get; set; }

        [Required] 
        public DateTime CreatedDate { get; set; }

        [Required]
        public EJobStatus JobStatus { get; set; }

        public List<DtoJobLine> JobLines { get; set; }
    }
}
