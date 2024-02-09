using CHU.Data.Interface;
using CHU.Data.Repositories;
using CHU.Domain.Requests;
using CHU.Infrastructure.Interfaces;
using CHU.Infrastructure.Repositories;
using CHU.Service.Interfaces;
using CHU.Service.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IUtil,Util>();
builder.Services.AddSingleton<IAccountDAO, AccountDAO>();
builder.Services.AddSingleton<ITransferDAO, TransferDAO>();
builder.Services.AddSingleton<IAccountService,AccountService>();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ExtratoRequestValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AccountRequest>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TransferRequest>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
