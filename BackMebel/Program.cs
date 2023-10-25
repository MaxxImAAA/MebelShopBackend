using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.DAL.Realization;
using BackMebel.Service.Dtos.UserDtos;
using BackMebel.Service.IService;
using BackMebel.Service.Service;
using BackMebel.Service.Tools.FluentValidation;
using BackMebel.Service.Tools.TokenFolder;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signingKey,
        ValidateIssuerSigningKey = true,
        
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<UserRegisterForm>, UserRegistrationValidation>();
builder.Services.AddAutoMapper(typeof(BackMebel.Service.Tools.AutoMapper.Mapper).Assembly);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyString")));
builder.Services.AddScoped<IProductInterface, ProductRealization>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserAuthInterface, UserAuthRealization>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<ICartInterface, CartRealization>();
builder.Services.AddScoped<ICartProductInterface, CartProductRealization>();
builder.Services.AddScoped<IUserInterface, UserRealization>();
builder.Services.AddScoped<IOrderInterface, OrderRealization>();
builder.Services.AddScoped<IOrderProductInterface, OrderProductRealization>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IFeedbackInterface, FeedbackRealization>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddScoped<IMessageInterface, MessageRealization>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<TokenGetInfoInterface, TokenGetInfo>();

builder.Services.AddScoped<ITokenInterface, TokenRealization>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
