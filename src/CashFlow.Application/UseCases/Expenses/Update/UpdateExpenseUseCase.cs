﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using System.Data;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    internal class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExpensesUpdateOnlyRepository _expensesRepository;

        public UpdateExpenseUseCase(IUnitOfWork unitOfWork, IMapper mapper, IExpensesUpdateOnlyRepository expensesRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _expensesRepository = expensesRepository;
        }

        public async Task Execute(long id, ExpenseRequest request)
        {
            Validate(request);

            var entity = await _expensesRepository.GetById(id);

            if (entity is null)
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

            _mapper.Map(request, entity);
            _expensesRepository.Update(entity);

            //await _unitOfWork.Commit();
        }

        private void Validate(ExpenseRequest request)
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
