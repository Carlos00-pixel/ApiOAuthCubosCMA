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
    public class PedidosController : ControllerBase
    {
        private RepositoryCubos repo;

        public PedidosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        //[Route("[action]")]
        [Authorize]
        public async Task<ActionResult<List<CompraCubo>>> GetPedidos()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "ClaimUser");
            string json = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
            return await this.repo.GetPedidosAsync(usuario.IdUsuario);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult> RealizarPedido(CompraCubo ped)
        {
            await this.repo.RealizarPedidoAsync
                (ped.IdCubo, ped.IdUsuario, ped.FechaPedido);
            return Ok();
        }
    }
}
