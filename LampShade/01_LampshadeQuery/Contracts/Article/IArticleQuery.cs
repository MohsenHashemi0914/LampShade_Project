namespace _01_LampshadeQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetDetails(string slug);
        List<ArticleQueryModel> GetLatestArticles();
    }
}
