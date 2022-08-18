global using studentmanagement_webapi.Data;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using studentmanagement_webapi;
using studentmanagement_webapi.Helpers;
using studentmanagement_webapi.Authorization;
using studentmanagement_webapi.Services;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;
    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    // Add services to the container.
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();

    var app = builder.Build();
    {
        // global cors policy
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        builder.Services.AddControllers();
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseMiddleware<JwtMiddleware>();

        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dataContext.SaveChanges();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

