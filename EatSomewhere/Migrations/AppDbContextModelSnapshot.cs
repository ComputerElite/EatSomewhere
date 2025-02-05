﻿// <auto-generated />
using System;
using EatSomewhere.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EatSomewhere.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("EatSomewhere.Data.Assembly", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Assemblies");
                });

            modelBuilder.Entity("EatSomewhere.Data.Food", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rezept")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("EatSomewhere.Data.FoodEntry", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Cost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CostPerPerson")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("FoodId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PayedById")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("PayedById");

                    b.ToTable("FoodEntry");
                });

            modelBuilder.Entity("EatSomewhere.Data.FoodParticipant", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FoodEntryId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FoodEntryId");

                    b.ToTable("FoodParticipant");
                });

            modelBuilder.Entity("EatSomewhere.Data.Ingredient", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId");

                    b.HasIndex("UserId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("EatSomewhere.Data.IngredientEntry", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("FoodId")
                        .HasColumnType("TEXT");

                    b.Property<string>("IngredientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Unit")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientEntry");
                });

            modelBuilder.Entity("EatSomewhere.Data.Tag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FoodId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId");

                    b.HasIndex("FoodId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("EatSomewhere.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyId1")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId");

                    b.HasIndex("AssemblyId1");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EatSomewhere.Users.UserSession", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastAccess")
                        .HasColumnType("TEXT");

                    b.Property<string>("Origin")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidUnti")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("EatSomewhere.Data.Food", b =>
                {
                    b.HasOne("EatSomewhere.Data.Assembly", "Assembly")
                        .WithMany()
                        .HasForeignKey("AssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assembly");
                });

            modelBuilder.Entity("EatSomewhere.Data.FoodEntry", b =>
                {
                    b.HasOne("EatSomewhere.Data.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EatSomewhere.Users.User", "PayedBy")
                        .WithMany()
                        .HasForeignKey("PayedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("PayedBy");
                });

            modelBuilder.Entity("EatSomewhere.Data.FoodParticipant", b =>
                {
                    b.HasOne("EatSomewhere.Data.FoodEntry", null)
                        .WithMany("Participants")
                        .HasForeignKey("FoodEntryId");
                });

            modelBuilder.Entity("EatSomewhere.Data.Ingredient", b =>
                {
                    b.HasOne("EatSomewhere.Data.Assembly", "Assembly")
                        .WithMany()
                        .HasForeignKey("AssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EatSomewhere.Users.User", null)
                        .WithMany("Intolerances")
                        .HasForeignKey("UserId");

                    b.Navigation("Assembly");
                });

            modelBuilder.Entity("EatSomewhere.Data.IngredientEntry", b =>
                {
                    b.HasOne("EatSomewhere.Data.Food", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodId");

                    b.HasOne("EatSomewhere.Data.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("EatSomewhere.Data.Tag", b =>
                {
                    b.HasOne("EatSomewhere.Data.Assembly", "Assembly")
                        .WithMany()
                        .HasForeignKey("AssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EatSomewhere.Data.Food", null)
                        .WithMany("Tags")
                        .HasForeignKey("FoodId");

                    b.Navigation("Assembly");
                });

            modelBuilder.Entity("EatSomewhere.Users.User", b =>
                {
                    b.HasOne("EatSomewhere.Data.Assembly", null)
                        .WithMany("Admins")
                        .HasForeignKey("AssemblyId");

                    b.HasOne("EatSomewhere.Data.Assembly", null)
                        .WithMany("Users")
                        .HasForeignKey("AssemblyId1");
                });

            modelBuilder.Entity("EatSomewhere.Data.Assembly", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EatSomewhere.Data.Food", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("EatSomewhere.Data.FoodEntry", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("EatSomewhere.Users.User", b =>
                {
                    b.Navigation("Intolerances");
                });
#pragma warning restore 612, 618
        }
    }
}
