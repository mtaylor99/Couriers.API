using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Couriers.Database.Models
{
    [Table("JobLine")]
    public partial class JobLine
    {
        public JobLine()
        {
            ScheduleItems = new HashSet<ScheduleItem>();
        }

        [Key]
        public int JobLineId { get; set; }
        public int JobId { get; set; }
        public int CollectionAddressId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string CollectionNotes { get; set; }
        public string DeliveryNotes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CollectionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DeliveryDate { get; set; }

        [ForeignKey(nameof(CollectionAddressId))]
        [InverseProperty(nameof(Address.JobLineCollectionAddresses))]
        public virtual Address CollectionAddress { get; set; }
        [ForeignKey(nameof(DeliveryAddressId))]
        [InverseProperty(nameof(Address.JobLineDeliveryAddresses))]
        public virtual Address DeliveryAddress { get; set; }
        [ForeignKey(nameof(JobId))]
        [InverseProperty("JobLines")]
        public virtual Job Job { get; set; }
        [InverseProperty(nameof(ScheduleItem.JobLine))]
        public virtual ICollection<ScheduleItem> ScheduleItems { get; set; }
    }
}
