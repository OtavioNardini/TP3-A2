using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPP_TP3.Integração.Interfaces;
using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CNPJController : ControllerBase
    {
        private readonly IBrasilAPIIntegracao _brasilAPIIntegracao;
        public CNPJController(IBrasilAPIIntegracao brasilAPIIntegracao)
        {
            _brasilAPIIntegracao = brasilAPIIntegracao;
        }

        [HttpGet("{cnpj}")]
        public async Task<ActionResult<BrasilAPIResponse>> ListarDadosEdereço(string cnpj)
        {
            var responseData = await _brasilAPIIntegracao.ObterDadosViaCNPJ(cnpj);

            if (responseData == null)
            {
                return BadRequest("CNPJ não encontrado.");
            }
            return Ok(responseData);
        }
    }
}
