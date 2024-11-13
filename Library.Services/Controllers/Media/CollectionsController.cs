using Library.Services.Services.Media;
using Microsoft.AspNetCore.Mvc;

namespace Library.Services.Controllers.Media
{
    [Route("apiAccount/{accountId}/Library/Media/[controller]")]
    [ApiController]
    public class CollectionsController : Controller
    {
        private readonly CollectionService _collectionService;
        private readonly ILogger<CollectionsController> _logger;

        public CollectionsController(CollectionService collectionService, ILogger<CollectionsController> logger)
        {
            _collectionService = collectionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            return View();
        }
    }
}
