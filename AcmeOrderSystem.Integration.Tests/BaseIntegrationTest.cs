using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Services;
using AcmeOrderSystem.Integration.Tests;
using Microsoft.Extensions.DependencyInjection;


namespace AcmeOrderSystem.Integration.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ICustomerService _customerService;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _customerService = _scope.ServiceProvider.GetRequiredService<ICustomerService>();

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}