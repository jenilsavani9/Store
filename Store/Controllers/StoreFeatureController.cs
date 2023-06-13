using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Entity.ViewModels;
using Store.Repository.Interface;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreFeatureController : ControllerBase
    {
        private readonly IStoreFeatureRepository _StoreFeatureRepository;

        public StoreFeatureController(IStoreFeatureRepository StoreFeatureRepository)
        {
            _StoreFeatureRepository = StoreFeatureRepository;
        }

        [HttpGet("Store")]
        [Authorize()]
        public IActionResult GetStoreFeatures(int StoreId)
        {
            var result = _StoreFeatureRepository.GetStoreFeatures(StoreId);
            return Ok(new { result });
        }

        [HttpGet("User")]
        [Authorize()]
        public IActionResult GetStoreFeaturesByUserId(int UserId)
        {
            var result = _StoreFeatureRepository.GetFeatureByUserId(UserId);
            return Ok(new { result });
        }

        [HttpPost("")]
        [Authorize()]
        public IActionResult ChangeStoreFeatures(StoreFeatureModal obj)
        {
            var result = _StoreFeatureRepository.AddStoreFeatures(obj.FeatureIds, obj.StoreId, obj.UserId);
            return Ok(result);
        }
    }
}
