using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudyLastCore2.Repositories;
using StudyLastCore2.Helpers;
using StudyLastCore2.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthentication(options => {
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("StoreConnection");

builder.Services.AddTransient<RepositoryStore>();
builder.Services.AddSingleton<HelperCryptography>();
builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddSingleton<HelperUploadFiles>();

builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
};

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseMvc(routes => {
    routes.MapRoute(
        name: "default",
        template: "{controller=Store}/{action=Index}/{id?}");
});

app.Run();

