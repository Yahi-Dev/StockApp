using AutoMapper;
using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Wrappers;
using StockApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StockApp.Core.Application.Features.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Parametros Para la actualizacion de un proyecto
    /// </summary>
    public class UpdateProductCommand : IRequest<Response<ProductUpdateResponse>>
    {
        [SwaggerParameter(Description = "El Id del producto que se esta actualizando")]
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nuevo nombre del producto")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Seleccionar la imagen del producto")]
        public string? ImagePath { get; set; }

        [SwaggerParameter(Description = "Una nueva descripcion para el producto")]
        public string? Description { get; set; }

        [SwaggerParameter(Description = "El nuevo precio que se le pondra del producto")]
        public double? Price { get; set; }

        [SwaggerParameter(Description = "El Id de la categoria que pertenece al producto")]
        public int CategoryId { get; set; }
        public string? UserId { get; set; }
    }

    public class UpdateProductCommandhandler : IRequestHandler<UpdateProductCommand, Response<ProductUpdateResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandhandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductUpdateResponse>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product == null) throw new ApiException("Products not found", (int)HttpStatusCode.NotFound);

            product = _mapper.Map<Product>(command);

            await _productRepository.UpdateAsync(product, product.Id);

            var productResponse = _mapper.Map<ProductUpdateResponse>(product);

            return new Response<ProductUpdateResponse>(productResponse);
        }
    }
}
