using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Puppy_Project;
using Puppy_Project.Dbcontext;
using Puppy_Project.Interfaces;
using Puppy_Project.Models;
using Puppy_Project.Services.Carts;
using Puppy_Project.Services.Categorys;
using Puppy_Project.Services.Orders;
using Puppy_Project.Services.Products;
using Puppy_Project.Services.WishLists;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMaper));
/*builder.Services.AddSingleton<PuppyDb>();
*/
builder.Services.AddDbContext<PuppyDb>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
builder.Services.AddScoped<IUsersService,UsersService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductsService,ProductsService>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IWishListService,WishListService>();

builder.Services.AddControllers();



//-------------------------Authorization & Token------------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
//---------------------------------------------------------------------------



//--------------------React------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
//-------------------------------------------------------

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//---------React----------------------------------
app.UseCors("ReactPolicy");
//-----------------------------------------------
app.UseStaticFiles();


//---------------------Authorization-------------------------
app.UseAuthentication();
app.UseAuthorization();
//-----------------------------------------------------------



app.MapControllers();

app.Run();
