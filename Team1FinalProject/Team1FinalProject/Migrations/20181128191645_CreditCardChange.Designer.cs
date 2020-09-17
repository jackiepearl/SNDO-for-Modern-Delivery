﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Team1FinalProject.DAL;

namespace Team1FinalProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181128191645_CreditCardChange")]
    partial class CreditCardChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Team1FinalProject.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("AccountActive");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreditCard1");

                    b.Property<string>("CreditCard2");

                    b.Property<string>("CreditCard3");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("StAddress");

                    b.Property<string>("State");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ActiveSell");

                    b.Property<string>("Author");

                    b.Property<decimal>("Cost");

                    b.Property<string>("Description");

                    b.Property<int?>("GenreID");

                    b.Property<int>("Inventory");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("PublishDate");

                    b.Property<int>("ReorderLevel");

                    b.Property<string>("Title");

                    b.Property<int>("UniqueNum");

                    b.HasKey("BookID");

                    b.HasIndex("GenreID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Coupon", b =>
                {
                    b.Property<string>("CouponID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("FreeShip");

                    b.Property<decimal>("PercentOff");

                    b.HasKey("CouponID");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Team1FinalProject.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreditCardNumber");

                    b.Property<string>("CreditCardType");

                    b.Property<string>("UserId");

                    b.HasKey("CreditCardID");

                    b.HasIndex("UserId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName");

                    b.HasKey("GenreID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ProDateArrived");

                    b.Property<DateTime>("ProDatePlaced");

                    b.Property<string>("UserId");

                    b.HasKey("InvoiceID");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Team1FinalProject.Models.InvoiceDet", b =>
                {
                    b.Property<int>("InvoiceDetID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BookCost");

                    b.Property<int?>("BookID");

                    b.Property<int?>("InvoiceID");

                    b.Property<int>("QuantityArrived");

                    b.Property<int>("QuantityOrdered");

                    b.HasKey("InvoiceDetID");

                    b.HasIndex("BookID");

                    b.HasIndex("InvoiceID");

                    b.ToTable("InvoiceDets");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId");

                    b.Property<string>("CouponID");

                    b.Property<decimal>("OrderCost");

                    b.Property<DateTime>("OrderDate");

                    b.Property<decimal>("OrderTotal");

                    b.Property<decimal>("ShipCost");

                    b.Property<string>("Status");

                    b.HasKey("OrderID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CouponID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Team1FinalProject.Models.OrderDet", b =>
                {
                    b.Property<int>("OrderDetID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookID");

                    b.Property<int?>("OrderID");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("TotalBookCost");

                    b.Property<decimal>("TotalBookPrice");

                    b.HasKey("OrderDetID");

                    b.HasIndex("BookID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDets");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApproverId");

                    b.Property<string>("AuthorId");

                    b.Property<int?>("BookID");

                    b.Property<decimal>("Rating");

                    b.Property<string>("ReviewContent");

                    b.HasKey("ReviewID");

                    b.HasIndex("ApproverId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Team1FinalProject.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Team1FinalProject.Models.Book", b =>
                {
                    b.HasOne("Team1FinalProject.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreID");
                });

            modelBuilder.Entity("Team1FinalProject.Models.CreditCard", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser", "User")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Invoice", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Team1FinalProject.Models.InvoiceDet", b =>
                {
                    b.HasOne("Team1FinalProject.Models.Book", "Book")
                        .WithMany("InvoiceDets")
                        .HasForeignKey("BookID");

                    b.HasOne("Team1FinalProject.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDets")
                        .HasForeignKey("InvoiceID");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Order", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser", "AppUser")
                        .WithMany("Orders")
                        .HasForeignKey("AppUserId");

                    b.HasOne("Team1FinalProject.Models.Coupon", "Coupon")
                        .WithMany("Orders")
                        .HasForeignKey("CouponID");
                });

            modelBuilder.Entity("Team1FinalProject.Models.OrderDet", b =>
                {
                    b.HasOne("Team1FinalProject.Models.Book", "Book")
                        .WithMany("OrderDets")
                        .HasForeignKey("BookID");

                    b.HasOne("Team1FinalProject.Models.Order", "Order")
                        .WithMany("OrderDets")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("Team1FinalProject.Models.Review", b =>
                {
                    b.HasOne("Team1FinalProject.Models.AppUser", "Approver")
                        .WithMany("ReviewsApproved")
                        .HasForeignKey("ApproverId");

                    b.HasOne("Team1FinalProject.Models.AppUser", "Author")
                        .WithMany("ReviewsWritten")
                        .HasForeignKey("AuthorId");

                    b.HasOne("Team1FinalProject.Models.Book", "Book")
                        .WithMany("ReviewsApproved")
                        .HasForeignKey("BookID");
                });
#pragma warning restore 612, 618
        }
    }
}
