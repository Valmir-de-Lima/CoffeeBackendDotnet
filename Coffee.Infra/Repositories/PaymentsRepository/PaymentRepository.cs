using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.PaymentCommands;
using Coffee.Domain.Models.Payments;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.PaymentsRepository;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    private readonly CoffeeDataContext _context;

    public PaymentRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Payments
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<PaymentCommandResult>(
                await _context.Payments
                            .AsNoTracking()
                            .Select(x => new PaymentCommandResult(x))
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync()
        );
        return new
        {
            count,
            skip,
            take,
            list
        };
    }

    public async Task<Payment?> GetByOrderIdAsync(Guid id)
    {
        var payment = await _context.Payments
                        .Include(x => x.Order)
                        .ThenInclude(p => p.Items)
                        .FirstOrDefaultAsync(x => x.OrderId == id);
        return payment ?? null!;
    }

    public async Task<Payment?> GetByIdWithOrderAsync(Guid id)
    {
        var payment = await _context.Payments
                        .Include(x => x.Order)
                        .ThenInclude(p => p.Items)
                        .FirstOrDefaultAsync(x => x.Id == id);
        return payment ?? null!;
    }

    public async Task<dynamic> GetByCustomerIdAsync(Guid id, int skip = 0, int take = 25)
    {
        var count = await _context.Payments
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<PaymentCommandResult>(
                await _context.Payments
                            .AsNoTracking()
                            .Where(x => x.CustomerId == id)
                            .Select(x => new PaymentCommandResult(x))
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync()
        );
        return new
        {
            count,
            skip,
            take,
            list
        };
    }
}