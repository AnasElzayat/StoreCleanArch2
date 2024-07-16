using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quentity { get; set; }

        [ForeignKey("Category")]
        public int? CatId { get; set; }
        public Category? Category { get; set; }
    }
}
