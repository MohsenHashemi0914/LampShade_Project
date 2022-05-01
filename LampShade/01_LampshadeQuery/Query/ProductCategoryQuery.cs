using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        #region Constructor

        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
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
                }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var categories = _shopContext.ProductCategories
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
                }).AsNoTracking().OrderByDescending(x => x.Id).ToList();

            categories.ForEach(category =>
            {
                category.Products.ForEach(product =>
                {
                    var price = inventory.FirstOrDefault(x =>
                        x.ProductId == product.Id)?.UnitPrice ?? 0;

                    var discountRate = discounts.FirstOrDefault(x =>
                        x.ProductId == product.Id)?.DiscountRate ?? 0;

                    product.Price = price.ToMoney();
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;

                    if (price > 0 && product.HasDiscount)
                    {
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                });
            });

            return categories;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var category = _shopContext.ProductCategories
                .Include(x => x.Products)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    KeyWords = x.KeyWords,
                    Products = MapProducts(x.Products, x.Name)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            category.Products.ForEach(product =>
            {
                var price = inventory.FirstOrDefault(x =>
                    x.ProductId == product.Id)?.UnitPrice ?? 0;

                var discountRate = discounts.FirstOrDefault(x =>
                    x.ProductId == product.Id)?.DiscountRate ?? 0;

                product.Price = price.ToMoney();
                product.DiscountRate = discountRate;
                product.HasDiscount = discountRate > 0;

                if (price > 0 && product.HasDiscount)
                {
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    product.DiscountExpireDate = discounts.FirstOrDefault(x =>
                    x.ProductId == product.Id).EndDate.ToDiscountFormat();
                }
            });

            return category;
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