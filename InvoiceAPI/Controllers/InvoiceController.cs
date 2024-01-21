using InvoiceAPI.DataHelper;
using InvoiceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            try
            {
                DataOperations.ValidateInvoiceModel(invoice);

                _sqlDbContext.Invoices.Add(invoice);
                await _sqlDbContext.SaveChangesAsync();

            }
            catch(DbUpdateException)
            {
                return Conflict($"Duplicate key or unique constraint violation.\nInvoice with {invoice.Uuid} already exists.");
            }
            catch(InvalidDataException ex)
            {
                return BadRequest("Invalid invoice data.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\n" + ex.Message);
            }

            return CreatedAtAction(nameof(CreateInvoice), new { id = invoice.Uuid}, invoice);
        }

        [HttpGet("Read/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _sqlDbContext.Invoices.FindAsync(id);   
            return invoice == null ? NotFound() : Ok(invoice);  
        }


        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Invoice>> UpdateInvoice(Guid id, UpdateInvoice? invoice)
        {
            var invoiceToUpdate = await _sqlDbContext.Invoices.FindAsync(id);
            if (invoiceToUpdate == null) { return  NotFound(); }

            if (invoice == null) { return NoContent(); }

            try
            {
                DataOperations.ValidateUpdateInvoiceModel(invoice);
                DataOperations.UpdateInvoice(ref invoiceToUpdate!, invoice);
                _sqlDbContext.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return Conflict($"Duplicate key or unique constraint violation.\nInvoice with {invoiceToUpdate.Uuid} already exists.");
            }
            catch(InvalidDataException ex)
            {
                return BadRequest("Invalid invoice data.\n" + ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\n" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoiceToDelete = await _sqlDbContext.Invoices.FindAsync(id);
            if(invoiceToDelete == null) { return NotFound(); }

            try
            {
                _sqlDbContext.Invoices.Remove(invoiceToDelete);
                _sqlDbContext.SaveChanges();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error\n" + ex.Message);
            }
        }
    }
}
