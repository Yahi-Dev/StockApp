using AutoMapper;
using StockApp.Core.Application.Dtos.Account;
using StockApp.Core.Application.Features.Categories.Commands.CreateCategory;
using StockApp.Core.Application.Features.Categories.Commands.UpdateCategory;
using StockApp.Core.Application.Features.Products.Commands.CreateProduct;
using StockApp.Core.Application.Features.Products.Commands.UpdateProduct;
using StockApp.Core.Application.Features.Products.Queries.GetAllProducts;
using StockApp.Core.Application.ViewModels.Categories;
using StockApp.Core.Application.ViewModels.Products;
using StockApp.Core.Application.ViewModels.Users;
using StockApp.Core.Domain.Entities;


namespace StockApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region ProductProfile
            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.CategoryName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Product, SaveProductViewModel>()
                .ForMember(x => x.FileImg, opt => opt.Ignore())
                .ForMember(x => x.Categories, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Category, opt => opt.Ignore());
            #endregion

            #region CategoryProfile
                CreateMap<Category, CategoryViewModel>()
                .ForMember(x => x.ProductsQuantity, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Category, CategoryWithoutProductsViewModel>()
               .ForMember(x => x.ProductsQuantity, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreateBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.Products, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Category, SaveCategoryViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Products, opt => opt.Ignore());
            #endregion

            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region CQRS
            CreateMap<GetAllProductsQuery, GetAllProductsParamerts>()
              .ReverseMap();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductUpdateResponse, Product>()
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateCategoryCommand, Category>()
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CategoryUpdateResponse, Category>()
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ReverseMap();

            #endregion
        }
    }
}
