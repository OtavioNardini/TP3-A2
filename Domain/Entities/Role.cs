using Microsoft.AspNetCore.Identity;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        // IdentityRole has Id, Name, NormalizedName.
        // UML matches this exactly.
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
    }
}
