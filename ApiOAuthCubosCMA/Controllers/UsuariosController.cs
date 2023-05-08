using ApiOAuthCubosCMA.Models;
using ApiOAuthCubosCMA.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiOAuthCubosCMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryCubos repo;

        public UsuariosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        //[Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Usuario>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "ClaimUser");
            string json = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
            return usuario;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CrearUsuario(Usuario user)
        {
            await this.repo.CrearUsuarioAsync
                (user.Nombre, user.Email, user.Pass, user.Imagen);
            return Ok();
        }
    }
}
