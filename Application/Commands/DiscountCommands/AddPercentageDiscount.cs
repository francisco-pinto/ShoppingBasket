namespace Application.Commands.DiscountCommands;

using Application.Interfaces;
using Application.Utils;
using Domain.Domain;
using Domain.Domain.DiscountTypes;
using MediatR;

public static class AddPercentageDiscount
{
    public record AddPercentageDiscountCommand : IRequest<Discount>
    {
        public DateTime BeginDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public int Percentage { get; set; }
    }
    
    private class CommandHandler(
        IClock clock,
        IDiscountRepository discountRepository) : IRequestHandler<AddPercentageDiscountCommand, Discount>
    {
        public async Task<Discount> Handle(AddPercentageDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = PercentageDiscount.Create(
                request.BeginDate,
                request.EndDate,
                request.Percentage,
                clock.Now);

            await discountRepository.AddAsync(discount);

            await discountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return discount;
        }
    }
}