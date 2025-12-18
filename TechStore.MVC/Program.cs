using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TechStore.BLL.Concrete;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete;
using TechStore.DALEF.Concrete.ctx;

var builder = WebApplication.CreateBuilder(args);

// 1. ÁÄ
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TechStoreDbContext>(options => options.UseSqlServer(connectionString));

// 2. AutoMapper
builder.Services.AddAutoMapper(typeof(TechStore.DALEF.Concrete.ProductDALEF).Assembly);

// 3. Ðåºñòðàö³ÿ DAL
builder.Services.AddScoped<IUserDAL>(p => new UserDALEF(connectionString, p.GetRequiredService<AutoMapper.IMapper>()));
builder.Services.AddScoped<IProductDAL>(p => new ProductDALEF(connectionString, p.GetRequiredService<AutoMapper.IMapper>()));
builder.Services.AddScoped<ICategoryDAL>(p => new CategoryDALEF(connectionString, p.GetRequiredService<AutoMapper.IMapper>()));
builder.Services.AddScoped<ICartDAL>(p => new CartDALEF(connectionString, p.GetRequiredService<AutoMapper.IMapper>()));
builder.Services.AddScoped<ICartItemsDAL>(p => new CartItemsDALEF(connectionString, p.GetRequiredService<AutoMapper.IMapper>()));
builder.Services.AddScoped<IOrderDAL>(p => new OrderDALEF(connectionString, p.GetRequiredService<AutoMapper.IMapper>()));

// 4. Ðåºñòðàö³ÿ BLL
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();

// 5. Àóòåíòèô³êàö³ÿ
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();