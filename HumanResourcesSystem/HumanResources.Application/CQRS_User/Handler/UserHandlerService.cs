using AutoMapper;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_User.Handler
{
    public class UserHandlerService : IUserHandlerService
    {

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapperProfile;

        public UserHandlerService(IUserRepository userRepository, IMapper mappperProfile)
        {
            _userRepository = userRepository;
            _mapperProfile = mappperProfile;
        }

        public async Task<GetInfromationsDto> GetInfromationsAboutUserAsync() 
            => _mapperProfile.Map<GetInfromationsDto>(await _userRepository.GetInfromationsAboutUserAsync());


        
    }
}
