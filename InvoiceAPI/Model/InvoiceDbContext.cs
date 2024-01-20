using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Model
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }  
    }
}
