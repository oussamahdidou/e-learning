using MailKit.Net.Smtp;
using api.Dtos.EmailConfirmation;
using api.interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace api.Service
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;
        public Mailer(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;
        }
        public async Task SendEmailAsync(string email, string subject, string content)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("E-Learning", _smtpSettings.From));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = content
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_smtpSettings.SmtpServer, _smtpSettings.Port, SecureSocketOptions.StartTls);
                    }
                    else
                    {
                        await client.ConnectAsync(_smtpSettings.SmtpServer);
                    }
                    await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                }

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);

            }
        }
    }
}