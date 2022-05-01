using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        #region Constructor

        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        #endregion

        public ArticleCategoryQueryModel GetArticleCategoryWithArticles(string slug)
        {
            var articleCategory = _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt =x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    MetaDescription= x.MetaDescription,
                    Keywords = x.Keywords,
                    CanonicalAddress = x.CanonicalAddress,
                    Articles = MapArticles(x.Articles),
                    ArticlesCount = (short)x.Articles.Count()
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            articleCategory.KeywordList = articleCategory.Keywords.Split(',').ToList();

            return articleCategory;
        }

        public List<ArticleCategoryQueryModel> GetLatestArticleCategories()
        {
            return _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ArticlesCount = (short)x.Articles.Count()
                }).AsNoTracking()
                .OrderByDescending(x => x.Id).Take(6).ToList();
        }

        #region Utilities

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Slug = x.Slug,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle, 
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
            }).ToList();
        }

        #endregion
    }
}
