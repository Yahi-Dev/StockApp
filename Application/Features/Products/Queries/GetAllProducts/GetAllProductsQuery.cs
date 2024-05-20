using AutoMapper;
using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.ViewModels.Categories;
using StockApp.Core.Application.ViewModels.Products;
using StockApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


namespace StockApp.Core.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// Parametros Para filtrar los productos
    /// </summary>
    public class GetAllProductsQuery : IRequest<Response<IList<ProductViewModel>>>
    {
        [SwaggerParameter(Description = "El Id de de la categoria por la cual se va a filtrar")]
        public int? CategoryId { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<IList<ProductViewModel>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<ProductViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllProductsParamerts>(request);
            var productList = await GetAllViewModelWithFilters(filter);
            if (productList == null || productList.Count == 0) throw new ApiException("Products not found", (int)HttpStatusCode.NotFound);
            return new Response<IList<ProductViewModel>>(productList);
        }

        private async Task<List<ProductViewModel>> GetAllViewModelWithFilters(GetAllProductsParamerts filters)
        {
            var productList = await _productRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var listViewModels = productList.Select(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryName = product.Category.Name,
                CategoryId = product.Category.Id,
                CategoryWithoutProduct = _mapper.Map<CategoryWithoutProductsViewModel>(product.Category),
                UserId = product.UserId
            }).ToList();

            if (filters.CategoryId != null)
            {
                listViewModels = listViewModels.Where(product => product.CategoryId == filters.CategoryId.Value).ToList();
            }

            return listViewModels;
        }
    }
}
