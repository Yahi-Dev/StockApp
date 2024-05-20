using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


namespace StockApp.Core.Application.Features.Products.Commands.DeleteProductById
{
    /// <summary>
    /// Parametos para la eliminacion de un producto
    /// </summary>
    public class DeleteProductByIdCommand :IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id del producto que se va a eliminar")]
        public int Id { get; set; }
    }

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<int>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);

            if (product == null) throw new ApiException("Products not found", (int)HttpStatusCode.NotFound);

            await _productRepository.DeleteAsync(product);

            return new Response<int>(product.Id);
        }
    }
}
