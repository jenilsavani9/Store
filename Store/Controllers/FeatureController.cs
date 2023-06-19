using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Entity.ViewModels;
using Store.Repository.Interface;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : Controller
    {
        private readonly IFeaturesRepository _FeatureRepository;

        public FeatureController(IFeaturesRepository FeatureRepository)
        {
            _FeatureRepository = FeatureRepository;
        }

        [HttpGet("")]
        [Authorize()]
        public IActionResult GetFeatures(int UserId)
        {
            var result = _FeatureRepository.GetFeatures(UserId);
            return Json(new { result });
        }

        [HttpGet("{FeatureId:int}")]
        [Authorize()]
        public IActionResult GetFeaturesById(int FeatureId)
        {
            var result = _FeatureRepository.GetFeaturesById(FeatureId);
            return Json(new { result });    
        }

        [HttpPost("")]
        [Authorize()]
        public IActionResult AddFeatures(FeatureModel obj)
        {
            var result = _FeatureRepository.AddFeatures(obj);
            return Json(new { result });
        }

        [HttpPut("{FeatureId:int}")]
        [Authorize()]
        public IActionResult EditFeatures(FeatureModel obj)
        {
            var result = _FeatureRepository.EditFeatures(obj);
            return Json(new { result });
        }

        [HttpDelete("{FeatureId:int}")]
        [Authorize()]
        public IActionResult DeleteFeatures(int FeatureId)
        {
            var result = _FeatureRepository.DeleteFeatures(FeatureId);
            return Json(new { result });
        }
    }
}
