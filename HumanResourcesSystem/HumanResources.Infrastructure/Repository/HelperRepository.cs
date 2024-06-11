using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.Exceptions;
using HumanResources.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class HelperRepository : Domain.Repository.IHelperRepository
    {

        private readonly HumanResourcesDatabase _database;

        public HelperRepository(HumanResourcesDatabase Database)
        {
            _database = Database;
        }
        public async Task<string> GenerateRandomUserCode()
        {
            var chars = "0123456789";

            var code = new string(Enumerable.Repeat(chars, 12).Select(s => s[Random.Shared.Next(chars.Length)]).ToArray());

            var codeList = await _database.Users.FirstOrDefaultAsync(pr=>pr.UserCode == code);
            if(codeList != null) 
            {
                throw new ServerErrorException("Generate User Code: Problem with code generation, try again later");
            }
            return code;
        }


        public string GenerateRandomKey()
        {
            var chars = "0123456789abcdefghijklmnoprstuvwxyz_-<>?@#$%^&*+=";

            var code = new string(Enumerable.Repeat(chars, 32).Select(s => s[Random.Shared.Next(chars.Length)]).ToArray());

            return code;
        }
        public string ConfirmEmailBody(ConfirmEmailMessageInfoDto confirmEmail)
        {

            return $@"
                    <div>
                      <h2>Welcome to HR System</h2>
                      <h2>Confirm Your Email Address</h2>
                      <p>
                        Tap the button below to confirm your email address. If you didn't create
                        an account, you can safely delete this email.
                      </p>
                      <div>
                        <a href='https://localhost:7068/api/account/confirm?email={confirmEmail.UserName}&token={confirmEmail.token}'>
                          <button style=""height: 50px"">Confirm Email</button>
                        </a>
                      </div>
                      <p>
                        If that doesn't work, copy and paste the following link in your browser:
                      </p>
                      <a href='https://localhost:7068/api/account/confirm?email={confirmEmail.UserName}&token={confirmEmail.token}'>link</a>
                    </div>";
        }

        public string GenerateForgetPasswordToken (ConfirmEmailMessageInfoDto confirmEmail)
        {

            return $@"
                    <div>
                      <h2>Welcome</h2>
                      <h5>HRSystem here!!</h5>
                      <h2>Did you forget your password?</h2>
                      <p>
                        Tap the button below to start procces of recovery of your password. If you didn't want to
                        recovery your password, you can easly delete this email.
                      </p>
                      <div>
                        <a href='http://localhost:5173/forget/token?token=${confirmEmail.token}'>
                          <button style=""height: 50px"">Recovery Password</button>
                        </a>
                      </div>
                      <p>
                        If that doesn't work, copy and paste the following link in your browser:
                      </p>
                      <a href='http://localhost:5173/forget/token?token={confirmEmail.token}&email={confirmEmail.UserName}'>link</a>
                    </div>";
        }
    }
}
