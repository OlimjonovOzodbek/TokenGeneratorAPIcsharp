using Car.Application.Services;
using Car.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Car.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenGeneratorController : ControllerBase
    {
        private readonly ICarService _carService;

        public TokenGeneratorController(ICarService carService)
        {
            _carService = carService;
        }

        public string GenerateToken(CarModel model)
        {
            return _carService.TokenGenerator(model);
        }
    }
}
