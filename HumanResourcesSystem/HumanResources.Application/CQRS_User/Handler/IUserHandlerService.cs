using AutoMapper;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_User.Handler
{
    public interface IUserHandlerService
    {
        public Task<GetInfromationsDto> GetInfromationsAboutUserAsync();
         
    }
}
