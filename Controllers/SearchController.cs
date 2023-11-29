using Microsoft.AspNetCore.Mvc;
using RentHiveV2.DAL;

    

    namespace RentHiveV2.Controllers
    {
        public class SearchController : Controller
        {
            private readonly ApplicationDbContext _context;

            public SearchController(ApplicationDbContext context)
            {
                _context = context;
            }

            public IActionResult Index(string keywords, string country, string city)
            {
                // find results in database based on search phrases
                var results = _context.Listing
                    .Where(l =>
                        (string.IsNullOrEmpty(keywords) || l.Description.Contains(keywords)) &&
                        (string.IsNullOrEmpty(country) || l.Country == country) &&

                        (string.IsNullOrEmpty(city) || l.City == city)
                    )
                    .ToList();

                // show the results
                return View(results);
            }
        }
    }

