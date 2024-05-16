using HumanResources.Domain.EmailModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.EmailService
{
    public interface IEmailServices
    {
        Task<EmailResponseDto> SendEmailAsync(SendEmailDto sendEmail, CancellationToken token);
    }
}
