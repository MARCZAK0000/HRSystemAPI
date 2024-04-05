using HumanResources.Domain.EmailModelDto;



namespace HumanResources.Domain.Repository
{
    public interface IEmailRepostiory
    {
        Task <EmailResponseDto> SendEmailAsync (SendEmailDto sendEmail);
    }
}
