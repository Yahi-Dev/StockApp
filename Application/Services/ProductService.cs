using AutoMapper;
using Microsoft.AspNetCore.Http;
using StockApp.Core.Application.Dtos.Account;
using StockApp.Core.Application.Helpers;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Interfaces.Services;
using StockApp.Core.Application.ViewModels.Products;
using StockApp.Core.Domain.Entities;


namespace StockApp.Core.Application.NewFolder
{
    public class ProductService : GenericService<SaveProductViewModel, ProductViewModel, Product>,IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
        }
        public async Task<List<ProductViewModel>> GetAllViewModelWithInclude()
        {
            var productlist = await _productRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            if(userViewModel != null )
            {
                productlist =  productlist.Where(product => product.UserId == userViewModel.Id).ToList();
            }
            return productlist.Select(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name
            }).ToList();
        }

        public async Task<List<ProductViewModel>> GetAllViewModelWithFilters(FilterProductViewModel filter)
        {
            var productlist = await _productRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            if (userViewModel != null)
            {
                productlist = productlist.Where(product => product.UserId == userViewModel.Id).ToList();
            }

            var ListFilter = productlist.Select(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name
            }).ToList();

            if (filter.CategoryId != null)
            {
                ListFilter = ListFilter.Where(product => product.CategoryId == filter.CategoryId.Value).ToList();
            }

            return ListFilter;
        }

        public override async Task<SaveProductViewModel> Add(SaveProductViewModel vm)
        {
            vm.UserId = userViewModel != null ? userViewModel.Id : vm.UserId;
            var SavedVm = await base.Add(vm);
            return SavedVm;
        }

        public override async Task Update(SaveProductViewModel vm, int id)
        {
            vm.UserId = userViewModel != null ? userViewModel.Id : vm.UserId;
            await base.Update(vm, id);
        }
    }
}