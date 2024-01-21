using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.Model
{
    /// <summary>
    /// Model representing updatable Invoice entity.
    /// </summary>
    public class UpdateInvoice
    {
        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public double? Ammount { get; set; }

        [StringLength(50)]
        public string? SupplierFullName { get; set; }

        [StringLength(9, MinimumLength = 8)]
        public string? SupplierIco { get; set; }

        [StringLength(50)]
        public string? PurchaserFullName { get; set; }

        [StringLength(9, MinimumLength = 8)]
        public string? PurchaserIco { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? FulfillmentDate { get; set; }

        public Payment? PaymentType { get; set; }
    }
}
