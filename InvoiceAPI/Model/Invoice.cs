using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.Model
{
    public class Invoice
    {
        [Key]
        public Guid Uuid { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]
        public double Ammount { get; set; }

        [StringLength (50)]
        public string? SupplierFullName { get; set; }

        [Required]
        [StringLength (9, MinimumLength = 8)]
        public string? SupplierIco { get; set; }

        [StringLength (50)]
        public string? PurchaserFullName { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 8)]
        public string? PurchaserIco { get; set; }

        [Required]
        public DateTime? IssueDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }

        public DateTime? FulfillmentDate { get; set; }

        [Required]
        public Payment PaymentType { get; set; }

    }

    public enum Payment
    {
        ByOrder,
        ByCard,
        ByCash
    }
}
