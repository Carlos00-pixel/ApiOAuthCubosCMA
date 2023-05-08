using ApiOAuthCubosCMA.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthCubosCMA.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options)
            : base(options)
        {
        }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CompraCubo> Pedidos { get; set; }
    }
}
