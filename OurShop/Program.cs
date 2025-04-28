using Microsoft.EntityFrameworkCore;
using Repository;
using Services;
using NLog.Web;
using OurShop;
using PresidentsApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();

builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingServices, RatingServices>();


builder.Services.AddDbContext<AdoNetManageContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

// Configure the HTTP request pipeline.
app.UseErrorHandlingMiddleware();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseMiddlewareRating();

app.MapControllers(); 

app.Run();
