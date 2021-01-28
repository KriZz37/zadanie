﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zadanie.Models;

namespace zadanie.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210125174513_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("zadanie.Entities.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("FormId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FormId")
                        .IsUnique()
                        .HasFilter("[FormId] IS NOT NULL");

                    b.HasIndex("ParentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("zadanie.Entities.Form", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("zadanie.Entities.File", b =>
                {
                    b.HasOne("zadanie.Entities.Form", "Form")
                        .WithOne("File")
                        .HasForeignKey("zadanie.Entities.File", "FormId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("zadanie.Entities.File", "Parent")
                        .WithMany("Subfiles")
                        .HasForeignKey("ParentId");

                    b.Navigation("Form");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("zadanie.Entities.File", b =>
                {
                    b.Navigation("Subfiles");
                });

            modelBuilder.Entity("zadanie.Entities.Form", b =>
                {
                    b.Navigation("File");
                });
#pragma warning restore 612, 618
        }
    }
}
