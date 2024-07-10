using Microsoft.EntityFrameworkCore;
using 学生干部考评管理系统模型.Enity;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统数据库映射
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StudentCadreInfo> StudentCadreInfos { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<OperationLog> OperationLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 用户表配置
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.Avatar).HasColumnType("BLOB"); 
            });

            // 学生干部信息表配置
            modelBuilder.Entity<StudentCadreInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });

            // 评价表配置
            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<StudentCadreInfo>()
                    .WithMany()
                    .HasForeignKey(e => e.StudentCadreId);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.EvaluatorId);
            });

            // 公告表配置
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // 操作日志表配置
            modelBuilder.Entity<OperationLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });
        }
    }
}
