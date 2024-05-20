namespace StockApp.Core.Domain.Common
{
    public class BaseModel : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
