using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DigitalWallet.Aplication.interfaces;
using DigitalWallet.Aplication.service;
using DigitalWallet.Domain.repository;
using DigitalWallet.Infrastructure.context;
using DigitalWallet.Infrastructure.repository;
using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Aplication.Validator;
using FluentValidation;
using DigitalWallet.Infrastructure.data;
using System;
using DigitalWallet.Domain.service;


var builder = WebApplication.CreateBuilder(args);

// Registra o AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Adiciona os serviços de controle e documentação
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wallet API",
        Version = "v1"
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddDbContext<DigitalWalletContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
builder.Services.AddScoped<IValidator<CreateWalletRequest>, CreateWalletRequestValidator>();
builder.Services.AddScoped<IValidator<TransferRequest>, TransferRequestValidator>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();



app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DigitalWalletContext>();
    context.Database.EnsureCreated();
    await DatabaseSeeder.SeedAsync(context);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();