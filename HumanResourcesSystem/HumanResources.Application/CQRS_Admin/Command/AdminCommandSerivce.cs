using AutoMapper;
using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_Admin.Command
{
    public class AdminCommandSerivce : IAdminCommandService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminCommandSerivce(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<GetUserDto> AddToLeaderAsync(string userCode, RolesEnum role, CancellationToken token)
            => await _adminRepository.AddToLeaderAsync(userCode, role, token);

        public async Task<GetUserDto> AddToManagerAsync(string userCode, RolesEnum role, CancellationToken token) 
            => await _adminRepository.AddToManagerAsync(userCode, role, token);

        public async Task<GetUserDto> AddToAdminAsync(string userCode, RolesEnum role, CancellationToken token) 
            => await _adminRepository.AddToAdminAsync(userCode, role, token);

        public async Task<GetUserDto> RemoveLeaderAsync(string userCode, RolesEnum role, CancellationToken token) 
            => await _adminRepository.RemoveLeaderAsync(userCode, role, token);

        public async Task<GetUserDto> RemoveManagerAsync(string userCode, RolesEnum role, CancellationToken token) 
            => await _adminRepository.RemoveManagerAsync(userCode, role, token);

        public async Task<GetUserDto> RemoveAdminAsync(string userCode, RolesEnum role, CancellationToken token) 
            => await _adminRepository.RemoveAdminAsync(userCode, role, token);


    }
}
