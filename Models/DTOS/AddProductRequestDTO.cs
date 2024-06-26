using CrudApi.Models.Domain;

namespace CrudApi.Models.DTOS
{
    public class AddProductRequestDTO
    {

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }

        public string Sku { get; set; }

        public Size Size { get; set; }
    }
}
