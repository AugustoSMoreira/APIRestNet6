using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

var app = builder.Build();
var configuration = app.Configuration;
ProductRepository.Init(configuration);


// EndPoint Post para salvar dados
app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) =>
{
    var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
    var product = new Product
    {
        Code = productRequest.Code,
        Mark = productRequest.Mark,
        Amount = productRequest.Amount,
        Category = category

    };
    if (productRequest.Tags != null)
    {
        product.Tags = new List<Tag>();
        foreach (var item in productRequest.Tags)
        {
            product.Tags.Add(new Tag { Name = item });
        }
    }
    context.Products.Add(product);
    context.SaveChanges();
    return Results.Created($"/products/{product.Id}", product.Id);
});

// EndPoint com valores por Rota para chamar os produtos
app.MapGet("/products/{id}", ([FromRouteAttribute] int id, ApplicationDbContext context) =>
{
    var product = context.Products
    .Include(p => p.Category)
    //.Include(p => p.Tags)
    .Where(p => p.Id == id).FirstOrDefault();
    if (product != null)
    {
        return Results.Ok(product);
    }
    return Results.NotFound();
});

// EP para editar o produto
app.MapPut("/products/{id}", ([FromRouteAttribute] int id, ProductRequest productRequest, ApplicationDbContext context) =>
{
    var product = context.Products
     .Include(p => p.Category)
     .Include(p => p.Tags)
     .Where(p => p.Id == id).First();

    var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
    product.Code = productRequest.Code;
    product.Mark = productRequest.Mark;
    product.Amount = productRequest.Amount;
    product.Category = category;
    product.Tags = new List<Tag>();
    if (productRequest.Tags != null)
    {
        product.Tags = new List<Tag>();
        foreach (var item in productRequest.Tags)
        {
            product.Tags.Add(new Tag { Name = item });
        }
    }
    context.SaveChanges();
    return Results.Ok();
});

//EP para Deletar os produtos
app.MapDelete("/products/{id}", ([FromRouteAttribute] int id, ApplicationDbContext context) =>
{
    var product = context.Products.Where(p => p.Id == id).First();
    context.Products.Remove(product);
    context.SaveChanges();
    return Results.Ok();
});

// EP para configurations que retorna info da conexão db
app.MapGet("/configuration/database", (IConfiguration configuration) =>
{
    return Results.Ok($"{configuration["database:SqlServer"]}");
});

app.Run();
