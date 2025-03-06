using Microsoft.AspNetCore.Mvc;
using Rush.Application.Services.Seeders;

namespace Rush.WebAPI.Controllers.Seeders
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly Seed _dataSeeder;

        public SeedController(Seed dataSeeder)
        {
            _dataSeeder = dataSeeder;
        }

        [HttpGet]
        public async Task<IActionResult> SeedData()  
        {
            var response = await _dataSeeder.SeedDataAsync();

            return Ok(response);  
        }
    }
}
