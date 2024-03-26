using HumanResources.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_User.Command
{
    public class UserCommandService:IUserCommandService
    {

        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository accountReposiotry)
        {
            _userRepository = accountReposiotry;
        }
    }
}
