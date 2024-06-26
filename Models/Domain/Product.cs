using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApi.Models.Domain
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Sku { get; set; }
        public Size Size { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
