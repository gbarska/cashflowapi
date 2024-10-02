using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<RegisteredUserResponse> Execute(RegisterUserRequest request);
    }
}
