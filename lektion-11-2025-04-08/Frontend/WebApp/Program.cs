using Data.Extensions;
using Business.Extensions;
using Business.Handlers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();



//if (builder.Environment.IsDevelopment())
//{
//    var localPath = Path.Combine(builder.Environment.WebRootPath, "images", "uploads");
//    builder.Services.AddScoped<IImageHandler>(_ => new LocalImageHandler(localPath));
//}
//else
//{
//    builder.Services.AddScoped<IImageHandler, AzureImageHandler>();
//}

builder.Services.AddScoped<IImageHandler, AzureImageHandler>();


builder.Services.AddContexts(builder.Configuration.GetConnectionString("SqlConnection")!);
builder.Services.AddLocalIdentity();
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Projects}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
