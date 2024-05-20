using AutoMapper;
using MediatR;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.ViewModels.Categories;
using StockApp.Core.Application.ViewModels.Products;
using StockApp.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Queries.GetAllCategories
{
    /// <summary>
    /// Parámetros para el listado de categoria
    /// </summary>  
    public class GetAllCategoriesQuery : IRequest<Response<IEnumerable<CategoryViewModel>>>
    {       
    }
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Response<IEnumerable<CategoryViewModel>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CategoryViewModel>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {        
            var categoriesViewModel = await GetAllViewModelWithInclude();
            return new Response<IEnumerable<CategoryViewModel>>(categoriesViewModel);
        }

        private async Task<List<CategoryViewModel>> GetAllViewModelWithInclude()
        {
            var categoryList = await _categoryRepository.GetAllWithIncludeAsync(new List<string> { "Products" });

            return categoryList.Select(category => new CategoryViewModel
            {
                Name = category.Name,
                Description = category.Description,
                Id = category.Id,
                ProductsQuantity = category.Products.Count
            }).ToList();
        }
    }
}
