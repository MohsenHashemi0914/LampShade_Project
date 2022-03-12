using System.Collections.Generic;
using System.Linq;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        #region Constructor

        private readonly ShopContext _shopContext;

        public ProductCategoryQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        #endregion

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            return _shopContext.ProductCategories
                .Include(x => x.Products)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Products = MapProducts(x.Products, x.Name)
                }).ToList();
        }

        #region Utilities

        private static List<ProductQueryModel> MapProducts(List<Product> products, string categoryName)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Category = categoryName,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).OrderByDescending(x => x.Id).ToList();
        }

        #endregion
    }
}