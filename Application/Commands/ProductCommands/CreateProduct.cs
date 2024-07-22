namespace Application.Commands.ProductCommands;

using Application.Interfaces;
using Application.Utils;
using Domain.Domain;
using MediatR;

public static class CreateProduct
{
    public record CreateProductCommand(string Name) : IRequest<Product>;

    public class CommandHandler(
        IClock clock,
        IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Product>
    {
        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, clock.Now);

            await productRepository.AddAsync(product);

            await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return product;
        }
    }
}