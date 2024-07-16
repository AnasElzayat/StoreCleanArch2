using AutoMapper;
using MediatR;
using Store.Application.Interfaces;
using Store.Domain;

namespace Store.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateProductViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            CreateCommandValidator _validator = new();
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new Exception("Product is not valid");
            }

            var product = _mapper.Map<Product>(request);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();

            product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id == product.Id, [ "Category" ]);

            var productViewModel = _mapper.Map<CreateProductViewModel>(product);
            return productViewModel;
        }
    }
}
