using Microsoft.AspNetCore.Mvc;
using player.Model;
using System.Reflection;

namespace player.Controllers
{
    public class PlayerAPIController : ControllerBase
    {

        public static List<Jogador> jogadores = new List<Jogador>() {
            { new Jogador { id = "1", Vida = 3, QuantidadeItens = 0, PosicaoX = 0f, PosicaoY = 0f, PosicaoZ = 0f } },
            { new Jogador { id = "2", Vida = 2, QuantidadeItens = 1, PosicaoX = 3.5f, PosicaoY = 1.2f, PosicaoZ = 2f } }
        };

        [HttpGet]
        [Route("api/player")]
        public IActionResult GetJogadores()
        {
            return Ok(jogadores);
        }

        [HttpGet]
        [Route("api/player/{id}")]
        public IActionResult GetPlayerByID(string id)
        {
            var player = jogadores.FirstOrDefault(a => a.id == id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        [Route("api/player")]
        public IActionResult AddPlayer([FromBody] Jogador novoJogador)
        {
            jogadores.Add(novoJogador);
            return Ok(novoJogador);
        }

        [HttpPut]
        [Route("api/player/{id}")]
        public IActionResult UpdatePlayer(string id, [FromBody] Jogador jogadorAtualizado)
        {
            var player = jogadores.FirstOrDefault(a => a.id == id);
            if (player == null)
            {
                return NotFound();
            }
            player.Vida = jogadorAtualizado.Vida;
            player.QuantidadeItens = jogadorAtualizado.QuantidadeItens;
            player.PosicaoX = jogadorAtualizado.PosicaoX;
            player.PosicaoY = jogadorAtualizado.PosicaoY;
            player.PosicaoZ = jogadorAtualizado.PosicaoZ;
            return Ok(player);
        }

        [HttpDelete]
        [Route("api/player/{Id}")]
        public IActionResult DeletePlayer(string id)
        {
            var player = jogadores.FirstOrDefault(a => a.id == id);
            if (player == null)
            {
                return NotFound();
            }
            jogadores.Remove(player);
            return Ok();
        }
    }
}
