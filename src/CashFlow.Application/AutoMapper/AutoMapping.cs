using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<ExpenseRequest, Expense>();
            CreateMap<RegisterUserRequest, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, RegisterExpenseResponse>();
            CreateMap<Expense, ShortExpenseResponse>();
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<User, RegisteredUserResponse>();
        }
    }
}

