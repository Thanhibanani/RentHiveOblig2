using Microsoft.AspNetCore.Mvc;
using RentHiveV2.DAL;



namespace RentHiveV2.Controllers


{

    [ApiController]
    [Route("api/[controller]")]
        
    public class SearchController : Controller
        
    {




        private readonly IListingRepository _listingRepository;
        private readonly ILogger<ListingController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        



            public SearchController(ApplicationDbContext context, IListingRepository listingRepository, ILogger<ListingController> logger, IWebHostEnvironment hostEnvironment)
        
            {
                _context = context;
                _listingRepository = listingRepository;
                _logger = logger;
                _hostEnvironment = hostEnvironment;

        }

        [HttpGet]
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

