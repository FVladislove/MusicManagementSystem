using Microsoft.EntityFrameworkCore;
using MusicManagementSystem.Data;
using MusicManagementSystem.Models.NotMapped;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Configuration.AddJsonFile(
    new MusicManagementSystem.Services.GoogleCloudKMSEncryptedFileProvider(),
    "appsecrets.json.encrypted",
    optional: true, reloadOnChange: false);
builder.Services.Configure<AppSecretsModel>(
    builder.Configuration.GetSection("Secrets"));

builder.Services.AddDbContext<MusicManagemetSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("Secrets").GetConnectionString("MusicManagementSystemContext")
    ?? throw new InvalidOperationException("Connection string 'MusicManagementSystemContext' not found.")));


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
