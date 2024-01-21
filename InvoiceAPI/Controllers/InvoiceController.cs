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
        private readonly InvoiceDbContext _dbContext;
        private readonly InvoiceSqlDbContext _sqlDbContext;

        public InvoiceController(ILogger<InvoiceController> logger, InvoiceDbContext dbContext, InvoiceSqlDbContext sqlDbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _sqlDbContext = sqlDbContext;
        }

        

        [HttpPost("Create")]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            //_dbContext.Invoices.Add(invoice);
            //await _dbContext.SaveChangesAsync();

            _sqlDbContext.Invoices.Add(invoice);
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
        public async Task<ActionResult<Invoice>> UpdateInvoice(Guid id, UpdateInvoice? invoice)
        {
            var invoiceToUpdate = await _dbContext.Invoices.FirstOrDefaultAsync(e => e.Uuid == id);
            
            // Upravit -> Vytvorit aj svoje korektne spravy na vratenie uzivatelovi

            DataFiller.UpdateInvoice(ref invoiceToUpdate!, invoice);
            _dbContext.SaveChanges();

            return invoiceToUpdate;
        }

        [HttpDelete("Delete/{id}")]
        public async void DeleteInvoice(Guid id)
        {
            var invoiceToDelete = await _dbContext.Invoices.FirstOrDefaultAsync(e => e.Uuid == id);
            
            if(invoiceToDelete != null) 
            {
                _dbContext.Invoices.Remove(invoiceToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
