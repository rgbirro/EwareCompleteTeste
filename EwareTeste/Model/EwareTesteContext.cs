using Microsoft.EntityFrameworkCore;

namespace EwareTeste.Model
{
    public class EwareTesteContext : DbContext
    {
        public DbSet<Caminhao> Caminhoes { get; set; }
        public DbSet<ModeloCaminhao> ModelosCaminhoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ewareTeste; Trusted_connection = true");
        }
    }
}
