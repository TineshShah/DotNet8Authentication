using DotNet8Authentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
//This is just adding metadata so that when you open Swagger UI,
//you get a little padlock icon and an “Authorize” button. Nothing more exotic.
builder.Services.AddSwaggerGen(options => {

    options.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Your API Title",
        Version = "v1",
        Description = "API description here"
    });
    //This line defines a security scheme named "oauth2" that uses the API key type. 
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    //This line specifies  that the "oauth2" security scheme is required for accessing the API endpoints.
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//TINN_ADDED THESE LINES MANUALLY
//AddDbContext<DataContext> means “register the DataContext and its configuration in the DI container. so it can be injected wherever needed"
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
