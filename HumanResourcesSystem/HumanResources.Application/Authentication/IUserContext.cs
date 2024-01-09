using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.Authentication
{
    public interface IUserContext
    {

        CurrentUser GetCurrentUser();

    }
}
