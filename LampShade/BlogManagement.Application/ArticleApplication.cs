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
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditArticle command)
        {
            throw new NotImplementedException();
        }

        public EditArticle GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
