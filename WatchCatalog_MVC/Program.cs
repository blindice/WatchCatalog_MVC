using Microsoft.AspNetCore.Mvc;
using WatchCatalog_MVC.Filters;
using WatchCatalog_MVC.Interfaces;
using WatchCatalog_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var apiBaseAddress = builder.Configuration.GetValue<string>("Connection:API");

builder.Services.AddHttpClient("WatchClient", config =>
{
    config.BaseAddress = new Uri(apiBaseAddress);
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddMvc( builder => builder.Filters.Add(new GlobalExceptionFilter()));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Watch}/{action=Index}/{id?}");

app.Run();
