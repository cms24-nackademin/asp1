using Microsoft.AspNetCore.Rewrite;
using Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddContexts(builder.Configuration.GetConnectionString("SqlConnection")!);
builder.Services.AddLocalIdentity(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);


var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/admin/overview"));
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Overview}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
