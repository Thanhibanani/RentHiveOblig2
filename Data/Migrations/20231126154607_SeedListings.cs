using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHiveV2.Data.Migrations
{
    public partial class SeedListings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 2,
                columns: new[] { "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 4, "Oslo", new DateTime(2023, 11, 26, 16, 46, 7, 383, DateTimeKind.Local).AddTicks(6728), "Here lies a remarkable house of outstanding quality, meticulously built from teh ground up. It represents the very best in comfort, style, and craftsmanship. It consists of a large carport, 3 bedrooms, one bathroom, and a wardrobe in the hallway. A great family home.", "ClientApp/src/assets/images/house_1_s1", "ClientApp/src/assets/images/house_1_s2", "ClientApp/src/assets/images/house_1_s3", 650.0, "Oslo", "Ammerudgrenda", "Cozy cabin, newly buildt near Oslo", "0958" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 3,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 3, 3, "Gjøvik", new DateTime(2023, 11, 26, 16, 46, 7, 383, DateTimeKind.Local).AddTicks(6798), "Welcome to this property, which is nicely located and very secluded at the end of a cul-de-sac. Close to the sea and Knarberg marina, which leads you directly into the idyllic Bjerkøysundet with many beautiful island opportunities in the archipelago. The house is also sunny and child-friendly. From the house, it's not far down to the sea. This area offers many great hiking opportunities and is close to the illuminated ski trail. ", "ClientApp/src/assets/images/house_2_s1", "ClientApp/src/assets/images/house_2_s2", "ClientApp/src/assets/images/house_2_s3", 400.0, "Innlandet", "Tanbergveien", "Cozy modern apartment", "2819" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 4,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 4, 4, "Lillehammer", new DateTime(2023, 11, 26, 16, 46, 7, 383, DateTimeKind.Local).AddTicks(6807), "Detached turnkey single-family homes from Nordbohus Modum, with a lifetime standard, projected on plot 1, zoned for 5 single-family homes. Centrally located in Skurdalen. 200 meters from the kindergarten, exit from Fv40, 10 km to Geilo, halfway between Oslo and Bergen by road and the Bergen Railway.", "ClientApp/src/assets/images/house_3_s1", "ClientApp/src/assets/images/house_3_s2", "ClientApp/src/assets/images/house_3_s3", 460.0, "Troms og Finnmark", "Alarmvegen", "Cozy large sized cabin", "9020" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 5,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 2, 3, "Geilo", new DateTime(2023, 11, 26, 16, 46, 7, 383, DateTimeKind.Local).AddTicks(6812), "The mini-house have an area-efficient and rich floor plan. The 1st floor comprises a hall with stairs, bathroom, storage room, technical room, one bedroom, and a living room/kitchen in an open layout. The 2nd floor has a practical loft. In connection with the entrance, a decking will be built with space for garden furniture. The house comes with a sports storage room, as well as a parking space.", "ClientApp/src/assets/images/house_4_s1", "ClientApp/src/assets/images/house_4_s2", "ClientApp/src/assets/images/house_4_s3", 500.0, "Viken", "Daglivegen", "Cozy small sized cabin", "3580" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 6,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 1, 1, "Reinsvoll", new DateTime(2023, 11, 26, 16, 46, 7, 383, DateTimeKind.Local).AddTicks(6820), "Modern home where emphasis has been placed on quality. Turnkey homes with no index regulation or additional costs. Attractive area in Resahagen which is starting to take shape with schools, kindergarten, and sports field nearby. 3 low-maintenance detached houses with a sheltered outdoor area where one can enjoy the afternoon and evening sun directly from the kitchen/living room. Practical home with 3 bedrooms, 2 living rooms, 2 bathrooms, laundry room, storage rooms. Carport with storage. Upgraded kitchen with appliances from Sigdal. Separate kitchen island. Single plank laminate throughout the home on all floors except tiles in the bathrooms, ensuring a consistent profile.", "ClientApp/src/assets/images/house_5_s1", "ClientApp/src/assets/images/house_5_s2", "ClientApp/src/assets/images/house_5_s3", 600.0, "Innlandet", "Torvvegen", "Medium sized modern cabin", "2840" });

            migrationBuilder.InsertData(
                table: "Listing",
                columns: new[] { "ListingId", "ApplicationUserId", "Bathroom", "Bedroom", "Beds", "City", "Country", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 7, null, 1, 3, 3, "Jørpeland", "Norway", new DateTime(2023, 11, 26, 16, 46, 7, 383, DateTimeKind.Local).AddTicks(6825), "Here is a beautiful home that are to be built in the lovely Tromsdalen. It has a beautiful architectural design with large glass surfaces and exquisite material choices. Here you can live in peaceful surroundings while having all desired amenities within walking distance from the front door; school, kindergartens, recreational areas, illuminated ski trail, grocery store, as well as specialty stores with Pizza and Sushi. Good bus connections with a short distance to the bus stop.", "ClientApp/src/assets/images/house_6_s1", "ClientApp/src/assets/images/house_6_s2", "ClientApp/src/assets/images/house_6_s3", 450.0, "Rogaland", "Gaupevegen", "Modern apartment near Tromsø", "4103" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 2,
                columns: new[] { "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 3, "Gjøvik", new DateTime(2023, 11, 26, 16, 28, 23, 430, DateTimeKind.Local).AddTicks(6478), "Welcome to this property, which is nicely located and very secluded at the end of a cul-de-sac. Close to the sea and Knarberg marina, which leads you directly into the idyllic Bjerkøysundet with many beautiful island opportunities in the archipelago. The house is also sunny and child-friendly. From the house, it's not far down to the sea. This area offers many great hiking opportunities and is close to the illuminated ski trail. ", "ClientApp/src/assets/images/house_2_s1", "ClientApp/src/assets/images/house_2_s2", "ClientApp/src/assets/images/house_2_s3", 400.0, "Innlandet", "Tanbergveien", "Cozy modern apartment", "2819" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 3,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 4, 4, "Lillehammer", new DateTime(2023, 11, 26, 16, 28, 23, 430, DateTimeKind.Local).AddTicks(6484), "Detached turnkey single-family homes from Nordbohus Modum, with a lifetime standard, projected on plot 1, zoned for 5 single-family homes. Centrally located in Skurdalen. 200 meters from the kindergarten, exit from Fv40, 10 km to Geilo, halfway between Oslo and Bergen by road and the Bergen Railway.", "ClientApp/src/assets/images/house_3_s1", "ClientApp/src/assets/images/house_3_s2", "ClientApp/src/assets/images/house_3_s3", 460.0, "Troms og Finnmark", "Alarmvegen", "Cozy large sized cabin", "9020" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 4,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 2, 3, "Geilo", new DateTime(2023, 11, 26, 16, 28, 23, 430, DateTimeKind.Local).AddTicks(6488), "The mini-house have an area-efficient and rich floor plan. The 1st floor comprises a hall with stairs, bathroom, storage room, technical room, one bedroom, and a living room/kitchen in an open layout. The 2nd floor has a practical loft. In connection with the entrance, a decking will be built with space for garden furniture. The house comes with a sports storage room, as well as a parking space.", "ClientApp/src/assets/images/house_4_s1", "ClientApp/src/assets/images/house_4_s2", "ClientApp/src/assets/images/house_4_s3", 500.0, "Viken", "Daglivegen", "Cozy small sized cabin", "3580" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 5,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 1, 1, "Reinsvoll", new DateTime(2023, 11, 26, 16, 28, 23, 430, DateTimeKind.Local).AddTicks(6492), "Modern home where emphasis has been placed on quality. Turnkey homes with no index regulation or additional costs. Attractive area in Resahagen which is starting to take shape with schools, kindergarten, and sports field nearby. 3 low-maintenance detached houses with a sheltered outdoor area where one can enjoy the afternoon and evening sun directly from the kitchen/living room. Practical home with 3 bedrooms, 2 living rooms, 2 bathrooms, laundry room, storage rooms. Carport with storage. Upgraded kitchen with appliances from Sigdal. Separate kitchen island. Single plank laminate throughout the home on all floors except tiles in the bathrooms, ensuring a consistent profile.", "ClientApp/src/assets/images/house_5_s1", "ClientApp/src/assets/images/house_5_s2", "ClientApp/src/assets/images/house_5_s3", 600.0, "Innlandet", "Torvvegen", "Medium sized modern cabin", "2840" });

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 6,
                columns: new[] { "Bedroom", "Beds", "City", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 3, 3, "Jørpeland", new DateTime(2023, 11, 26, 16, 28, 23, 430, DateTimeKind.Local).AddTicks(6496), "Here is a beautiful home that are to be built in the lovely Tromsdalen. It has a beautiful architectural design with large glass surfaces and exquisite material choices. Here you can live in peaceful surroundings while having all desired amenities within walking distance from the front door; school, kindergartens, recreational areas, illuminated ski trail, grocery store, as well as specialty stores with Pizza and Sushi. Good bus connections with a short distance to the bus stop.", "ClientApp/src/assets/images/house_6_s1", "ClientApp/src/assets/images/house_6_s2", "ClientApp/src/assets/images/house_6_s3", 450.0, "Rogaland", "Gaupevegen", "Modern apartment near Tromsø", "4103" });

            migrationBuilder.InsertData(
                table: "Listing",
                columns: new[] { "ListingId", "ApplicationUserId", "Bathroom", "Bedroom", "Beds", "City", "Country", "CreatedDateTime", "Description", "Image1", "Image2", "Image3", "PricePerNight", "State", "Street", "Title", "ZipCode" },
                values: new object[] { 1, null, 1, 3, 4, "Oslo", "Norway", new DateTime(2023, 11, 26, 16, 28, 23, 430, DateTimeKind.Local).AddTicks(6416), "Here lies a remarkable house of outstanding quality, meticulously built from teh ground up. It represents the very best in comfort, style, and craftsmanship. It consists of a large carport, 3 bedrooms, one bathroom, and a wardrobe in the hallway. A great family home.", "ClientApp/src/assets/images/house_1_s1", "ClientApp/src/assets/images/house_1_s2", "ClientApp/src/assets/images/house_1_s3", 650.0, "Oslo", "Ammerudgrenda", "Cozy cabin, newly buildt near Oslo", "0958" });
        }
    }
}
