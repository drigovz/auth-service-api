using AuthService.Application.Notifications;
using AuthService.Infra.IoC.DependencyInjection;
using FluentValidation.AspNetCore;

#region [+] ConfigureServices
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                     .AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddScoped<NotificationContext>();
builder.Services.AddControllers()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<NotificationContext>());
#endregion

#region [+] Configure
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var env = app.Environment;
if (app.Environment.IsDevelopment() || env.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
#endregion
