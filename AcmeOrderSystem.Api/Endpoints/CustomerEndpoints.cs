using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using AcmeOrderSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace AcmeOrderSystem.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("customers", [Authorize(Roles = "admin")] async (
            CreateCustomerRequest request,
            ICustomerService customerService) =>
        {
            
            var customer = await customerService.AddCustomer(request);

            if (customer is null) 
            { 
                return Results.NoContent();
            }
            
            return Results.Ok(customer);
        }).WithTags("Customers");

        app.MapGet("customers", [Authorize(Roles = "admin")] async (
            ICustomerService customerService) =>
        {
            var customers = await customerService.GetAllCustomers();
                
            return Results.Ok(customers);
        }).WithTags("Customers");

        app.MapGet("customers/{id}", [Authorize(Roles = "admin")] async (
            int id,
            ICustomerService customerService) =>
        {
            var customer = await customerService.GetSingleCustomer(id);

            return customer is null ? Results.NotFound() : Results.Ok(customer);
        }).WithTags("Customers");

        app.MapPut("customers/{id}", [Authorize(Roles = "admin")] async (
            int id,
            UpdateCustomerRequest request,
            ICustomerService customerService) =>
        {
            
            var updatedCustomer = await customerService.UpdateCustomer(id, request);

            if (updatedCustomer is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(updatedCustomer);
        }).WithTags("Customers");

        app.MapDelete("customers/{id}", [Authorize(Roles = "admin")] async (
            int id,
            ICustomerService customerService) =>
        {
            var deleted = await customerService.DeleteCustomer(id);

            if (deleted is null) return Results.NotFound();

            return Results.NoContent();
        }).WithTags("Customers");
    }
}
