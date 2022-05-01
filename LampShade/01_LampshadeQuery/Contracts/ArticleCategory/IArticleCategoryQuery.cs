namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategoryWithArticles(string slug);
        List<ArticleCategoryQueryModel> GetLatestArticleCategories();
    }
}
