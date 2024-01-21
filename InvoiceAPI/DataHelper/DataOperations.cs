using InvoiceAPI.Model;

namespace InvoiceAPI.DataHelper
{
    /// <summary>
    /// Class implementing operations on Invoice data.
    /// </summary>
    public static class DataOperations
    {
        /// <summary>
        /// Update of the Invoice entity data.
        /// </summary>
        /// <param name="invoice">Referenced Invoice entity from the database.</param>
        /// <param name="updateInvoice">Invoice entity with updatable data.</param>
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

        /// <summary>
        /// Validation of Invoice model values.
        /// </summary>
        /// <param name="invoice">Invoice model to be validated.</param>
        /// <exception cref="InvalidDataException">Thrown if model values are invalid.</exception>
        public static void ValidateInvoiceModel(Invoice invoice)
        {
            if(!ValidateIco(invoice.SupplierIco!)) { throw new InvalidDataException(); }

            if(!ValidateIco(invoice.PurchaserIco!)) { throw new InvalidDataException(); }
        }

        /// <summary>
        /// Validation of update Invoice model values.
        /// </summary>
        /// <param name="invoice">Update Invoice model to be validated.</param>
        /// <exception cref="InvalidDataException">Thrown if model values are invalid.</exception>
        public static void ValidateUpdateInvoiceModel(UpdateInvoice invoice)
        {
            if (invoice.SupplierIco != null && !ValidateIco(invoice.SupplierIco)) { throw new InvalidDataException(); }

            if (invoice.PurchaserIco != null && !ValidateIco(invoice.PurchaserIco!)) { throw new InvalidDataException(); }
        }

        /// <summary>
        /// Validation if ICO data.
        /// </summary>
        /// <param name="ico">ICO property to be validated.</param>
        /// <returns>True if ICO data are valid.</returns>
        public static bool ValidateIco(string ico)
        {
            return int.TryParse(ico, out var i);
        } 
    }
}
