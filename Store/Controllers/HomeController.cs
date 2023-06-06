using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Entity.ViewModels;
using Store.Repository.Interface;

namespace Store.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHomeRepository _HomeRepository;

        public HomeController(IHomeRepository HomeRepository)
        {
            _HomeRepository = HomeRepository;
        }

        [HttpGet("")]
        [Authorize()]
        public IActionResult GetStores(int UserId)
        {
            var result = _HomeRepository.GetStores(UserId);
            return Json(new {result});
        }

        [HttpGet("{StoreId:int}")]
        [Authorize()]
        public IActionResult GetStoresById(int StoreId)
        {
            var result = _HomeRepository.GetStoresById(StoreId);
            return Json(new { result });
        }

        [HttpPost("")]
        [Authorize()]
        public IActionResult AddStores(StoresModel obj)
        {
            object? result = _HomeRepository.AddStores(obj);
            return Json(new { result });
        }

        [HttpPost("{StoreId:int}")]
        [Authorize()]
        public IActionResult EditStores(StoresModel obj)
        {
            object? result = _HomeRepository.EditStores(obj);
            return Json(new { result });
        }

        [HttpDelete("{StoreId:int}")]
        [Authorize()]
        public IActionResult DeleteStore(int StoreId)
        {
            var result = _HomeRepository.DeleteStores(StoreId);
            return Json(new { result });
        }

        [HttpGet("locations")]
        public IActionResult GetLocations()
        {
            object cities = _HomeRepository.GetCities();
            object states = _HomeRepository.GetStates();
            object countries = _HomeRepository.GetCountry();
            return Json(new { cities, states, countries });
        }


    }
}
