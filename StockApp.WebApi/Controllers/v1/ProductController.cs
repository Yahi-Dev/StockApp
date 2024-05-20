using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Application.Features.Products.Commands.CreateProduct;
using StockApp.Core.Application.Features.Products.Commands.DeleteProductById;
using StockApp.Core.Application.Features.Products.Commands.UpdateProduct;
using StockApp.Core.Application.Features.Products.Queries.GetAllProducts;
using StockApp.Core.Application.Features.Products.Queries.GetProductById;
using StockApp.Core.Application.ViewModels.Products;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace StockApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Basic")]
    [SwaggerTag("Mantenimiento de productos")]
    public class ProductController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Listado de productos ",
                          Description = "Obtine todos los productos filtrados por categorias")]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsParamerts filters)
        {
                return Ok(await Mediator.Send
                (
                   new GetAllProductsQuery()
                   {
                       CategoryId = filters.CategoryId
                   }

                 ));
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveProductViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener por Id",
                          Description = "Obtine un producto filtrando por el id del mismo")]
        public async Task<IActionResult> Get(int id)
        {
                return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));

        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Creacion de productos ",
                          Description = "Recibe los parametros necesarios para crear un nuevo producto")]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();
        }


        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveProductViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actulizacion de producto",
                          Description = "Obtiene los datos necesarios para modificar un producto existente")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductCommand command)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un producto ",
                          Description = "Obtiene los datos necesarios para eliminar un producto existente")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductByIdCommand { Id = id });
            return NoContent();
        }
    }
}
