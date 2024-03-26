using HumanResources.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_User.Handler
{
    public class UserHandlerService:IUserHandlerService
    {

        private readonly IUserRepository _userRepository;


        public UserHandlerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
