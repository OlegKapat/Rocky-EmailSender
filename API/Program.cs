using API.Utility;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(Options=>Options.IdleTimeout=TimeSpan.FromMinutes(30));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddDbContext<ApplicationDbContext>( options =>
{
  options.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]);
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddUserStore<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
