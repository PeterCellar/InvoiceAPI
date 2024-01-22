using InvoiceAPI.DataHelper;
using InvoiceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly ILogger<InvoicesController> _logger;
        private readonly InvoiceSqlDbContext _sqlDbContext;

        public InvoicesController(ILogger<InvoicesController> logger, InvoiceSqlDbContext sqlDbContext)
        {
            _logger = logger;
            _sqlDbContext = sqlDbContext;
        }

        /// <summary>
        /// Creation of new Invoice entity and saving to the database.
        /// </summary>
        /// <param name="invoice">The entity to create.</param>
        /// <returns>Created (201) on success.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            try
            {
                DataOperations.ValidateInvoiceModel(invoice);
                _logger.LogInformation("Model {ModelType} with UUID {Uuid} has valid data.", typeof(Invoice), invoice.Uuid);

                _sqlDbContext.Invoices.Add(invoice);
                await _sqlDbContext.SaveChangesAsync();
                _logger.LogInformation("Invoice entity with UUID {Uuid} was successfully saved in database.", invoice.Uuid);

                return CreatedAtAction(nameof(CreateInvoice), new { id = invoice.Uuid }, invoice);

            }
            catch (DbUpdateException)
            {
                _logger.LogError("Duplicate key or unique constraint violation. Invoice entity with {Uuid} already exists.", invoice.Uuid);
                return Conflict($"Duplicate key or unique constraint violation. Invoice entity with {invoice.Uuid} already exists.");
            }
            catch (InvalidDataException ex)
            {
                _logger.LogError("Model {ModelType} with UUID {Uuid} has invalid data.\n{ErrorMessage}", typeof(Invoice), invoice.Uuid, ex.Message);
                return BadRequest($"Model {typeof(Invoice)} with UUID {invoice.Uuid} has invalid data.\n{ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error occured while processing Invoice entity with UUID {Uuid}.\n{ErrorMessage}", invoice.Uuid, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error occured while processing Invoice entity with UUID {invoice.Uuid}.\n{ex.Message}");
            }
        }

        /// <summary>
        /// Retrieval of existing Invoice entity identified by UUID from database.
        /// </summary>
        /// <param name="id">UUID of the entity to retrieve.</param>
        /// <returns>OK (200) on success.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _sqlDbContext.Invoices.FindAsync(id);   
            if(invoice != null)
            {
                _logger.LogInformation("Found Invoice entity with UUID {Uuid}.", invoice.Uuid);
                return Ok(invoice);
            }

            _logger.LogWarning("Invoice entity with UUID {Uuid} was not found.", id);
            return NotFound();  
        }

        /// <summary>
        /// Update of existing Invoice entity identified by UUID in database. 
        /// </summary>
        /// <param name="id">UUID of the entity to update.</param>
        /// <param name="invoice">The entity with updatable data.</param>
        /// <returns>No Content(204) on success.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Invoice>> UpdateInvoice(Guid id, UpdateInvoice? invoice)
        {
            var invoiceToUpdate = await _sqlDbContext.Invoices.FindAsync(id);
            if (invoiceToUpdate == null) 
            {
                _logger.LogWarning("Invoice entity to update with UUID {Uuid} was not found.", id);
                return  NotFound(); 
            }

            if (invoice == null || DataOperations.CountNonNullValues(invoice) == 0 ) 
            {
                _logger.LogWarning("There are no updating data prepared on model {ModelType}.", typeof(UpdateInvoice));
                return NoContent(); 
            }

            try
            {

                DataOperations.ValidateUpdateInvoiceModel(invoice);
                _logger.LogInformation("Model {ModelType} has valid data.", typeof(UpdateInvoice));

                DataOperations.UpdateInvoice(ref invoiceToUpdate!, invoice);
                _sqlDbContext.SaveChanges();
                _logger.LogInformation("Invoice entity with UUID {Uuid} was successfully updated in database.", invoiceToUpdate.Uuid);
                
                return NoContent();
            }
            catch(DbUpdateException)
            {
                _logger.LogError("Duplicate key or unique constraint violation.\nInvoice with UUID {Uuid} already exists.", invoiceToUpdate.Uuid);
                return Conflict($"Duplicate key or unique constraint violation.\nInvoice with {invoiceToUpdate.Uuid} already exists.");
            }
            catch(InvalidDataException ex)
            {
                _logger.LogError("Model {ModelType} has invalid data.\n{ErrorMessage}", typeof(UpdateInvoice), ex.Message);
                return BadRequest($"Model {typeof(UpdateInvoice)} has invalid data.\n{ex.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogError("Internal server error occured while updating Invoice entity with UUID {Uuid}.\n{ErrorMessage}", invoiceToUpdate.Uuid, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error occured while updating Invoice entity with UUID {invoiceToUpdate.Uuid}\n{ex.Message}");
            }
        }

        /// <summary>
        /// Deletion of the Invoice entity identified by UUID from database.
        /// </summary>
        /// <param name="id">UUID of the entity to delete.</param>
        /// <returns>No Content(204) on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoiceToDelete = await _sqlDbContext.Invoices.FindAsync(id);
            if(invoiceToDelete == null) 
            {
                _logger.LogWarning("Invoice entity to delete with UUID {Uuid} was not found.", id);
                return NotFound(); 
            }

            try
            {
                _sqlDbContext.Invoices.Remove(invoiceToDelete);
                _sqlDbContext.SaveChanges();
                _logger.LogInformation("Invoice entity with UUID {Uuid} was successfully deleted from database.", id);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError("Internal server error occured while deleting Invoice entity with UUID {id}.\n{ErrorMessage}.", id, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error occured while deleting Invoice entity with UUID {id}.\n{ex.Message}.");
            }
        }
    }
}
