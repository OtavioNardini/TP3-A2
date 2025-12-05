using Refit;
using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Integração.Refit
{
    public interface IBrasilAPIIntegracaoRefit
    {
        [Get("/cnpj/v1/{cnpj}")]
        Task<ApiResponse<BrasilAPIResponse>> ObterDadosViaCNPJ(string cnpj);
    }
}
