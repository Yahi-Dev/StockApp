using StockApp.Core.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Core.Application.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public int ProductsQuantity { get; set; }
        public ICollection<ProductViewModel>? Products { get; set; }
    }
}
