﻿// <auto-generated />
using AboutUs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AboutUs.Migrations.Profile
{
    [DbContext(typeof(ProfileContext))]
    partial class ProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("AboutUs.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("About")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("BgImg")
                        .HasColumnType("TEXT");

                    b.Property<string>("Likes")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Place")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("TEXT");

                    b.Property<string>("Qualifications")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("AboutUs.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Business")
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
