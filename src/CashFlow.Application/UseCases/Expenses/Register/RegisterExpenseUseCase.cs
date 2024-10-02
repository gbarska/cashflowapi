﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase : IRegisterExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _expensesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterExpenseUseCase(
            IExpensesWriteOnlyRepository expensesRepository, 
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _expensesRepository = expensesRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterExpenseResponse> Execute(ExpenseRequest request)
        {
            Validate(request);

            var entity = _mapper.Map<Expense>(request);

            await _expensesRepository.Add(entity);
            //await _unitOfWork.Commit();

            return _mapper.Map<RegisterExpenseResponse>(entity);
        }

        private void Validate (ExpenseRequest request)
        {
            var validator = new ExpenseValidator();
            
            var result = validator.Validate(request);
        
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
