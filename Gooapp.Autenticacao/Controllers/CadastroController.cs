using Microsoft.AspNetCore.Mvc;
using Gooapp.Autenticacao.Services;
using FluentResults;
using Gooapp.Autenticacao.Data.Dtos;

namespace Gooapp.Autenticacao.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDTO createDto)
        {
            Result resultado = await _cadastroService.CadastraUsuario(createDto);
            if (resultado.IsFailed) return StatusCode(500, resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes);
        }

        [HttpGet("/ativa")]
        public async Task<IActionResult> AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result resultado = await _cadastroService.AtivaContaUsuario(request);
            if (resultado.IsFailed) return StatusCode(500, resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes);
        }
    }
}
