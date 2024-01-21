using InvoiceAPI.Model;

namespace InvoiceAPI.DataHelper
{
    public static class DataOperations
    {
        public static void UpdateInvoice(ref Invoice invoice, UpdateInvoice updateInvoice)
        {
            if (updateInvoice.CreationDate != null) { invoice.CreationDate = updateInvoice.CreationDate; }

            if (updateInvoice.UpdateDate != null) { invoice.UpdateDate = updateInvoice.UpdateDate; }

            if (updateInvoice.Ammount != null) { invoice.Ammount = (double)updateInvoice.Ammount; }

            if (updateInvoice.SupplierFullName != null) { invoice.SupplierFullName = updateInvoice.SupplierFullName; }

            if (updateInvoice.SupplierIco != null) { invoice.SupplierIco = updateInvoice.SupplierIco; }

            if (updateInvoice.PurchaserFullName != null) { invoice.PurchaserFullName = updateInvoice.PurchaserFullName; }

            if (updateInvoice.PurchaserIco != null) { invoice.PurchaserIco = updateInvoice.PurchaserIco; }

            if (updateInvoice.IssueDate != null) { invoice.IssueDate = updateInvoice.IssueDate; }

            if (updateInvoice.DueDate != null) { invoice.DueDate = updateInvoice.DueDate; }

            if (updateInvoice.FulfillmentDate != null) { invoice.FulfillmentDate = updateInvoice.FulfillmentDate; }

            if (updateInvoice.PaymentType != null) { invoice.PaymentType = (Payment)updateInvoice.PaymentType; }
        }

        public static void ValidateInvoiceModel(Invoice invoice)
        {
            if(!ValidateIco(invoice.SupplierIco!)) { throw new InvalidDataException(); }

            if(!ValidateIco(invoice.PurchaserIco!)) { throw new InvalidDataException(); }
        }

        public static void ValidateUpdateInvoiceModel(UpdateInvoice invoice)
        {
            if (invoice.SupplierIco != null && !ValidateIco(invoice.SupplierIco)) { throw new InvalidDataException(); }

            if (invoice.PurchaserIco != null && !ValidateIco(invoice.PurchaserIco!)) { throw new InvalidDataException(); }
        }

        public static bool ValidateIco(string ico)
        {
            return int.TryParse(ico, out var i);
        } 
    }
}
