using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MusicManagementSystem.Data;
using MusicManagementSystem.Models.NotMapped.SecretsModels;
using MusicManagementSystem.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Configuration.AddJsonFile(
    new GoogleCloudKMSEncryptedFileProvider(),
    "appsecrets.json.encrypted",
    optional: true, reloadOnChange: false);

// Add secrets configurations
// TODO think about changing this code to add manually with class bindings
//var secretsModelsInstances = from t in Assembly.GetExecutingAssembly().GetTypes()
//                             where t.GetInterfaces().Contains(typeof(ISecretsModel))
//                             where t.GetInterfaces().Contains(typeof(IStaticType))
//                             && t.GetConstructor(Type.EmptyTypes) != null
//                             select Activator.CreateInstance(t) as ISecretsModel;

//foreach (var secretModel in secretsModelsInstances)
//{
//    var sectionName = secretModel.SectionName;
//    var secretModelWithGetStaticType = secretModel as IStaticType;
//    if (secretModelWithGetStaticType != null)
//    {
//        var bindedGoogleAuth = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
//        var baseConfigMethod = typeof(IServiceCollection).GetMethod("Configure", new Type[] {secretModelWithGetStaticType.GetStaticType});//, new Type[] { secretModelWithGetStaticType.GetStaticType });
//        if (baseConfigMethod != null)
//        {
//            var resultMethod = baseConfigMethod.MakeGenericMethod(secretModelWithGetStaticType.GetStaticType);
//            resultMethod.Invoke(null, new[] { builder.Configuration.GetSection(sectionName) });
//        }
//    }
//}

//TODO think about model creating and binding
// =================================================================================================================================
var connectionStringModel = new ConnectionStringsModel();
var connectionStringModelBinded = builder.Configuration.GetSection(connectionStringModel.SectionName).Get<ConnectionStringsModel>();
var connectionString = connectionStringModelBinded.MusicManagementSystemContext
    ?? throw new InvalidOperationException("Connection string 'MusicManagementSystemContext' not found.");

var sendGridModel = new SendGridModel();
builder.Services.Configure<SendGridModel>(
    builder.Configuration.GetSection(sendGridModel.SectionName));

var googleAuthModel = new GoogleAuthModel();
builder.Services.Configure<GoogleAuthModel>(
    builder.Configuration.GetSection(googleAuthModel.SectionName));
// =================================================================================================================================

//var connectionString = builder.Configuration
//.GetConnectionString("MusicManagementSystemContext")
//    // or
//    //.GetSection(ConnectionStringsModel.SectionName)["MusicManagementSystemContext"]
//    ?? throw new InvalidOperationException("Connection string 'MusicManagementSystemContext' not found.");

builder.Services.AddDbContext<MusicManagemetSystemDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<MusicManagemetSystemDbContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration.GetSection(googleAuthModel.SectionName)["ClientId"];
    googleOptions.ClientSecret = builder.Configuration.GetSection(googleAuthModel.SectionName)["ClientSecret"];
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
