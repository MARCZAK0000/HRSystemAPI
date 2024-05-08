using HumanResources.Application.Authentication;
using HumanResources.Application.EmailService;
using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using HumanResources.Infrastructure.Authentication;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HumanResources.Infrastructure.Repository
{
    public class AccountRepository : IAccountReposiotry
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContext _userContext;
        private readonly HumanResourcesDatabase _database; 
        private readonly IHelperRepository _helperRepository;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IEmailServices _emailServices;
        private readonly EmailAuthenticationSettings _emailAuthenticationSettings;
        private readonly IEmailRepostiory _emailRepository;
        public AccountRepository(SignInManager<User> signInManager, UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, 
            IUserContext userContext, HumanResourcesDatabase database, IHelperRepository helperRepository, 
            RoleManager<Roles> roleManager, IEmailServices emailServices, EmailAuthenticationSettings emailAuthenticationSettings,
            IEmailRepostiory emailRepostiory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContext = userContext;
            _database = database;
            _helperRepository = helperRepository;
            _roleManager = roleManager;
            _emailServices = emailServices;
            _emailAuthenticationSettings = emailAuthenticationSettings;
            _emailRepository = emailRepostiory;
        }

        public async Task<UserResponse> RegisterAsync(RegisterAccountAsyncDto registerUser)
        {
            if (registerUser is null)
            {
                throw new NotFoundException("Registration: Empty Request"); 
            }

            var checkEmail = await _userManager.FindByEmailAsync(registerUser.Email);
            var confirmPassword = registerUser.Password.ToLower() == registerUser.ConfirmPassword.ToLower();

            if (checkEmail is not null || !confirmPassword)
            {
                throw new BadRequestException("Registration: Email is in use or passwords are not the same"); 
            }

            var createUser = new User()
            {
                Email = registerUser.Email,
                UserName = registerUser.Email,
                UserCode = await _helperRepository.GenerateRandomUserCode(),
                PhoneNumber = registerUser.PhoneNumber,
            };

            createUser.PasswordHash = _passwordHasher.HashPassword(createUser, registerUser.Password);
            

            var result = await _userManager.CreateAsync(createUser);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Registration: Registration went wrong");
            }
            await _userManager.AddToRoleAsync(createUser, "User");


            return new UserResponse()
            {
                Email = registerUser.Email,
                Result = result.Succeeded,
                Message = "Well Done"
            };
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.SerialNumber,user.UserCode!),
            };

            var getrole = await _userManager.GetRolesAsync(user);

            foreach (var item in getrole)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDays = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireDay);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, null, expireDays, credentails);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public Task<string> GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Task.FromResult(Convert.ToBase64String(randomNumber));
        }

        public async Task<UserResponse> SignInAccountAsync(LoginAccountAsyncDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user is null)
            {
                throw new InvalidEmailOrPasswordExcepiton("SignIn: Invalid Email or Password");
            }

            var passwordResult = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);
            if (!passwordResult.Succeeded)
            {
                throw new InvalidEmailOrPasswordExcepiton("SignIn: Invalid Email or Password");
            }

            var token = await GenerateTokenAsync(user);

            var refresh = await GenerateRefreshTokenAsync();
            user.RefreshToken = refresh;

            await _database.SaveChangesAsync();

            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;

            return new UserResponse()
            {
                Result = true,
                Email = user.Email,
                Username = user.UserName,
                Message = $"Well done {user.Email}, You have logged successfully ",
                Token = token,
                RefreshToken = refresh
            };
        }

        public async Task<UserResponse> RefreshTokenAsync(string refreshToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("RefreshToken: Invalid jwtToken");

            if(user.RefreshToken != refreshToken)
            {
                throw new BadRequestException("Invalid refresh Token");
            }
            var newRefreshToken = await GenerateRefreshTokenAsync();
            user.RefreshToken = newRefreshToken;
            var accessToken = await GenerateTokenAsync(user);

            await _database.SaveChangesAsync();
            return new UserResponse()
            {
                Result = true,
                Username = user.UserName,
                Email = user.Email,
                Token = accessToken,
                RefreshToken = newRefreshToken,
                Message = "Well done"
            };
        }

        public async Task<bool> LogOutAsync()
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Something went wrong");

            user.RefreshToken = null;
            await _database.SaveChangesAsync();
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            return true;
        }

        public async Task<EmailResponseDto> GenerateConfirmEmailTokenAsync()
        {

            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("GenerateToken: Invalid Email");

            var checkAuthentication = await _userManager.IsEmailConfirmedAsync(user);
            if (checkAuthentication)
            {
                return new EmailResponseDto()
                {
                    IsConfirmed = true,
                    Message = "Email is confirmed"
                };
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var tokenEncoded = WebUtility.UrlEncode(token);

            var renderMessage = _helperRepository.ConfirmEmailBody(new ConfirmEmailMessageInfoDto { token = tokenEncoded, UserName = user.UserName!});


            var emailInformations = new SendEmailDto()
            {
                EmailFrom = _emailAuthenticationSettings.Email,
                EmailTo = user.Email!,
                EmailSubject = "Confirm Email",
                EmailBody = renderMessage
            };

            var result = await _emailServices.SendEmailAsync(emailInformations);
            return result;
        }

        public async Task<UserResponse> ConfirmEmailAsync(string email, string token)
        {

            var user = await _userManager.FindByEmailAsync(email) ?? throw new InvalidEmailOrPasswordExcepiton("Confirm Email: Invalid Email");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return new UserResponse()
            {
                Result = result.Succeeded,
                Message = result.Succeeded? "Well done": $"Confirm Email: {string.Join(",", result.Errors.Select(s=>s.Description))}"
            };
        }
       
        public async Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto login)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)?? 
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: We cannot find user with that Email and Password"); ;

            if(login.Password.ToLower() != login.ConfirmPassword.ToLower() || login.NewPassword.ToLower() != login.ConfirmNewPassword.ToLower()) 
            {
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: Password and Confirm Password are not the same");
            }

            var checkPassword = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

            if (!checkPassword.Succeeded)
            {
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: We can not find user with that Email and Password");
            }


            var changePassword = await _userManager.ChangePasswordAsync(user, login.Password, login.NewPassword);


            return new UserResponse()
            {
                Result = changePassword.Succeeded,
               
                Message = changePassword.Succeeded ? "Well Done" : $"ChangePassword - We can not change your password: {string.Join(",", changePassword.Errors.Select(s=>s.Description))}"
            };

           
        } //Try to add option for Admin to change Password!

        public async Task<UserResponse> ForgetPasswordAsync(ResetPasswordAsyncDto forgetPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgetPassword.Email);

            if(user is null || forgetPassword.Password.ToLower() != forgetPassword.ConfirmPassword.ToLower())
            {
                throw new InvalidEmailOrPasswordExcepiton("Forget Password: Invalid Email");
            }

            var decision = await _userManager.ResetPasswordAsync(user, forgetPassword.Token, forgetPassword.Password);


            return new UserResponse()
            {
                Result = decision.Succeeded,
                Message = decision.Succeeded ? "Well done" : $"Forget Password: {string.Join(", ", decision.Errors.Select(s => s.Description))}"
            };

        }

        public async Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.PhoneNumber!.ToLower() != phonenumber.ToLower()) 
            {
                throw new InvalidEmailOrPasswordExcepiton("GenerateForgetPasswordToken: Invalid Email or phone number");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);

            var emailMessageBody = _helperRepository.GenerateForgetPasswordToken(new ConfirmEmailMessageInfoDto
            {
                token = encodedToken,
                UserName = email
            });
            await _emailServices.SendEmailAsync(new SendEmailDto()
            {
                EmailFrom = _emailAuthenticationSettings.Email,
                EmailTo = email,
                EmailSubject = "Forget Password",
                EmailBody = emailMessageBody
            });

            return new UserResponse()
            {
                Result = token!=null,
                Message = token != null ? "Well Done" : "Something went wrong",
            };
        }

        public async Task<UserResponse> GeneratePhoneNumberChangeTokenAsync(ChangePhoneNumberDto phoneNumber)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("GeneratePhoneNumberToken: We cannot find user with that Email and Password"); ;

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber.PhoneNumber);
            


            return new UserResponse()
            {
                Result = token != null,
                Message = token!=null? "Well Done": "Somethign went wrong",
                Token = token
            };

        }

        public async Task<UserResponse> ChangePhoneNumberAsync(ChangePhoneNumberDto changePhone)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("GeneratePhoneNumberToken: We cannot find user with that Email and Password"); ;

            if (changePhone.Token == null)
            {
                return new UserResponse()
                {
                    Result = false,
                    Message = "Invalid Token"
                };
            }

            var result = await _userManager.ChangePhoneNumberAsync(user, changePhone.PhoneNumber, changePhone.Token!);
            return new UserResponse()
            {
                Result = result.Succeeded,
                Message = result.Succeeded ? "Well Done" : "Something went wrong"
            };
        }


    }
}
