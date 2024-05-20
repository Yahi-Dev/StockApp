using StockApp.Core.Application.ViewModels.Categories;
using System.Text.Json.Serialization;

namespace StockApp.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public double? Price { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public CategoryViewModel? Category { get; set; }

        
        public CategoryWithoutProductsViewModel? CategoryWithoutProduct { get; set; }
        public string? UserId { get; set; }
        public string? CategoryName { get; set; }

    }
}
