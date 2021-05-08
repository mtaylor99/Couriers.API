using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public class DtoDriver
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}
