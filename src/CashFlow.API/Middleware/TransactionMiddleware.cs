using CashFlow.Domain.Repositories;

namespace CashFlow.Api.Middleware;

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        unitOfWork.BeginTransaction();

        try
        {
            await _next(context);
            unitOfWork.Commit();
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }
    }
}
