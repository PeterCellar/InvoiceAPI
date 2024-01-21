using InvoiceAPI.DataHelper;
using InvoiceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly InvoiceSqlDbContext _sqlDbContext;

        public InvoiceController(ILogger<InvoiceController> logger, InvoiceSqlDbContext sqlDbContext)
        {
            _logger = logger;
            _sqlDbContext = sqlDbContext;
        }

        

        [HttpPost("Create")]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            var existingInvoice = _sqlDbContext.Invoices.FindAsync(invoice.Uuid);
            if(existingInvoice.Result != null) { throw new DbUpdateException(); } 

            DataOperations.ValidateData(invoice);

            _sqlDbContext.Invoices.Add(invoice);
            await _sqlDbContext.SaveChangesAsync();    

            return CreatedAtAction(nameof(CreateInvoice), new { id = invoice.Uuid }, invoice);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _sqlDbContext.Invoices.FindAsync(id);   

            return invoice == null ? NotFound() : invoice;  
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<Invoice>> UpdateInvoice(Guid id, UpdateInvoice? invoice)
        {
            var invoiceToUpdate = await _sqlDbContext.Invoices.FindAsync(id);
            

            DataOperations.UpdateInvoice(ref invoiceToUpdate!, invoice);
            _sqlDbContext.SaveChanges();

            return invoiceToUpdate;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoiceToDelete = await _sqlDbContext.Invoices.FindAsync(id);
            
            if(invoiceToDelete != null) 
            {
                _sqlDbContext.Invoices.Remove(invoiceToDelete);
                _sqlDbContext.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
    }
}
