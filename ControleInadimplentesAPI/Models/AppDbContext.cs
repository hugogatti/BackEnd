using Microsoft.EntityFrameworkCore;

namespace ControleInadimplentesAPI.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<CobrancaModel> Cobrancas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=controledepgtodb;user=root;password=1234",
                new MySqlServerVersion(new Version(8, 0, 38)));
        }
    }
}