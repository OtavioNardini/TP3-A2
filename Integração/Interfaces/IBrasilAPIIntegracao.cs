using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Integração.Interfaces
{
    public interface IBrasilAPIIntegracao
    {
        Task<BrasilAPIResponse> ObterDadosViaCNPJ(string cnpj);
    }
}
