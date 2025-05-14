using TechnoparkProj.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
//using TechnoparkProj.DataAccess.Repositories;
//using TechnoparkProj.Application.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connection = builder.Configuration.GetConnectionString("TechnoparkProjDbContext");
IServiceCollection serv_col = builder.Services.AddDbContext<TechnoparkProjDbContext>(options => options.UseSqlServer(connection));

//builder.Services.AddScoped<IProjectService, ProjectService>();
//builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // URL вашего Vite приложения
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

//app.UseCors(builder => builder
//    .WithOrigins("http://localhost:5173")
//    .AllowAnyMethod()
//    .AllowAnyHeader());

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TechnoparkProjDbContext>();
        DbItitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
