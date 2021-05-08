using System;
using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public class DtoJobLine
    {
        [Key]
        public int JobLineId { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required] 
        public DtoAddress CollectionAddress { get; set; }

        [Required] 
        public DtoAddress DeliveryAddress { get; set; }

        public string CollectionNotes { get; set; }

        public string DeliveryNotes { get; set; }

        [Required] 
        public DateTime CollectionDate { get; set; }

        [Required] 
        public DateTime DeliveryDate { get; set; }
    }
}
