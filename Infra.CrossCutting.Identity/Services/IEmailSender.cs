using System.Threading.Tasks;

namespace Autoglass.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {        
        Task SendEmailNewSubscriptionWithCreditCardAsync(string username, string email);
        Task SendEmailNewSubscriptionWithBoletoAsync(string username, string email, string urlBoleto);
        Task SendEmailSubscriptionInvoiceApprovedAsync(string username, string email);
        Task SendEmailRecoveryPasswordAsync(string email, int userId, string code);
        Task SendEmailAgentAsync(string username, string email);
        Task SendEmailAgentDocsAnalisysAsync(string username, string email);
        Task SendEmailAgentActivedAsync(string username, string email);
        Task SendEmailAgentRejectedAsync(string username, string email); 
        Task SendEmailCustomerActivateddAsync(string name, string email, string code);
        Task SendEmailPartnerCreatedAsync(string name, string email, string code);
    }
}
