using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentHiveV2.Models;

namespace RentHiveV2.DAL
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<IListing> Listing { get; set; }
        public DbSet<Bookings> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Hvis vi skal lage brukere i DB som i forrige oblig
             var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        UserName = "Pål@oslomet.no",
                        Firstname = "Pål",
                        Lastname = "Mikkelson",
                        Email = "Pål@oslomet.no",
                    },
                    new ApplicationUser
                    {
                        UserName = "Andreas@oslomet.no",
                        Firstname = "Andreas",
                        Lastname = "Anderson",
                        Email = "Andreas@oslomet.no",
                    },
                    new ApplicationUser
                    {
                        UserName = "Linea@oslomet.no",
                        Firstname = "Linea",
                        Lastname = "Mørk",
                        Email = "Linea@oslomet.no",
                    },
                    new ApplicationUser
                    {
                        UserName = "Lars@oslomet.no",
                        Firstname = "Lars",
                        Lastname = "Petterson",
                        Email = "Lars@oslomet.no",
                    },
                };
             */
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IListing>().HasData(
                new IListing
                {
                    //Starter denne på 2 fordi får feilmelding om duplicated PK på ListingId(1)
                    //Jeg tror dette er pga den test listingen som ligger i dabasen som sikkert har verdi id 1
                    ListingId = 2,
                    Title = "Cozy cabin, newly buildt near Oslo",
                    Description = "Here lies a remarkable house of outstanding quality, meticulously built from teh ground up. It represents the very best in comfort, style, and craftsmanship. It consists of a large carport, 3 bedrooms, one bathroom, and a wardrobe in the hallway. A great family home.",
                    PricePerNight = 650,
                    Street = "Ammerudgrenda",
                    City = "Oslo",
                    Country = "Norway",
                    ZipCode = "0958",
                    State = "Oslo",
                    Bedroom = 3,
                    Bathroom = 1,
                    Beds = 4,
                    Image1 = "ClientApp/src/assets/images/house_1_s1",
                    Image2 = "ClientApp/src/assets/images/house_1_s2",
                    Image3 = "ClientApp/src/assets/images/house_1_s3",
                },
                new IListing
                {
                    ListingId = 3,
                    Title = "Cozy modern apartment",
                    Description = "Welcome to this property, which is nicely located and very secluded at the end of a cul-de-sac. Close to the sea and Knarberg marina, which leads you directly into the idyllic Bjerkøysundet with many beautiful island opportunities in the archipelago. The house is also sunny and child-friendly. From the house, it's not far down to the sea. This area offers many great hiking opportunities and is close to the illuminated ski trail. ",
                    PricePerNight = 400,
                    Street = "Tanbergveien",
                    City = "Gjøvik",
                    Country = "Norway",
                    ZipCode = "2819",
                    State = "Innlandet",
                    Bedroom = 3,
                    Bathroom = 1,
                    Beds = 3,
                    Image1 = "ClientApp/src/assets/images/house_2_s1",
                    Image2 = "ClientApp/src/assets/images/house_2_s2",
                    Image3 = "ClientApp/src/assets/images/house_2_s3",
                },
                new IListing
                {
                    ListingId = 4,
                    Title = "Cozy large sized cabin",
                    Description = "Detached turnkey single-family homes from Nordbohus Modum, with a lifetime standard, projected on plot 1, zoned for 5 single-family homes. Centrally located in Skurdalen. 200 meters from the kindergarten, exit from Fv40, 10 km to Geilo, halfway between Oslo and Bergen by road and the Bergen Railway.",
                    PricePerNight = 460,
                    Street = "Alarmvegen",
                    City = "Lillehammer",
                    Country = "Norway",
                    ZipCode = "9020",
                    State = "Troms og Finnmark",
                    Bedroom = 4,
                    Bathroom = 1,
                    Beds = 4,
                    Image1 = "ClientApp/src/assets/images/house_3_s1",
                    Image2 = "ClientApp/src/assets/images/house_3_s2",
                    Image3 = "ClientApp/src/assets/images/house_3_s3",
                },
                new IListing
                {
                    ListingId = 5,
                    Title = "Cozy small sized cabin",
                    Description = "The mini-house have an area-efficient and rich floor plan. The 1st floor comprises a hall with stairs, bathroom, storage room, technical room, one bedroom, and a living room/kitchen in an open layout. The 2nd floor has a practical loft. In connection with the entrance, a decking will be built with space for garden furniture. The house comes with a sports storage room, as well as a parking space.",
                    PricePerNight = 500,
                    Street = "Daglivegen",
                    City = "Geilo",
                    Country = "Norway",
                    ZipCode = "3580",
                    State = "Viken",
                    Bedroom = 2,
                    Bathroom = 1,
                    Beds = 3,
                    Image1 = "ClientApp/src/assets/images/house_4_s1",
                    Image2 = "ClientApp/src/assets/images/house_4_s2",
                    Image3 = "ClientApp/src/assets/images/house_4_s3",
                },
                new IListing
                {
                    ListingId = 6,
                    Title = "Medium sized modern cabin",
                    Description = "Modern home where emphasis has been placed on quality. Turnkey homes with no index regulation or additional costs. Attractive area in Resahagen which is starting to take shape with schools, kindergarten, and sports field nearby. 3 low-maintenance detached houses with a sheltered outdoor area where one can enjoy the afternoon and evening sun directly from the kitchen/living room. Practical home with 3 bedrooms, 2 living rooms, 2 bathrooms, laundry room, storage rooms. Carport with storage. Upgraded kitchen with appliances from Sigdal. Separate kitchen island. Single plank laminate throughout the home on all floors except tiles in the bathrooms, ensuring a consistent profile.",
                    PricePerNight = 600,
                    Street = "Torvvegen",
                    City = "Reinsvoll",
                    Country = "Norway",
                    ZipCode = "2840",
                    State = "Innlandet",
                    Bedroom = 1,
                    Bathroom = 1,
                    Beds = 1,
                    Image1 = "ClientApp/src/assets/images/house_5_s1",
                    Image2 = "ClientApp/src/assets/images/house_5_s2",
                    Image3 = "ClientApp/src/assets/images/house_5_s3",
                },
                new IListing
                {
                    ListingId = 7,
                    Title = "Modern apartment near Tromsø",
                    Description = "Here is a beautiful home that are to be built in the lovely Tromsdalen. It has a beautiful architectural design with large glass surfaces and exquisite material choices. Here you can live in peaceful surroundings while having all desired amenities within walking distance from the front door; school, kindergartens, recreational areas, illuminated ski trail, grocery store, as well as specialty stores with Pizza and Sushi. Good bus connections with a short distance to the bus stop.",
                    PricePerNight = 450,
                    Street = "Gaupevegen",
                    City = "Jørpeland",
                    Country = "Norway",
                    ZipCode = "4103",
                    State = "Rogaland",
                    Bedroom = 3,
                    Bathroom = 1,
                    Beds = 3,
                    Image1 = "ClientApp/src/assets/images/house_6_s1",
                    Image2 = "ClientApp/src/assets/images/house_6_s2",
                    Image3 = "ClientApp/src/assets/images/house_6_s3",
                }
            );
        }
    }
}
