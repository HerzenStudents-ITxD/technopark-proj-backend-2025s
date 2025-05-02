using TechnoparkProj.DataAccess;
using Microsoft.EntityFrameworkCore;
//using TechnoparkProj.DataAccess.Repositories;
//using TechnoparkProj.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connection = builder.Configuration.GetConnectionString("TechnoparkProjDbContext");
IServiceCollection serv_col = builder.Services.AddDbContext<TechnoparkProjDbContext>(options => options.UseSqlServer(connection));

//builder.Services.AddScoped<IProjectService, ProjectService>();
//builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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
