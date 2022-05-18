using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace CommentManagement.Configuration
{
    public static class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentApplication, CommentApplication>();

            services.AddDbContext<CommentContext>(options => options.UseSqlServer(connectionString));
        }
    }
}