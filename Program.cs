using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Natalia Spindola", Age = 26});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "Natalia Spindola");
    return new {Name = "Natalia Spindola", Age = 26};
});

app.MapPost("/products", (Product product) => {
    ProductRepository.Add(product);
    return Results.Created($"/products/{product.Code}", product.Code);
});

//api.app.com/users/{code} -> passando informações pela rota
app.MapGet("/products/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    if(product != null)
        return Results.Ok(product);
    return Results.NotFound();
});

app.MapPut("/products", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    return Results.Ok();
});


// //api.app.com/users?datastart={date}&dataend={date} -> PASSANDO INFORMAÇÕES PELO QUERY, ENDPOINTS, URL
// app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) => {
//     return dateStart + " - " + dateEnd;
// });

// //PASSANDO INFORMAÇÕES PELO HEADER
// app.MapGet("/getproductbyheader", (HttpRequest request) => {
//     return request.Headers["product-code"].ToString();
// });

app.Run();

public static class ProductRepository {
    public static List<Product> Products { get; set; }

    public static void Add(Product product) {
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code) {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove (Product product) {
        Products.Remove(product);
    }
}

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}