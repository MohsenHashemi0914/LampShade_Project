using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        #region Constructor

        private readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        #endregion

        public ArticleQueryModel GetDetails(string slug)
        {
            var article = _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Slug = x.Slug,
                    CanonicalAddress = x.CanonicalAddress,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Description = x.Description,
                    ShortDescription = x.ShortDescription
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            article.KeywordList = article.Keywords.Split(',').ToList();

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription
                }).AsNoTracking()
                .OrderByDescending(x => x.Id).Take(6).ToList();
        }
    }
}
