using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public class DtoScheduleStatus
    {
        [Key]
        public int ScheduleStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
