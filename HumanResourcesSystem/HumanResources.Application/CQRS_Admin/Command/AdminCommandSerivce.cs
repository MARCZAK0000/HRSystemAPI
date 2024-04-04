using AutoMapper;
using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Admin.Command
{
    public class AdminCommandSerivce : IAdminCommandService
    {
        private readonly IMapper _mapper;

        private readonly IAdminRepository _adminRepository;

        public AdminCommandSerivce(IMapper mapper, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }

        public async Task<GetUserDto> AddToRoleAsync(string userId, RolesEnum role) => 
            await _adminRepository.AddToRoleAsync(userId, role);

        public async Task<GetUserDto> RemoveRoleAsync(string userId, RolesEnum role) =>
            await _adminRepository.RemoveRoleAsync(userId, role);
    }
}
