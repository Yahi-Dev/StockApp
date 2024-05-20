using StockApp.Core.Domain.Common;

namespace StockApp.Core.Domain.Entities
{
    public class Category : BaseModel
    {
        public ICollection<Product>? Products { get; set; }
    }
}

