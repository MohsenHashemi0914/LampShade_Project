using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Slide;
using _01_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductApplication, ProductApplication>();

            services.AddScoped<IProductPictureRepository, ProductPictureRepository>();
            services.AddScoped<IProductPictureApplication, ProductPictureApplication>();

            services.AddScoped<ISlideRepository, SlideRepository>();
            services.AddScoped<ISlideApplication, SlideApplication>();

            services.AddScoped<ISlideQuery, SlideQuery>();
            services.AddScoped<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddScoped<IProductQuery, ProductQuery>();
            services.AddScoped<IPermissionExposer, ShopPermissionExposer>();
            services.AddScoped<ICartCalculatorService, CartCalculatorService>();

            services.AddDbContext<ShopContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
