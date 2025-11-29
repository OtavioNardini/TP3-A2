using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class Usuario : IdentityUser<int>
    {
        // IdentityUser already has Id, UserName (Nome), Email, etc.
        // We map "Nome" to UserName or just add a separate Name property if needed.
        // UML says: nome: String. Identity has UserName. Let's add Nome for full name if needed, or map it.
        // Usually IdentityUser uses UserName for login. Let's add a explicit Nome property for display name as per UML often implies.
        
        public string Nome { get; set; }

        public int? OrgId { get; set; }
        [ForeignKey("OrgId")]
        public Organizacao Organizacao { get; set; }

        public int? FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
