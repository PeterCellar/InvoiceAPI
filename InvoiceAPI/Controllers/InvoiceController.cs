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
        private readonly InvoiceDbContext _dbContext;

        public InvoiceController(ILogger<InvoiceController> logger, InvoiceDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        

        [HttpPost("Create")]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateInvoice), new { id = invoice.Uuid }, invoice);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _dbContext.Invoices.FindAsync(id);
        
            return invoice == null ? NotFound() : invoice;  
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<Invoice>> UpdateInvoice(Guid Id, Invoice invoice)
        {
            var invoiceToUpdate = await _dbContext.Invoices.FirstOrDefaultAsync(e => e.Uuid == Id);
            
            // Upravit -> Vytvorit aj svoje korektne spravy na vratenie uzivatelovi
            if(invoiceToUpdate == null) { return NotFound(); }

            // Zavolat metodu z DataHelperu na updatovanie dat

            return invoiceToUpdate;
        }

        [HttpDelete("Delete/{id}")]
        public void DeleteInvoice(Guid id)
        {

        }
    }
}
