using AutoMapper;
using MediatR;
using Store.Application.Interfaces;
using Store.Domain;

namespace Store.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IEnumerable<GetProductsListViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductsListViewModel>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products;
            if (request.Filter != null)
            {
                products = await _unitOfWork.ProductRepository.GetListAsync(request.Filter, [ "Category" ]);
            }
            else
            {
                products = await _unitOfWork.ProductRepository.GetAllAsync([ "Category" ]);
            }

            var productViewModels = _mapper.Map<IEnumerable<GetProductsListViewModel>>(products);
            return productViewModels;
        }
    }
}

