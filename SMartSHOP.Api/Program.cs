var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/test", () =>
{
    return Results.Ok(new { message = "Sklep SMart SHOP działa poprawnie!" });
})
.WithName("GetTest")
.WithOpenApi();

app.Run();