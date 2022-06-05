using _0_Framework.Application;
using _0_Framework.Presentation;
using _01_LampshadeQuery.Contracts.Comment;
using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        #region Constructor

        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(IServiceProvider serviceProvider)
        {
            _shopContext = serviceProvider.GetService<ShopContext>();
            _inventoryContext = serviceProvider.GetService<InventoryContext>();
            _discountContext = serviceProvider.GetService<DiscountContext>();
            _commentContext = serviceProvider.GetService<CommentContext>();
        }

        #endregion

        public ProductQueryModel GetDetails(string slug)
        {
            var product = GetProductBy(slug);

            if (product == null)
                return new ProductQueryModel();

            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice, x.IsInStock });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var price = inventory.FirstOrDefault(x =>
                x.ProductId == product.Id)?.UnitPrice ?? 0;

            var discountRate = discounts.FirstOrDefault(x =>
                x.ProductId == product.Id)?.DiscountRate ?? 0;

            product.Price = price.ToMoney();
            product.DoublePrice = price;
            product.DiscountRate = discountRate;
            product.HasDiscount = discountRate > 0;
            product.IsInStock = inventory.FirstOrDefault(x =>
            x.ProductId == product.Id)?.IsInStock ?? false;

            if (price > 0 && product.HasDiscount)
            {
                var discountAmount = Math.Round((price * discountRate) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
                product.DiscountExpireDate = discounts.FirstOrDefault(x =>
                x.ProductId == product.Id).EndDate.ToDiscountFormat();
            }

            product.Comments = _commentContext.Comments
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Where(x => x.Type == CommentType.Product && x.OwnerRecordId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var products = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            products.ForEach(product =>
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

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    KeyWords = x.KeyWords,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value)
                || x.ShortDescription.Contains(value) || x.KeyWords.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            products.ForEach(product =>
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

            return products;
        }

        public List<CartItem> CheckInventoryStatusFor(List<CartItem> cartItems)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.IsInStock, CurrentCount = x.CalculateCurrentCount() });
           
            cartItems.Where(cartItem => inventory.Any(x => cartItem.Id == x.ProductId && x.IsInStock))
                .ToList()
                .ForEach(cartItem =>
            {
                var itemInventory = inventory.First(x => x.ProductId == cartItem.Id);
                cartItem.IsInStock = itemInventory.CurrentCount >= cartItem.Count;
            });
           
            return cartItems;
        }

        #region Utilities

        private ProductQueryModel GetProductBy(string slug)
        {
            return _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Code = x.Code,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Pictures = MapProductPictures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> pictures)
        {
            return pictures.Where(x => !x.IsRemoved)
                .Select(x => new ProductPictureQueryModel
                {
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).ToList();
        }

        #endregion
    }
}