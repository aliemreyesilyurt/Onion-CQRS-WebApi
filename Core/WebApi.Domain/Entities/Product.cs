using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {

        }

        public Product(string title, string description, decimal price, decimal discount, int brandId)
        {
            Title = title;
            Description = description;
            Price = price;
            Discount = discount;
            BrandId = brandId;
        }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required decimal Discount { get; set; }
        //public required string ImagePath { get; set; }

        public required int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
