using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Domain
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Category")]
        public int CatId { get; set; }
        public Category Category { get; set; }
    }
}
