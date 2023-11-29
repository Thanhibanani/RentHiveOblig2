using Microsoft.AspNetCore.Mvc;
using RentHiveV2.DAL;
using RentHiveV2.Models;



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
        public async Task<IActionResult> GetAll(string searchPhrase)
        {

            var listings = await _listingRepository.GetAll();

            listings = listings.Where(l =>
        string.IsNullOrEmpty(searchPhrase) ||
        l.Title.Contains(searchPhrase) ||
        l.Description.Contains(searchPhrase) ||
        l.Country.Contains(searchPhrase) ||
        l.City.Contains(searchPhrase)
            ).ToList();

            if (listings == null)
            {
                _logger.LogError("There were no Listings found when executing _listingRepository.GetAll()");
                return Ok(new List<Listing>()); // Return an empty list instead of null
            }


            return Ok(listings);


            /**
        .Where(l =>
                    (string.IsNullOrEmpty(keywords) || l.Description.Contains(keywords)) &&
                    (string.IsNullOrEmpty(country) || l.Country == country) &&

                    (string.IsNullOrEmpty(city) || l.City == city)
                )
                .ToList();


        if (results == null)
        {
            _logger.LogError("There were no Listings found when executing _listingRepository.GetAll()");
            return NotFound(new Responses { Success = false, Message = "Nothing matches your search." });
        }

        return Ok(results);
    }
            */




            /*
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
                }*/
        }

    }
}

