using Microsoft.EntityFrameworkCore;
using NotesApp.Entities;
using NotesApp.Repositories;
using NotesApp.RepositoryContracts;
using NotesApp.ServiceContracts;
using NotesApp.Services;

namespace NotesApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Services
        builder.Services.AddControllers();

        builder.Services.AddScoped<ISubjectsRepository, SubjectsRepository>();
        
        //add DbContext as service
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        var app = builder.Build();

        // Check database connection
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                var canConnect = dbContext.Database.CanConnect();
                if (canConnect)
                {
                    app.Logger.LogInformation("Successfully connected to the database.");
                }
                else
                {
                    app.Logger.LogError("Failed to connect to the database.");
                }
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "An error occurred while trying to connect to the database.");
            }
        }
        
        app.UseRouting();
        app.MapControllers();
        
        app.Run();
    }
}