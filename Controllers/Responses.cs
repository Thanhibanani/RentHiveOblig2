namespace RentHiveV2.Controllers
{
    public class Responses
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; } // Include Errors property
    }
}
