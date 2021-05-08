using System.ComponentModel.DataAnnotations;

namespace Couriers.ModelService.Models
{
    public class DtoAddress
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(128)]
        public string Street1 { get; set; }

        [Required]
        [StringLength(128)]
        public string Street2 { get; set; }

        [Required]
        [StringLength(128)]
        public string Street3 { get; set; }

        [Required]
        [StringLength(128)]
        public string Town { get; set; }

        [Required]
        [StringLength(128)]
        public string County { get; set; }

        [Required]
        [StringLength(128)]
        public string Postcode { get; set; }

        [Required]
        [StringLength(128)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(128)]
        public string Fax { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required] 
        public decimal Longitude { get; set; }
    }
}
