using ApiOAuthCubosCMA.Models;
using ApiOAuthCubosCMA.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiOAuthCubosCMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet("{marca}")]
        public async Task<ActionResult<List<Cubo>>> FindCubo(string marca)
        {
            return await this.repo.FindCuboAsync(marca);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarCubo(Cubo cubo)
        {
            await this.repo.InsertarCuboAsync
                (cubo.Nombre, cubo.Marca, cubo.Imagen, cubo.Precio);
            return Ok();
        }
    }
}
