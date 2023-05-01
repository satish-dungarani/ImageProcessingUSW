using ImageProcessing.Data.DataContext;
using ImageProcessing.Data.Interface;
using ImageProcessing.Services;
using ImageProcessing.Web.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvcCore();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();   
builder.Services.AddDbContext<ImageProcessingDBContext>(options => options.UseSqlServer(builder.Configuration
    .GetConnectionString("IPConectionString")));
 

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

/// DI Registarton  -----------------
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IImageProcessingRepository,ImageProcessingRepository>();
builder.Services.AddScoped<IImageProcessingService, ImageProcessingService>();
/// --------------------------------

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
});


builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
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
app.UseAuthentication();;
app.UseAuthorization();

app.UseMiddleware<ImageProcessingMiddleware>();
app.MapAreaControllerRoute(
            name: "MyAreaAdmin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//   name: "areas",
//      pattern: "{area:}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.MapRazorPages();    

app.Run();
