using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    [Table("Address")]
    public partial class Address
    {
        public Address()
        {
            JobLineCollectionAddresses = new HashSet<JobLine>();
            JobLineDeliveryAddresses = new HashSet<JobLine>();
        }

        [Key]
        [Column("AddressID")]
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
        [Column(TypeName = "decimal(12, 9)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(12, 9)")]
        public decimal Longitude { get; set; }

        [InverseProperty(nameof(JobLine.CollectionAddress))]
        public virtual ICollection<JobLine> JobLineCollectionAddresses { get; set; }
        [InverseProperty(nameof(JobLine.DeliveryAddress))]
        public virtual ICollection<JobLine> JobLineDeliveryAddresses { get; set; }
    }
}
