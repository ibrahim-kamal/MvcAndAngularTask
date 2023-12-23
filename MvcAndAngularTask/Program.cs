using Microsoft.EntityFrameworkCore;
using MvcAndAngularTask.DataAcessLayer;
using MvcAndAngularTask.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DoctorDAL>();
builder.Services.AddScoped<ReservationDAL>();
builder.Services.AddScoped<ScheduleDAL>();
builder.Services.AddScoped<PatientDAL>();
builder.Services.AddScoped<AppoinmentsDAL>();
builder.Services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("local"));
            });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;



app.Run();
