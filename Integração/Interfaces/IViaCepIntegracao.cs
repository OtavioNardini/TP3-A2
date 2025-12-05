using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Integração.Interfaces
{
    public interface IViaCepIntegracao
    {
        Task<ViaCepResponse> ObterDadosViaCep(string cep);
    }
}
