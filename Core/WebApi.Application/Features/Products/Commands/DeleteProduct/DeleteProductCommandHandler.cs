using MediatR;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id && !p.IsDeleted);

            if (product is not null)
            {
                product.IsDeleted = true;
                await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
