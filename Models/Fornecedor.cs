using System.Collections.Generic;

namespace PlataformaB2B_A2_TP3.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string StatusKYC { get; set; }
        
        // Storing list of strings as JSON or simple relation? UML says List<String>. 
        // For simplicity in EF Core without extra tables, we might skip or use a value converter, 
        // but let's stick to a simple approach or ignore if not critical, 
        // OR create a separate table if strictly following 3NF. 
        // Given "List<String>", let's assume a simple collection or comma separated string for now if not specified.
        // However, to be clean, let's just leave it as a property that might be handled specially or ignored for now if complex.
        // actually, let's make it a string for now (comma separated) to keep it simple as requested "simples e legivel".
        public string Certificacoes { get; set; } 

        // Relationships
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<DocumentoFornecedor> Documentos { get; set; }
        public ICollection<CertificadoCompliance> CertificadosCompliance { get; set; }
        public ICollection<ProdutoServico> ProdutosServicos { get; set; }
        public ICollection<PropostaFornecedor> Propostas { get; set; }
    }
}
