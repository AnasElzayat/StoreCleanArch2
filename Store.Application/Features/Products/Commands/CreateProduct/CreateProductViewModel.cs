namespace Store.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quentity { get; set; }
        public string CategoryName { get; set; }
    }
}
