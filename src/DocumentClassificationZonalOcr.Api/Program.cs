using DocumentClassificationZonalOcr.Api;
using DocumentClassificationZonalOcr.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleWare>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddProjectDependencies(builder.Configuration);
builder.Host.UseSerilog((context, loggerConfig) =>
        loggerConfig.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.MapControllers();
app.Run();
