using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Model
{
    public class InvoiceSqlDbContext : DbContext
    {
        public InvoiceSqlDbContext(DbContextOptions<InvoiceSqlDbContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YourDatabase;Trusted_Connection=True;");
        }
    }
}
