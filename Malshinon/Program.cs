using Microsoft.EntityFrameworkCore;
using System;
using Malshinon;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://*:80");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//חיבור הדאטה בייס
builder.Services.AddDbContext<MalshinonDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));



//חיבור לדאטה בדוקר
//builder.Services.AddDbContext<MalshinonDbContext>(options =>
//    options.UseMySQL("server=mysql;user=intel_user;password=intelpass;database=Malshinon;"));



var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



