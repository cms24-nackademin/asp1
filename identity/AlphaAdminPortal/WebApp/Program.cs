using Business.Services;
using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddIdentity<AppUserEntity, IdentityRole>(x => 
    {
        x.User.RequireUniqueEmail = true;
        x.Password.RequiredLength = 8;
        x.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x => 
    {
        x.LoginPath = "/signin";
        x.LogoutPath = "/";
        x.AccessDeniedPath = "/denied";
        x.SlidingExpiration = true;
        x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignIn}/{id?}")
    .WithStaticAssets();


app.Run();
