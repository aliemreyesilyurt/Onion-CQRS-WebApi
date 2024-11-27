using MediatR;
using WebApi.Application.Interfaces.AutoMapper;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id && !p.IsDeleted);

            var map = _mapper.Map<Product, UpdateProductCommandRequest>(request);

            var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>()
                .GetAllAsync(pc => pc.ProductId == product.Id);

            await _unitOfWork.GetWriteRepository<ProductCategory>()
                .HardDeleteRangeAsync(productCategories);

            foreach (var categoryId in request.CategoryIds)
            {
                await _unitOfWork.GetWriteRepository<ProductCategory>()
                    .AddAsync(new() { CategoryId = categoryId, ProductId = product.Id });
            }

            await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
            await _unitOfWork.SaveAsync();
        }
    }
}
