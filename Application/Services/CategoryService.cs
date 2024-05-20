using AutoMapper;
using Microsoft.AspNetCore.Http;
using StockApp.Core.Application.Dtos.Account;
using StockApp.Core.Application.Helpers;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Interfaces.Services;
using StockApp.Core.Application.ViewModels.Categories;
using StockApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;


namespace StockApp.Core.Application.NewFolder
{
    public class CategoryService : GenericService<SaveCategoryViewModel, CategoryViewModel, Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;

        }

        public async Task<List<CategoryViewModel>> GetAllViewModelWithInclude()
        {
            var categorylist = await _categoryRepository.GetAllWithIncludeAsync(new List<string> { "Products" });

            return categorylist.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ProductsQuantity = _userViewModel != null ? category.Products.Where(product => product.UserId == _userViewModel.Id).Count() : category.Products.Count()
            }).ToList();
        }


    }
}