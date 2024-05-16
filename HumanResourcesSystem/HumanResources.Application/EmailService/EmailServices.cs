using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.Repository;
using MailKit.Net.Smtp;
using System.Net;

namespace HumanResources.Application.EmailService
{
    public class EmailServices : IEmailServices
    {
        private readonly IEmailRepostiory _emailRepostiory;

        public EmailServices(IEmailRepostiory emailRepostiory)
        {
            _emailRepostiory = emailRepostiory;
        }

        public async Task<EmailResponseDto> SendEmailAsync(SendEmailDto sendEmail, CancellationToken token) =>
            await _emailRepostiory.SendEmailAsync(sendEmail, token);
        
    }
}
