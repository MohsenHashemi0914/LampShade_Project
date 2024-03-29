﻿using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts.Inventory;
using _01_LampshadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration
{
    public static class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryApplication, InventoryApplication>();
            services.AddScoped<IPermissionExposer, InventoryPermissionExposer>();
            services.AddScoped<IInventoryQuery, InventoryQuery>();

            services.AddDbContext<InventoryContext>(options => options.UseSqlServer(connectionString));
        }
    }
}