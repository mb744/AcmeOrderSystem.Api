using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Endpoints;
using AcmeOrderSystem.Api.Extensions;
using AcmeOrderSystem.Api.Services;
using AcmeOrderSystem.Api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme. First use Login with username:acme password:acme123 and copy token, its the value in between the double quotes. Enter Bearer [space] add your token in the text input. Example: Bearer shskashdjs",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddLogging();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidSecretKey"))),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidIssuer"),

        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidAudiences")
    };
});



builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IValidator<Login>, LoginValidator>();

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));



var app = builder.Build();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}



app.MapCustomerEndpoints();
app.MapLoginEndpoint();
app.MapContactEndpoints();
app.MapOrderEndpoints();

app.UseExceptionHandler(app =>
{
    app.Run(async context =>
    {
        await Results.Problem().ExecuteAsync(context);
    });
});

app.Run();

public partial class Program { }
