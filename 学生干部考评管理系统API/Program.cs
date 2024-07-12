using Microsoft.EntityFrameworkCore;
using System;
using 学生干部考评管理系统API.Interface;
using 学生干部考评管理系统API.Service;
using 学生干部考评管理系统数据库映射;
using Microsoft.EntityFrameworkCore;
using 学生干部考评管理系统API;
using 学生干部考评管理系统模型.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
// Configure DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 注册 AutoMapper，并添加包含映射配置文件的程序集
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}, typeof(Program).Assembly, typeof(UserDto).Assembly);

builder.Services.AddSwaggerGen();

// 配置 CORS 策略
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
app.UseCors(); // 使用 CORS 中间件

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

        // 检查数据库文件是否存在
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "dbsqlite");
        // 如果数据库文件不存在，则创建数据库并执行迁移
        context.Database.Migrate();

        // 插入初始用户数据
        DataSeeder.SeedInitialUser(context, logger);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.Run();
