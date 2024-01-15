using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.Services.IServices;
using Sohag__Mills_Company.Services.map;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ECcontext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) );






//To Inject Any Type inside  this repo  with reflection .............................
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<StatementRepo>();
builder.Services.AddScoped<Indicators_DetailsRepo>();
builder.Services.AddScoped<IPhonesRepo,PhonesRepo>();
builder.Services.AddScoped<IRegionRepo,RegionRepo>();
builder.Services.AddScoped<IFilePdf,FilePdfServices>();
builder.Services.AddScoped<MapObject>();


//For Identity login , add user .....
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ECcontext>();







//builder.Services.AddScoped<IRepository<Statement>, StatementRepo>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

//endpoints.MapControllerRoute(
//    name: "dashbord",
//    pattern: "{controller=Dashbord}/{action=Index}/{id?}");



//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Dashbord}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        //pattern: "{controller=Home}/{action=Index}/{id?}");
//        pattern: "{controller=Dashbord}/{action=Index}/{id?}");
//});


app.Run();
