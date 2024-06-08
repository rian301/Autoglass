using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Infra.CrossCutting.Identity.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _configuration;
        private readonly string EmailFrom = "";
        private readonly string NameFrom = "";

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            NameFrom = _configuration["Integration:SendGrid:NameFrom"];
            EmailFrom = _configuration["Integration:SendGrid:EmailFrom"];
        }

        public async Task SendEmailNewSubscriptionWithCreditCardAsync(string username, string email)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateNewSubscriptionWithCreditCard"];
            var data = new
            {
                username,
                email
            };
            await Send(templateId, email, data);
        }

        public async Task SendEmailNewSubscriptionWithBoletoAsync(string username, string email, string urlBoleto)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateNewSubscriptionWithBoleto"];
            var data = new
            {
                username,
                email,
                urlBoleto
            };

            await Send(templateId, email, data);
        }

        public async Task SendEmailSubscriptionInvoiceApprovedAsync(string username, string email)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateSubscriptionInvoiceApproved"];
            var data = new
            {
                username,
                email
            };

            await Send(templateId, email, data);
        }


        public async Task SendEmailRecoveryPasswordAsync(string email, int userId, string code)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateRecoveryPassword"];

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            var link = $"{_configuration["ClientUrl:FrontEnd:Url"]}/{_configuration["ClientUrl:FrontEnd:Path:RecoveryPassword"]}?code={codeEncoded}&email={email}";
            var data = new
            {
                email,
                link
            };

            await Send(templateId, email, data, data.link);
        }

        public async Task SendEmailCustomerActivateddAsync(string name, string email, string code)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateCustomerActived"];

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            var link = $"{_configuration["ClientUrl:FrontEnd:Url"]}/{_configuration["ClientUrl:FrontEnd:Path:RecoveryPassword"]}?code={codeEncoded}&email={email}";
            var data = new
            {
                email,
                link
            };

            await Send(templateId, email, data);
        }

        public async Task SendEmailPartnerCreatedAsync(string name, string email, string code)
        {
            var templateId = _configuration["Integration:SendGrid:TemplatePartnerActived"];

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            var link = $"{_configuration["ClientUrl:FrontEnd:UrlPartner"]}/{_configuration["ClientUrl:FrontEnd:Path:RecoveryPassword"]}?code={codeEncoded}&email={email}";
            var data = new
            {
                email,
                link
            };

            await Send(templateId, email, data);
        }


        public async Task SendEmailAgentAsync(string username, string email)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateNewAgent"];
            var data = new
            {
                username,
                email
            };
            await Send(templateId, email, data);
        }

        public async Task SendEmailAgentDocsAnalisysAsync(string username, string email)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateAgentInAnalisys"];
            var data = new
            {
                username,
                email
            };
            await Send(templateId, email, data);
        }

        public async Task SendEmailAgentActivedAsync(string username, string email)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateAgentActived"];
            var data = new
            {
                username,
                email
            };
            await Send(templateId, email, data);
        }

        public async Task SendEmailAgentRejectedAsync(string username, string email)
        {
            var templateId = _configuration["Integration:SendGrid:TemplateAgentRejected"];
            var data = new
            {
                username,
                email
            };
            await Send(templateId, email, data);
        }

        private async Task Send(string templateId, string email, object data, string link = null)
        {
            try
            {
                var apiKey = _configuration["Integration:SendGrid:apiKey"];
                var client = new SendGridClient(apiKey);

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(EmailFrom, NameFrom),
                    TemplateId = templateId,
                    Personalizations = new List<Personalization>
                    {
                        new Personalization()
                        {
                            Tos = new List<EmailAddress>()
                            {
                                new EmailAddress(email),
                            },
                            TemplateData = data,
                        }
                    }
                };
                var response = await client.SendEmailAsync(msg);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Não foi possível concluir o envio no momento, por favor tente novamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
