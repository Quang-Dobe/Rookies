using Microsoft.Extensions.FileProviders;

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
