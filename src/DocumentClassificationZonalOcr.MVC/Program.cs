using DocumentClassificationZonalOcr.MVC.Clients;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.MVC.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleWare>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("ApiClient", client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("Integrations:Api:BaseUrl");
    client.BaseAddress = new Uri(apiUrl);
});
builder.Services.AddScoped<IFormClient, FormClient>();
builder.Services.AddScoped<IFormSampleClient, FormSampleClient>();
builder.Services.AddScoped<IPaperClient, PaperClient>();
builder.Services.AddScoped<IFieldClient, FieldClient>();
builder.Services.AddScoped<IFormDetectionSettingClient, FormDetectionSettingClient>();
builder.Services.AddSession(options =>
{

});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Form}/{action=Index}/{id?}");

app.Run();
