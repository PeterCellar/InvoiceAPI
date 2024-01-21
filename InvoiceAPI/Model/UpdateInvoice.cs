using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.Model
{
    public class UpdateInvoice
    {
        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public double? Ammount { get; set; }

        public string? SupplierFullName { get; set; }

        public int? SupplierIco { get; set; }

        public string? PurchaserFullName { get; set; }

        public int? PurchaserIco { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? FulfillmentDate { get; set; }

        public Payment? PaymentType { get; set; }
    }
}
