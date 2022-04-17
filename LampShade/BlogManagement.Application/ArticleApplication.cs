using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        #region Constructor

        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, 
            IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        #endregion

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.IsExist(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            var article = new Article(command.Title, command.ShortDescription, command.Description,
                publishDate, fileName, command.PictureAlt, command.PictureTitle, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.Add(article);
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.GetArticleWithCategory(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleRepository.IsExist(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            article.Edit(command.Title, command.ShortDescription, command.Description,
                publishDate, fileName, command.PictureAlt, command.PictureTitle, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
