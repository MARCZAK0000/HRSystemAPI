using AutoMapper;
using HumanResources.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Admin.Handler
{
    public class AdminHandlerService : IAdminHandlerService
    {
        private readonly IMapper _mapper;

        private readonly IAdminRepository _adminRepository;

        public AdminHandlerService(IMapper mapper, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }
    }
}
