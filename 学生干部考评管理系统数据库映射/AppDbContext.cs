using Microsoft.EntityFrameworkCore;
using 学生干部考评管理系统模型.Enity;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统数据库映射
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<OperationLog> OperationLogs { get; set; }
        public DbSet<AnnouncementReadStatus> AnnouncementReadStatuses { get; set; }

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

                entity.HasMany(u => u.SelfEvaluations)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.OtherEvaluations)
                .WithOne(e => e.StudentCadreInfo)
                .HasForeignKey(e => e.StudentCadreId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // 评价表配置
            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.HasKey(e => e.Id);
                //entity.HasOne<User>()
                //    .WithMany()
                //    .HasForeignKey(e => e.StudentCadreId);
                //entity.HasOne<User>()
                //    .WithMany()
                //    .HasForeignKey(e => e.EvaluatorId);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.SelfEvaluations)
                    .HasForeignKey(e => e.EvaluatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.StudentCadreInfo)
                    .WithMany(u => u.OtherEvaluations)
                    .HasForeignKey(e => e.StudentCadreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 公告表配置
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            //通知管理
            modelBuilder.Entity<AnnouncementReadStatus>()
                .HasKey(ars => ars.Id);

            modelBuilder.Entity<AnnouncementReadStatus>()
                .HasOne(ars => ars.Announcement)
                .WithMany(a => a.ReadStatuses)
                .HasForeignKey(ars => ars.AnnouncementId);

            modelBuilder.Entity<AnnouncementReadStatus>()
                .HasOne(ars => ars.User)
                .WithMany(u => u.ReadAnnouncements)
                .HasForeignKey(ars => ars.UserId);

            // 操作日志表配置
            modelBuilder.Entity<OperationLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }



        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreateOn = DateTime.UtcNow;
                }

                entity.UpdateOn = DateTime.UtcNow;
            }
        }
    }
}
