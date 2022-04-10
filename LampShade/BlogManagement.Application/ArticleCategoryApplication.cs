using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        #region Constructor

        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        #endregion

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.IsExist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleCategory(command.Name, fileName, command.Description,
                command.ShowOrder, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.Add(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleCategoryRepository.IsExist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, slug);

            articleCategory.Edit(command.Name, fileName, command.Description,
                command.ShowOrder, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}