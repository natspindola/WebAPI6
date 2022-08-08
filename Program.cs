var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Natalia Spindola", Age = 26});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "Natalia Spindola");
    return new {Name = "Natalia Spindola", Age = 26};
});

app.MapPost("/saveproduct", (Product product) => {
    return product.Code + " - " + product.Name;
});

app.Run();

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}