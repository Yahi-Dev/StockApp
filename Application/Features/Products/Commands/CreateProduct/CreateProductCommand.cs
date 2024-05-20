using AutoMapper;
using MediatR;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Wrappers;
using StockApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace StockApp.Core.Application.Features.Products.Commands.CreateProduct
{
    /// <summary>
    /// Parametos para crear un producto
    /// </summary>
    public class CreateProductCommand : IRequest<Response<int>>
    {
        /// <example>
        /// PS5
        /// </example>
        [SwaggerParameter(Description = "El nombre del producto")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "Seleccionar la imagen del producto")]
        public string? ImagePath { get; set; }


        /// <example>
        /// Producto electronico de entretenimiento creado por sony
        /// </example>
        [SwaggerParameter(Description = "Poner una descripcion del producto")]
        public string? Description { get; set; }


        /// <example>
        /// 7000.00
        /// </example>

        [SwaggerParameter(Description = "Ponemos el precio del producto")]
        public double? Price { get; set; }


        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "El id de la categoria a la que pertenece el producto")]
        public int CategoryId { get; set; }


        /// <example>
        /// 7450d0cd-47b7-469f-8f20-8215e8a0f1d2
        /// </example>
        [SwaggerParameter(Description = "El Id del usuario que creo este producto")]
        public string? UserId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(command);
            await _productRepository.AddAsync(product);
            return new Response<int>(product.Id);
        }
    }
}
