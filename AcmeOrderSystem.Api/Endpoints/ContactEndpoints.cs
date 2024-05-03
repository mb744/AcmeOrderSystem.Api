using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using AcmeOrderSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace AcmeOrderSystem.Api.Endpoints;

public static class ContactEndpoints
{
    public static void MapContactEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("contacts", [Authorize(Roles = "admin")] async (
            CreateContactRequest request,
            IContactService contactService) =>
        {
            
            var contact = await contactService.AddContact(request);

            if (contact is null) 
            { 
                return Results.NoContent();
            }
            
            return Results.Ok(contact);
        }).WithTags("Contacts");

        app.MapGet("contacts", [Authorize(Roles = "admin")] async (
            IContactService contactService) =>
        {
            var contacts = await contactService.GetAllContacts();
                
            return Results.Ok(contacts);
        }).WithTags("Contacts");

        app.MapGet("contacts/{id}", [Authorize(Roles = "admin")] async (
            int id,
            IContactService contactService) =>
        {
            var contact = await contactService.GetSingleContact(id);

            return contact is null ? Results.NotFound() : Results.Ok(contact);
        }).WithTags("Contacts");


        app.MapDelete("contacts/{id}", [Authorize(Roles = "admin")] async (
            int id,
            IContactService contactService) =>
        {
            var deleted = await contactService.DeleteContact(id);

            if (deleted is null) return Results.NotFound();

            return Results.NoContent();
        }).WithTags("Contacts");

        app.MapPut("contacts/{id}", [Authorize(Roles = "admin")] async (
            int id,
            UpdateContactRequest request,
            IContactService contactService) =>
        {
            var updated = await contactService.UpdateContact(id, request);

            if (updated is null) return Results.NotFound();

            return Results.Ok(updated);
        }).WithTags("Contacts");
    }
}
