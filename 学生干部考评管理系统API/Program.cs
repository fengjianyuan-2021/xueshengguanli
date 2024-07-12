using Microsoft.EntityFrameworkCore;
using System;
using ѧ���ɲ���������ϵͳAPI.Interface;
using ѧ���ɲ���������ϵͳAPI.Service;
using ѧ���ɲ���������ϵͳ���ݿ�ӳ��;
using Microsoft.EntityFrameworkCore;
using ѧ���ɲ���������ϵͳAPI;
using ѧ���ɲ���������ϵͳģ��.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
// Configure DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ע�� AutoMapper������Ӱ���ӳ�������ļ��ĳ���
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}, typeof(Program).Assembly, typeof(UserDto).Assembly);

builder.Services.AddSwaggerGen();

// ���� CORS ����
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCors(); // ʹ�� CORS �м��

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        // ������ݿ��ļ��Ƿ����
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "dbsqlite");
        // ������ݿ��ļ������ڣ��򴴽����ݿⲢִ��Ǩ��
        context.Database.Migrate();

        // �����ʼ�û�����
        DataSeeder.SeedInitialUser(context, logger);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.Run();
