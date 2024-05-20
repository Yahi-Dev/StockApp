using AutoMapper;
using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.ViewModels.Categories;
using StockApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Queries.GetCategoryById
{
    /// <summary>
    /// Parámetros para obtener una categoria por su id
    /// </summary>  
    public class GetCategoryByIdQuery : IRequest<Response<CategoryViewModel>>
    {
        [SwaggerParameter(Description = "El id de la categoria que se desea seleccionar")]
        public int Id { get; set; }
    }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<CategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryViewModel>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllWithIncludeAsync(new List<string> { "Products" });
            var category = categories.FirstOrDefault(w => w.Id == query.Id);
            if (category == null) throw new ApiException("Category not found", (int)HttpStatusCode.NotFound);
            var categoryVm = _mapper.Map<CategoryViewModel>(category);
            categoryVm.ProductsQuantity = category.Products.Count;
            return new Response<CategoryViewModel>(categoryVm);
        }
    }

}
