﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentHiveV2.DAL;

#nullable disable

namespace RentHiveV2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("bit");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RentHiveV2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("RentHiveV2.Models.Bookings", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"), 1L, 1);

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("BookingId");

                    b.HasIndex("GuestId");

                    b.HasIndex("ListingId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("RentHiveV2.Models.Listing", b =>
                {
                    b.Property<int>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ListingId"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Bathroom")
                        .HasColumnType("int");

                    b.Property<int>("Bedroom")
                        .HasColumnType("int");

                    b.Property<int>("Beds")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerNight")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ListingId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Listing");

                    b.HasData(
                        new
                        {
                            ListingId = 2,
                            Bathroom = 1,
                            Bedroom = 3,
                            Beds = 4,
                            City = "Oslo",
                            Country = "Norway",
                            CreatedDateTime = new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(346),
                            Description = "Here lies a remarkable house of outstanding quality, meticulously built from teh ground up. It represents the very best in comfort, style, and craftsmanship. It consists of a large carport, 3 bedrooms, one bathroom, and a wardrobe in the hallway. A great family home.",
                            Image1 = "assets/images/house_1_s1.jpg",
                            Image2 = "assets/images/house_1_s2.jpg",
                            Image3 = "assets/images/house_1_s3.jpg",
                            PricePerNight = 650.0,
                            State = "Oslo",
                            Street = "Ammerudgrenda",
                            Title = "Cozy cabin, newly buildt near Oslo",
                            ZipCode = "0958"
                        },
                        new
                        {
                            ListingId = 3,
                            Bathroom = 1,
                            Bedroom = 3,
                            Beds = 3,
                            City = "Gjøvik",
                            Country = "Norway",
                            CreatedDateTime = new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(377),
                            Description = "Welcome to this property, which is nicely located and very secluded at the end of a cul-de-sac. Close to the sea and Knarberg marina, which leads you directly into the idyllic Bjerkøysundet with many beautiful island opportunities in the archipelago. The house is also sunny and child-friendly. From the house, it's not far down to the sea. This area offers many great hiking opportunities and is close to the illuminated ski trail. ",
                            Image1 = "assets/images/house_2_s1.jpg",
                            Image2 = "assets/images/house_2_s2.jpg",
                            Image3 = "assets/images/house_2_s3.jpg",
                            PricePerNight = 400.0,
                            State = "Innlandet",
                            Street = "Tanbergveien",
                            Title = "Cozy modern apartment",
                            ZipCode = "2819"
                        },
                        new
                        {
                            ListingId = 4,
                            Bathroom = 1,
                            Bedroom = 4,
                            Beds = 4,
                            City = "Lillehammer",
                            Country = "Norway",
                            CreatedDateTime = new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(380),
                            Description = "Detached turnkey single-family homes from Nordbohus Modum, with a lifetime standard, projected on plot 1, zoned for 5 single-family homes. Centrally located in Skurdalen. 200 meters from the kindergarten, exit from Fv40, 10 km to Geilo, halfway between Oslo and Bergen by road and the Bergen Railway.",
                            Image1 = "assets/images/house_3_s1.jpg",
                            Image2 = "assets/images/house_3_s2.jpg",
                            Image3 = "assets/images/house_3_s3.jpg",
                            PricePerNight = 460.0,
                            State = "Troms og Finnmark",
                            Street = "Alarmvegen",
                            Title = "Cozy large sized cabin",
                            ZipCode = "9020"
                        },
                        new
                        {
                            ListingId = 5,
                            Bathroom = 1,
                            Bedroom = 2,
                            Beds = 3,
                            City = "Geilo",
                            Country = "Norway",
                            CreatedDateTime = new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(383),
                            Description = "The mini-house have an area-efficient and rich floor plan. The 1st floor comprises a hall with stairs, bathroom, storage room, technical room, one bedroom, and a living room/kitchen in an open layout. The 2nd floor has a practical loft. In connection with the entrance, a decking will be built with space for garden furniture. The house comes with a sports storage room, as well as a parking space.",
                            Image1 = "assets/images/house_4_s1.jpg",
                            Image2 = "assets/images/house_4_s2.jpg",
                            Image3 = "assets/images/house_4_s3.jpg",
                            PricePerNight = 500.0,
                            State = "Viken",
                            Street = "Daglivegen",
                            Title = "Cozy small sized cabin",
                            ZipCode = "3580"
                        },
                        new
                        {
                            ListingId = 6,
                            Bathroom = 1,
                            Bedroom = 1,
                            Beds = 1,
                            City = "Reinsvoll",
                            Country = "Norway",
                            CreatedDateTime = new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(387),
                            Description = "Modern home where emphasis has been placed on quality. Turnkey homes with no index regulation or additional costs. Attractive area in Resahagen which is starting to take shape with schools, kindergarten, and sports field nearby. 3 low-maintenance detached houses with a sheltered outdoor area where one can enjoy the afternoon and evening sun directly from the kitchen/living room. Practical home with 3 bedrooms, 2 living rooms, 2 bathrooms, laundry room, storage rooms. Carport with storage. Upgraded kitchen with appliances from Sigdal. Separate kitchen island. Single plank laminate throughout the home on all floors except tiles in the bathrooms, ensuring a consistent profile.",
                            Image1 = "assets/images/house_5_s1.jpg",
                            Image2 = "assets/images/house_5_s2.jpg",
                            Image3 = "assets/images/house_5_s3.jpg",
                            PricePerNight = 600.0,
                            State = "Innlandet",
                            Street = "Torvvegen",
                            Title = "Medium sized modern cabin",
                            ZipCode = "2840"
                        },
                        new
                        {
                            ListingId = 7,
                            Bathroom = 1,
                            Bedroom = 3,
                            Beds = 3,
                            City = "Jørpeland",
                            Country = "Norway",
                            CreatedDateTime = new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(390),
                            Description = "Here is a beautiful home that are to be built in the lovely Tromsdalen. It has a beautiful architectural design with large glass surfaces and exquisite material choices. Here you can live in peaceful surroundings while having all desired amenities within walking distance from the front door; school, kindergartens, recreational areas, illuminated ski trail, grocery store, as well as specialty stores with Pizza and Sushi. Good bus connections with a short distance to the bus stop.",
                            Image1 = "assets/images/house_6_s1.jpg",
                            Image2 = "assets/images/house_6_s2.jpg",
                            Image3 = "assets/images/house_6_s3.jpg",
                            PricePerNight = 450.0,
                            State = "Rogaland",
                            Street = "Gaupevegen",
                            Title = "Modern apartment near Tromsø",
                            ZipCode = "4103"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RentHiveV2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RentHiveV2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentHiveV2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RentHiveV2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentHiveV2.Models.Bookings", b =>
                {
                    b.HasOne("RentHiveV2.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentHiveV2.Models.Listing", "Listing")
                        .WithMany("Bookings")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("RentHiveV2.Models.Listing", b =>
                {
                    b.HasOne("RentHiveV2.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Listings")
                        .HasForeignKey("ApplicationUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("RentHiveV2.Models.ApplicationUser", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Listings");
                });

            modelBuilder.Entity("RentHiveV2.Models.Listing", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
