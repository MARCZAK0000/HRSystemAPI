using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Authentication;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;

namespace HumanResources.Infrastructure.Repository
{
    public class EmailRepository : IEmailRepostiory
    {
        
        private readonly EmailAuthenticationSettings _emailAuthenticationSettings;

        public EmailRepository( EmailAuthenticationSettings emailAuthenticationSettings)
        {
            _emailAuthenticationSettings = emailAuthenticationSettings;
        }

        public async Task<EmailResponseDto> SendEmailAsync(SendEmailDto sendEmail)
        {
            var messeage = await CreateMessage(sendEmail);
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailAuthenticationSettings.Host, _emailAuthenticationSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            if (!smtp.IsConnected)
            {
                throw new BadRequestException("Cannot connect to SMTP");
            }
            await smtp.AuthenticateAsync(new NetworkCredential(_emailAuthenticationSettings.Email, _emailAuthenticationSettings.Password));
            await smtp.SendAsync(messeage);
            await smtp.DisconnectAsync(true);

            return new EmailResponseDto() 
            {
                EmailFrom = sendEmail.EmailFrom,
                Emailto = sendEmail.EmailTo,
            };
        }
        
        private Task<MimeMessage> CreateMessage(SendEmailDto sendEmail) 
        {
            
            var _email = new MimeMessage();
            _email.From.Add(MailboxAddress.Parse(sendEmail.EmailFrom));
            _email.To.Add(MailboxAddress.Parse(sendEmail.EmailTo));
            _email.Subject = sendEmail.EmailSubject;
            _email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = sendEmail.EmailBody };

            return Task.FromResult(_email);
        }

    }
}
