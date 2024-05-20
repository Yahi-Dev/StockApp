namespace StockApp.Core.Application.ViewModels.Categories
{
    public class CategoryWithoutProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductsQuantity { get; set; }
    }
}
