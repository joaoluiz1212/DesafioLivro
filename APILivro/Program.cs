using APILivro.Repository;
using APILivro.Service;
using AspNetCore.Scalar;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Context>(opts =>
opts.UseSqlServer(builder.Configuration.GetConnectionString("livroConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAPIClient, APIService>();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(8080);
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
    app.UseScalar(options =>
    {
        options.UseTheme(Theme.Default);
        options.RoutePrefix = "api-docs";

    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Context>();
        context.Database.Migrate(); // Aplica as migrations automaticamente
        Console.WriteLine("Banco de dados migrado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao migrar banco de dados: {ex.Message}");
    }
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
