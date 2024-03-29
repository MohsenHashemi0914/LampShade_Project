﻿using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
    {
        #region Constructor

        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _context.ProductCategories
                .Select(x => new EditProductCategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug
                }).FirstOrDefault(x => x.Id == id);
        }

        public string GetSlugById(long id)
        {
            return _context.ProductCategories.Find(id)?.Slug;
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories
                .Include(x => x.Products)
                .Select(x => new ProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductsCount = x.Products.Count
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name == searchModel.Name);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}