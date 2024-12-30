using Microsoft.EntityFrameworkCore;
using PVMS_Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PassportDBContext>( 
    option=>option.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    ); 
    builder.Services.AddHttpContextAccessor();
    const string policyName = "CorsPolicy"; 
    builder.Services.AddCors(options => 
    { 
        options.AddPolicy(name: policyName, builder => 
        { 
            builder.AllowAnyOrigin() 
                .AllowAnyHeader() 
                .AllowAnyMethod();
            });
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

app.UseRouting();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "API Default", 
    pattern: "api/{controller}/{action}/{id?}");

app.Run();
