using AutoMapper;
using Library.Services.Models.Media.Music;
using Library.Services.Services.Media;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Services.Controllers.Media
{
    [Route("api/Account/{accountId}/Library/Media/[controller]")]
    [ApiController]
    public class MusicController : BaseController
    {
        private readonly IContentServiceFactory<Music> _musicService;
        private readonly IMapper _mapper;

        public MusicController(IValidate validate, IContentServiceFactory<Music> musicService, ILogger<MusicController> logger, IMapper mapper) : base(validate, logger)
        {
            _musicService = musicService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all music for a user
        /// </summary>
        /// <param name="accountId">user music are associated to</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of associated music</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Music>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _musicService.GetAllAsync(accountId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Music Failed");
        }


        /// <summary>
        /// Get music
        /// </summary>
        /// <param name="musicId">unique identifier of the music</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Music details</returns>
        [HttpGet("{musicId}")]
        [ProducesResponseType(typeof(Music), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, int musicId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _musicService.GetAsync(musicId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Music Failed");
        }


        /// <summary>
        /// Create new music entry
        /// </summary>
        /// <param name="accountId">user music are associated to</param>
        /// <param name="request">details about the music</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(int accountId, [FromBody] MusicCreationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _musicService.CreateAsync(_mapper.Map<Music>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Create Music Failed",
            isCreate: true);
        }


        /// <summary>
        /// Update music entry
        /// </summary>
        /// <param name="accountId">user music are associated to</param>
        /// <param name="request">details about the music</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of modification</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int accountId, [FromBody] MusicModificationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _musicService.UpdateAsync(_mapper.Map<Music>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Update Music Failed");
        }


        /// <summary>
        /// Delete music entry
        /// </summary>
        /// <param name="accountId">user music are associated to</param>
        /// <param name="musicId">music to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int accountId, int musicId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _musicService.DeleteAsync(musicId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Delete Music Failed");
        }

    }
}
