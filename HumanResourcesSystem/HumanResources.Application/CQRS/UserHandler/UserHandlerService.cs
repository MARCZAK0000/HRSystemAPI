﻿using AutoMapper;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.UserHandler
{
    public class UserHandlerService : IUserHandlerService
    {

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserHandlerService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<UserInfoResponse> GetInforamtionsAboutUserAsync(string email, string phonenumer) => 
            _mapper.Map<UserInfoResponse>(await _userRepository.GetInfomrationsAboutUserAsync(email, phonenumer));


        public async Task<UserResponse> GenerateConfirmEmailTokenAsync(string email) =>
            await _userRepository.GenerateConfirmEmailTokenAsync(email);

        public async Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber) =>
            await _userRepository.GenerateForgetPasswordTokenAsync(email, phonenumber);
    }
}
