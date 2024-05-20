using AutoMapper;
using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.ViewModels.Categories;
using StockApp.Core.Application.ViewModels.Products;
using StockApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StockApp.Core.Application.Features.Products.Queries.GetProductById
{
    /// <summary>
    /// Parametros para obtener un producto por Id
    /// </summary>
    public class GetProductByIdQuery : IRequest<Response<ProductViewModel>>
    {
        [SwaggerParameter(Description = "Debe colocar el Id del producto que quiere obtener")]
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Response<ProductViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await GetByIdViewModel(request.Id);
            if (product == null) throw new ApiException("Products not found", (int)HttpStatusCode.NotFound);
            return new Response<ProductViewModel>(product);
        }

        private async Task<ProductViewModel> GetByIdViewModel(int id)
        {
            var productlist = await _productRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var product = productlist.FirstOrDefault(f => f.Id == id);

            ProductViewModel productVm = new()
            {
                Name = product.Name,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name,
                Category = _mapper.Map<CategoryViewModel>(product.Category),
                UserId = product.UserId
            };

            return productVm;
        }
    }
}
