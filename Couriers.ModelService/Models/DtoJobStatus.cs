using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public  class DtoJobStatus
    {
        [Key]
        public int JobStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
