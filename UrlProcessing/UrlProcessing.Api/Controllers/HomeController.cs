using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UrlProcessing.Api.Services;
using UrlProcessing.Models;

namespace UrlProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public HomeController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        public async Task<IActionResult> PostUrl([FromBody]UrlModel model)
        {
            var status = await _urlService.PostUrl(model);
            if (status == HttpStatusCode.OK)
                return Ok(model.Id);
            return BadRequest();
        }
    }
}