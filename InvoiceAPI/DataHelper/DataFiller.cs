using InvoiceAPI.Model;

namespace InvoiceAPI.DataHelper
{
    public static class DataFiller
    {
        public static void UpdateInvoice(ref Invoice invoice, UpdateInvoice? updateInvoice)
        {
            if(updateInvoice != null)
            {
                if (updateInvoice.CreationDate != null) { invoice.CreationDate = (DateTime)updateInvoice.CreationDate; }
                
                if (updateInvoice.UpdateDate != null) { invoice.UpdateDate = updateInvoice.UpdateDate; }
                
                if (updateInvoice.Ammount != null) { invoice.Ammount = (double)updateInvoice.Ammount; }
                
                if (updateInvoice.SupplierFullName != null) { invoice.SupplierFullName = updateInvoice.SupplierFullName; }
                
                if (updateInvoice.SupplierIco != null) { invoice.SupplierIco = updateInvoice.SupplierIco; }
                
                if (updateInvoice.PurchaserFullName != null) { invoice.PurchaserFullName = updateInvoice.PurchaserFullName; }
               
                if (updateInvoice.PurchaserIco != null) { invoice.PurchaserIco = updateInvoice.PurchaserIco; }
                
                if (updateInvoice.IssueDate != null) { invoice.IssueDate = (DateTime)updateInvoice.IssueDate; }
               
                if (updateInvoice.DueDate != null) { invoice.DueDate = (DateTime)updateInvoice.DueDate; }
               
                if (updateInvoice.FulfillmentDate != null) { invoice.FulfillmentDate = updateInvoice.FulfillmentDate; }
              
                if (updateInvoice.PaymentType != null) { invoice.PaymentType = (Payment)updateInvoice.PaymentType; }
            }

        }
    }
}
