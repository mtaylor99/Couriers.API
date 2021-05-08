using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public partial class DtoScheduleItem
    {
        [Key]
        public int ScheduleItemId { get; set; }

        [Required]
        public DtoSchedule Schedule { get; set; }

        [Required]
        public DtoJobLine JobLine { get; set; }

        [Required]
        public bool IsCollection { get; set; }

        [Required] 
        public int DisplayOrder { get; set; }

        [Required] 
        public bool Completed { get; set; }
    }
}
