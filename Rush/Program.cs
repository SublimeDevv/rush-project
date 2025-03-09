using Microsoft.Extensions.FileProviders;
using Rush.Infraestructure;
using Rush.WebAPI;
using Serilog;
using Rush.Application;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true)
    .CreateLogger();
    
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
            "Origin, X-Requested-With, Content-Type, Accept");
    },
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "Uploads")),
    RequestPath = "/Uploads"
});



app.UseHttpsRedirection();

app.UseCors("AllowAllHeaders");

app.UseAuthorization();

app.MapControllers();

app.Run();
