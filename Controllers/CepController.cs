using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPP_TP3.Integração;
using RestauranteAPP_TP3.Integração.Interfaces;
using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly IViaCepIntegracao _viaCepIntegracao;
        public CepController(IViaCepIntegracao viaCepIntegracao)
        {
            _viaCepIntegracao = viaCepIntegracao;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<ViaCepResponse>> ListarDadosEdereço(string cep)
        {
           var responseData = await _viaCepIntegracao.ObterDadosViaCep(cep);

             if(responseData == null)
              {
                 return BadRequest("CEP não encontrado.");
            }
             return Ok(responseData);
        }
    }
}
