using Refit;
using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Integração.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json/")]
        Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);
    }
}
