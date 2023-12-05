﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WeSyncBackend.Migrations
{
    [DbContext(typeof(FisierDb))]
    [Migration("20231205140047_refactor dbdriver")]
    partial class refactordbdriver
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Fisier", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("content")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("size")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("Fisiers");
                });
#pragma warning restore 612, 618
        }
    }
}