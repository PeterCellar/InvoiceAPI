using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.Model
{
    public class UpdateInvoice
    {
        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public double? Ammount { get; set; }

        public string? SupplierFullName { get; set; }

        [StringLength(9, MinimumLength = 8)]
        public string? SupplierIco { get; set; }

        public string? PurchaserFullName { get; set; }

        [StringLength(9, MinimumLength = 8)]
        public string? PurchaserIco { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? FulfillmentDate { get; set; }

        public Payment? PaymentType { get; set; }
    }
}
