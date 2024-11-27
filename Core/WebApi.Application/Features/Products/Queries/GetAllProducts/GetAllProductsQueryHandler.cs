using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.DTOs;
using WebApi.Application.Interfaces.AutoMapper;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(b => b.Brand));

            _mapper.Map<BrandDto, Brand>(new Brand()); //eslestirme kurali AutoMapper'a tanitilmis oldu

            var map = _mapper.Map<GetAllProductsQueryResponse, Product>(products);

            foreach (var item in map)
                item.Price -= (item.Price * item.Discount / 100);

            return map;
        }
    }
}
