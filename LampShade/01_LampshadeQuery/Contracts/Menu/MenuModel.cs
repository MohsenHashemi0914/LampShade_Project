using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;

namespace _01_LampshadeQuery.Contracts.Menu
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; } = new();
        public List<ProductCategoryQueryModel> ProductCategories { get; set; } = new();
    }
}
