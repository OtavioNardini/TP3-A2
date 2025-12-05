using System.Collections.Generic;

namespace RestauranteAPP_TP3.Models
{
    public class ProfileViewModel
    {
        public Usuario User { get; set; } = null!;
        public List<Endereco> Enderecos { get; set; } = new();

        // CNPJ validation feedback (used by Profile view)
        public bool? CnpjIsValid { get; set; }
        public bool? CnpjFound { get; set; }
        public string? CnpjValidationMessage { get; set; }
    }
}