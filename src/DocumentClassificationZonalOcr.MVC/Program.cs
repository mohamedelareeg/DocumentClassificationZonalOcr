using DocumentClassificationZonalOcr.MVC.Clients;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("ApiClient", client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("Integrations:Api:BaseUrl");
    client.BaseAddress = new Uri(apiUrl);
});
builder.Services.AddScoped<IFormClient, FormClient>();
builder.Services.AddScoped<IFormSampleClient, FormSampleClient>();
builder.Services.AddSession(options =>
{
    // Configure session options here, if needed
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Form}/{action=Index}/{id?}");

app.Run();
