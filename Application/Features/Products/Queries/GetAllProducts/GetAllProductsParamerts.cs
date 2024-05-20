using Swashbuckle.AspNetCore.Annotations;

namespace StockApp.Core.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// Parametros Para filtrar los productos
    /// </summary>
    public class GetAllProductsParamerts
    {
        [SwaggerParameter(Description = "El Id de de la categoria por la cual se va a filtrar")]
        public int? CategoryId { get; set; }
    }
}
