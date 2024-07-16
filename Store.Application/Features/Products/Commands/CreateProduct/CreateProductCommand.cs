using MediatR;

namespace Store.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quentity { get; set; }
        public int? CatId { get; set; }
    }
}
