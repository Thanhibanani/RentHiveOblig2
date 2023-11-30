using Microsoft.AspNetCore.Mvc;
using RentHiveV2.DAL;
using RentHiveV2.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace RentHiveV2.Controllers


{

    [ApiController]
    [Route("api/[controller]")]

    public class SearchController : ControllerBase

    {




        private readonly IListingRepository _listingRepository;
        private readonly ILogger<SearchController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;




        public SearchController(ApplicationDbContext context, IListingRepository listingRepository, ILogger<SearchController> logger)

        {
            _context = context;
            _listingRepository = listingRepository;
            _logger = logger;
            

        }

        /* get all method gets all listings from database, and shows them if the search phrase entered matches
         logging*/

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(string searchPhrase)
        {
            try
            {
                var listings = _context.Listing.AsQueryable();

                if (!string.IsNullOrEmpty(searchPhrase))
                {
                    listings = listings.Where(l =>
                        l.Title.Contains(searchPhrase) ||
                        l.Description.Contains(searchPhrase) ||
                        l.Country.Contains(searchPhrase) ||
                        l.City.Contains(searchPhrase)
                    );
                }

                return Ok(listings.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SearchController: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

            


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

