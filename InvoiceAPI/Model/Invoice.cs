using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.Model
{
    public class Invoice
    {
        [Required] // "required" by default
        [Key]
        public Guid Uuid { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]
        public double Ammount { get; set; }

        public string? SupplierFullName { get; set; }

        [Required]
        public int SupplierIco { get; set; }

        public string? PurchaserFullName { get; set; }

        [Required]
        public int PurchaserIco { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

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
