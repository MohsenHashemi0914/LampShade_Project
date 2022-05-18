using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.Menu;
using _01_LampshadeQuery.Query;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Configuration
{
    public static class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleCategoryApplication, ArticleCategoryApplication>();

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleApplication, ArticleApplication>();

            services.AddScoped<IMenuQuery, MenuQuery>();
            services.AddScoped<IArticleQuery, ArticleQuery>();
            services.AddScoped<IArticleCategoryQuery, ArticleCategoryQuery>();

            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));
        }
    }
}