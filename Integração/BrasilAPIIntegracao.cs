using System.Text.RegularExpressions;
using RestauranteAPP_TP3.Integração.Interfaces;
using RestauranteAPP_TP3.Integração.Refit;
using RestauranteAPP_TP3.Integração.Response;

namespace RestauranteAPP_TP3.Integração
{
    public class BrasilAPIIntegracao : IBrasilAPIIntegracao
    {
        private readonly IBrasilAPIIntegracaoRefit _brasilAPIIntegracaoRefit;
        public BrasilAPIIntegracao(IBrasilAPIIntegracaoRefit brasilAPIIntegracaoRefit)
        {
            _brasilAPIIntegracaoRefit = brasilAPIIntegracaoRefit;
        }

        // Keeps the interface implementation
        public async Task<BrasilAPIResponse> ObterDadosViaCNPJ(string cnpj)
        {
            var clean = OnlyDigits(cnpj);
            var responseData = await _brasilAPIIntegracaoRefit.ObterDadosViaCNPJ(clean);

            if (responseData != null && responseData.IsSuccessStatusCode)
            {
                return responseData.Content;
            }
            return null;
        }

        // New helper to validate format/checksum and return useful info before saving
        public async Task<(bool IsValidFormat, bool Found, string Message, BrasilAPIResponse? Data)> ValidarEObterDadosViaCNPJ(string cnpj)
        {
            var clean = OnlyDigits(cnpj);
            if (clean.Length != 14)
            {
                return (false, false, "CNPJ must contain 14 digits.", null);
            }

            if (IsAllSameDigits(clean) || !ValidateCnpjChecksum(clean))
            {
                return (false, false, "CNPJ checksum is invalid.", null);
            }

            var data = await ObterDadosViaCNPJ(clean);
            if (data == null || string.IsNullOrWhiteSpace(data.Cnpj))
            {
                return (true, false, "CNPJ is well-formed but not found in BrasilAPI.", null);
            }

            var situacao = data.DescricaoSituacaoCadastral ?? data.SituacaoCadastral?.ToString() ?? "Unknown";
            var razao = data.RazaoSocial ?? data.NomeFantasia ?? "Unknown";
            var location = $"{data.Municipio ?? "Unknown"}, {data.Uf ?? "Unknown"}";
            var msg = $"Found: {razao} — {situacao} — {location}";

            return (true, true, msg, data);
        }

        private static string OnlyDigits(string input) =>
            Regex.Replace(input ?? string.Empty, @"\D", string.Empty);

        private static bool IsAllSameDigits(string digits) =>
            digits.Distinct().Count() == 1;

        // Standard CNPJ checksum validation
        private static bool ValidateCnpjChecksum(string cnpj)
        {
            if (cnpj.Length != 14) return false;

            int[] weight1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weight2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string number = cnpj.Substring(0, 12);
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (number[i] - '0') * weight1[i];

            int remainder = sum % 11;
            int firstCheck = remainder < 2 ? 0 : 11 - remainder;
            number += firstCheck;

            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += (number[i] - '0') * weight2[i];

            remainder = sum % 11;
            int secondCheck = remainder < 2 ? 0 : 11 - remainder;

            return cnpj[12] - '0' == firstCheck && cnpj[13] - '0' == secondCheck;
        }
    }
}
