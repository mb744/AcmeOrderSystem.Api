using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using AcmeOrderSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace AcmeOrderSystem.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("orders", [Authorize(Roles = "admin")] async (
            CreateOrderRequest request,
            IOrderService orderService) =>
        {
            
            var order = await orderService.AddOrder(request);

            if (order is null) 
            { 
                return Results.NoContent();
            }
            
            return Results.Ok(order);
        }).WithTags("Orders");

        app.MapGet("orders", [Authorize(Roles = "admin")] async (
            IOrderService orderService) =>
        {
            var orders = await orderService.GetAllOrders();
                
            return Results.Ok(orders);
        }).WithTags("Orders");

        app.MapGet("orders/{id}", [Authorize(Roles = "admin")] async (
            int id,
            IOrderService orderService) =>
        {
            var order = await orderService.GetSingleOrder(id);

            return order is null ? Results.NotFound() : Results.Ok(order);
        }).WithTags("Orders");


        app.MapDelete("orders/{id}", [Authorize(Roles = "admin")] async (
            int id,
            IOrderService orderService) =>
        {
            var deleted = await orderService.DeleteOrder(id);

            if (deleted is null) return Results.NotFound();

            return Results.NoContent();
        }).WithTags("Orders");

        app.MapPut("orders/{id}", [Authorize(Roles = "admin")] async (
            int id,
            UpdateOrderRequest request,
            IOrderService orderService) =>
        {
            var updated = await orderService.UpdateOrder(id, request);

            if (updated is null) return Results.NotFound();

            return Results.Ok(updated);
        }).WithTags("Orders");
    }
}
