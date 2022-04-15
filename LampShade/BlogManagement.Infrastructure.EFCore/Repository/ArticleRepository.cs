using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : BaseRepository<long, Article>, IArticleRepository
    {
        #region Constructor

        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public Article GetArticleWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Include(x => x.Category)
                .Select(x => new EditArticle
                {
                    Id = id,
                    Title = x.Title,
                    Descriptioin = x.Description,
                    ShortDescription = x.ShortDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    CanonicalAddress = x.CanonicalAddress,
                    PublishDate = x.PublishDate.ToFarsi(),
                    CategoryId = x.CategoryId
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Include(x => x.Category)
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Picture = x.Picture,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
