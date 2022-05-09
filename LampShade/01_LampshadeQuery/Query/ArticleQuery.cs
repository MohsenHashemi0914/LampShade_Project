using _0_Framework.Application;
using _0_Framework.Presentation;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        #region Constructor

        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(IServiceProvider serviceProvider)
        {
            _blogContext = serviceProvider.GetService<BlogContext>();
            _commentContext = serviceProvider.GetService<CommentContext>();
        }

        #endregion

        public ArticleQueryModel GetDetails(string slug)
        {
            var article = _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
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

            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article && x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi()
                }).AsNoTracking()
                .OrderByDescending(x => x.Id).ToList();

            if (comments is not null && comments.Any())
            {
                comments.ForEach(comment =>
                {
                    if (comment.ParentId is not null)
                        comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;  
                });
            }

            article.Comments = comments;
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
