using Infraestructure.Core.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N5Now.Domain.Mappins;
using N5Now.Handlers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console();
    configuration.MinimumLevel.Warning();
    configuration.WriteTo.File("Logs/LogChallenguer_.txt", rollingInterval: RollingInterval.Day);

});

#region SQL Server Connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringSQLServer"));
    options.EnableSensitiveDataLogging();
});
#endregion

#region Inyection
DependencyInyectionHandler.DependencyInyectionConfig(builder.Services);
#endregion

#region ConfigurationsAutomapper
builder.Services.AddAutoMapper(typeof(SettingAutomapper));
#endregion

#region CustimValidationFilterAttribute
builder.Services.Configure<ApiBehaviorOptions>(options
=> options.SuppressModelStateInvalidFilter = true);
#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

