using Microsoft.AspNetCore.Identity.UI.Services;
namespace Ecommerce.Services;


public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return null;
    }
}