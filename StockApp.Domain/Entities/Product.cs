using StockApp.Core.Domain.Common;

namespace StockApp.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string? ImagePath {get; set;}
        public double? Price {get; set;}
        public string Name { get; set; }
        public string? Description { get; set; }
        public int categoryId { get; set;} 

        public Category? Category { get; set;}

        public string? UserId{ get; set; } 
    }
}