using insecure_direct_object_reference_delete_credit_card.Core.Data;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.Account;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.CreditCard;
using insecure_direct_object_reference_delete_credit_card.Infrastructure.Repository.Account;
using insecure_direct_object_reference_delete_credit_card.Infrastructure.Repository.CreditCard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session hizmetini ekleyin
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None; // SameSitse özelliğini None olarak ayarlıyoruz
});

// DI
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();


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