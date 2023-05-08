using ApiOAuthCubosCMA.Data;
using ApiOAuthCubosCMA.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthCubosCMA.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await
                this.context.Cubos.ToListAsync();
        }

        public async Task<List<Cubo>> FindCuboAsync(string marca)
        {
            return await
                this.context.Cubos.Where
                (x => x.Marca == marca).ToListAsync();
        }

        private int GetMaxIdCubo()
        {
            if (this.context.Cubos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Cubos.Max(z => z.IdCubo) + 1;
            }
        }

        public async Task InsertarCuboAsync
            (string nombre, string marca, string imagen, int precio)
        {
            Cubo cubo = new Cubo();
            cubo.IdCubo = GetMaxIdCubo();
            cubo.Nombre = nombre;
            cubo.Marca = marca;
            cubo.Imagen = imagen;
            cubo.Precio = precio;
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> ExisteUsuarioAsync
            (string email, string pass)
        {
            return await
                this.context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == email
                && x.Pass == pass);
        }

        private int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }

        public async Task CrearUsuarioAsync
            (string nombre, string email, string pass, string imagen)
        {
            Usuario user = new Usuario();
            user.IdUsuario = GetMaxIdUsuario();
            user.Nombre = nombre;
            user.Email = email;
            user.Pass = pass;
            user.Imagen = imagen;
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> FindUsuarioAsync(int id)
        {
            return await
                this.context.Usuarios.FirstOrDefaultAsync
                (x => x.IdUsuario == id);
        }

        public async Task<List<CompraCubo>> GetPedidosAsync(int id)
        {
            return await
                this.context.Pedidos.Where(x => x.IdUsuario == id).ToListAsync();
        }

        private int GetMaxIdPedido()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdPedido) + 1;
            }
        }

        public async Task RealizarPedidoAsync
            (int idCubo, int idUsuario, DateTime fecha)
        {
            CompraCubo ped = new CompraCubo();
            ped.IdPedido = GetMaxIdPedido();
            ped.IdCubo = idCubo;
            ped.IdUsuario = idUsuario;
            ped.FechaPedido = fecha;
            this.context.Pedidos.Add(ped);
            await this.context.SaveChangesAsync();
        }
    }
}
