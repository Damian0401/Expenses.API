using API.Extensions;
using Application.Extensions;
using Application.Infrastructure;
using Application.Middleware;
using Domain.Models.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Seeds;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddServices();

// Add and congfigure identity
builder.Services.AddSecurity(configuration);

// Add controllers
builder.Services.AddControllers();

builder.Services.AddFluentValidation();

builder.Services.AddValidators();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add cors
var origins = configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
var methods = configuration.GetSection("CorsSettings:AllowedMethods").Get<string[]>();
builder.Services.AddCors(options => 
{
    options.AddPolicy("CorsPolicy", opt =>
        opt.AllowAnyHeader()
            .WithMethods(methods)
            .WithOrigins(origins));
});

// Configure database conntection
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

// Add middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();

// Add automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Add JsonEnum converter
builder.Services.AddMvc(option => option.EnableEndpointRouting = false).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Seed database
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var context = services.GetRequiredService<DataContext>();

var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

await Seed.SeedData(context, roleManager, userManager);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

var cultureInfo = new CultureInfo("en-US");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.Run();
