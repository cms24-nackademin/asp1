using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();






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
