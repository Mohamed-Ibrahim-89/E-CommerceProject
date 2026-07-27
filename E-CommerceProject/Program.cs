using E_CommerceProject.Repositories.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUploadFile, UploadFile>();

// Add Connection Strings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add NToastNotify for notifications
builder.Services.AddMvc().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopRight,
    PreventDuplicates = true,
    CloseButton = true
});

// Add configuration for identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    //options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


// Session for cart
builder.Services.AddScoped<ICartRepository, CartRepository>(sp => CartRepository.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Register for WishListRepository
builder.Services.AddScoped<IWishListRepository, WishListRepository>();

// Register for CacheService
builder.Services.AddCacheService();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
