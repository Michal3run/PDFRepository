using Microsoft.EntityFrameworkCore;
using PDFRepositoryProject.Models;

namespace PDFRepositoryProject.Data
{
    public class PDFContext : DbContext
    {

        public PDFContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DbPDFDocument> Documents { get; set; }
    }
}
