using cross_site_request_forgery_password_change.Core.Data;
using cross_site_request_forgery_password_change.Core.Interface.Repository.Account;
using cross_site_request_forgery_password_change.Infrastructure.Repository.Account;

var builder = WebApplication.CreateBuilder(args);

// Session hizmetini ekleyin
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None; // SameSite özelliğini None olarak ayarlıyoruz
});

// DI
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


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

app.UseSession(); // Session kullanımı için gerekli

app.UseAuthorization();


app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();