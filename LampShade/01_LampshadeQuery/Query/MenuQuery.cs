using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.Menu;
using _01_LampshadeQuery.Contracts.ProductCategory;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class MenuQuery : IMenuQuery
    {
        #region Constructor

        private readonly ShopContext _shopContext;
        private readonly BlogContext _blogContext;

        public MenuQuery(ShopContext shopContext, BlogContext blogContext)
        {
            _shopContext = shopContext;
            _blogContext = blogContext;
        }

        #endregion

        public MenuModel GetDataForMenu()
        {
            var menu = new MenuModel();

            menu.ProductCategories = _shopContext.ProductCategories
                .Select(x =>
                new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug
                }).AsNoTracking()
                .OrderByDescending(x => x.Id).Take(6).ToList();

            menu.ArticleCategories = _blogContext.ArticleCategories
                .Select(x =>
                new ArticleCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug
                }).AsNoTracking()
                .OrderByDescending(x => x.Id).Take(6).ToList();

            return menu;
        }
    }
}
