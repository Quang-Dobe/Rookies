using ECommerce.CustomerSite.Services;
using ECommerce.CustomerSite.Services.Interface;
using Microsoft.Extensions.FileProviders;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session storage
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
});

// Add IHttpClientFactory and put it in Dependency injection then use it to call api as a client
builder.Services.AddHttpClient("", opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? "");
});

// Add RefitClient (Instead of using normal HttpClientFactory, we use Refit to call API (Follow Restful API)
//builder.Services.AddRefitClient<IProduct>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? ""));

// Add Services to DI Container
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIdentityUserService, IdentityUserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

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
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
