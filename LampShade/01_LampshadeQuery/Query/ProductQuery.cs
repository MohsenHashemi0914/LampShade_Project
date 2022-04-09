﻿using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
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

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        #endregion

        public ProductQueryModel GetDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice, x.IsInStock });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.Comments)
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
                    Comments = MapComments(x.Comments),
                    Pictures = MapProductPictures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var price = inventory.FirstOrDefault(x =>
                x.ProductId == product.Id)?.UnitPrice ?? 0;

            var discountRate = discounts.FirstOrDefault(x =>
                x.ProductId == product.Id)?.DiscountRate ?? 0;

            product.Price = price.ToMoney();
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

            return product;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

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
            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice });

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

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
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

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

        #region Utilities

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

        private static List<CommentQueryModel> MapComments(List<Comment> comments)
        {
            return comments.Where(x => !x.IsCanceled && x.IsConfirmed)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message
                }).OrderByDescending(x => x.Id).ToList();
        }

        #endregion
    }
}