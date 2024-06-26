using CrudApi.Models.Domain;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;


namespace CrudApi.Models.DTOS
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Sku { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Size Size { get; set; }
        public Guid CategoryId { get; set; }
    }
}
