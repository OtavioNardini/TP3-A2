using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RestauranteAPP_TP3.Customs
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
