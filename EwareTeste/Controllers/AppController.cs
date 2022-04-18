using EwareTeste.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwareTeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private EwareTesteContext context;
        public AppController() {
            context = new();
        }

        [HttpGet("caminhoes", Name = nameof(GetCaminhoes))]
        public IEnumerable<Caminhao> GetCaminhoes()
        {
            using var context = new EwareTesteContext();
            return context.Caminhoes.Include(x => x.Modelo).ToList();
        }        

        [HttpPost("caminhoes/salvar", Name = nameof(PostCaminhoes))]
        public async Task<ActionResult<Caminhao>> PostCaminhoes(Caminhao caminhao)
        {
            if (caminhao.Id > 0)
            {

            }
            context.Caminhoes.Add(caminhao);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCaminhoes), new { id = caminhao.Id }, caminhao);
        }        
    }
}