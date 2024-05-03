using AcmeOrderSystem.Api.Database;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace AcmeOrderSystem.Integration.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;

    public IntegrationTestWebAppFactory()
    {
        this._dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithPortBinding("5433")
            .WithHostname("localhost")
            .WithDatabase("AcmeOrderSystem")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _dbContainer.GetConnectionString();
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

        });

    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using (var scope = Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();

            await cntx.Database.EnsureCreatedAsync();
            await cntx.SaveChangesAsync();

            await cntx.Database.ExecuteSqlAsync($"insert into \"Customers\"(\"Id\", \"Name\", \"City\") values(1, 'Customer2', 'Los Angeles')");
        }


    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }


    


}