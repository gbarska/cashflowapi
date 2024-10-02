﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<RegisteredUserResponse> Execute(LoginRequest request);
    }
}
