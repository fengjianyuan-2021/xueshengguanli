﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using 学生干部考评管理系统数据库映射;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EvaluationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("EvaluationType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EvaluatorId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Score")
                        .HasColumnType("REAL");

                    b.Property<int>("StudentCadreId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EvaluatorId");

                    b.HasIndex("StudentCadreId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.OperationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OperationLogs");
                });

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.StudentCadreInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BasicInfo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PeerEvaluation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SelfEvaluation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeacherEvaluation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("TotalScore")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCadreInfos");
                });

            modelBuilder.Entity("学生干部考评管理系统模型.StudentCadreEvaluation.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Avatar")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.Evaluation", b =>
                {
                    b.HasOne("学生干部考评管理系统模型.StudentCadreEvaluation.Models.User", null)
                        .WithMany()
                        .HasForeignKey("EvaluatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("学生干部考评管理系统模型.Enity.StudentCadreInfo", null)
                        .WithMany()
                        .HasForeignKey("StudentCadreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.OperationLog", b =>
                {
                    b.HasOne("学生干部考评管理系统模型.StudentCadreEvaluation.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("学生干部考评管理系统模型.Enity.StudentCadreInfo", b =>
                {
                    b.HasOne("学生干部考评管理系统模型.StudentCadreEvaluation.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
