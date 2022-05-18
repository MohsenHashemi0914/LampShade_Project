using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public static class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddScoped<ICustomerDiscountApplication, CustomerDiscountApplication>();

            services.AddScoped<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            services.AddScoped<IColleagueDiscountApplication, ColleagueDiscountApplication>();

            services.AddDbContext<DiscountContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
