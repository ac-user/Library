using AutoMapper;
using Library.Services.Services.Media;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Models.Media.Movies;

namespace Library.Services.Controllers.Media
{
    [Route("api/Account/{accountId}/Library/Media/[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        private readonly IContentServiceFactory<Movie> _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IValidate validate, IContentServiceFactory<Movie> movieService, ILogger<MoviesController> logger, IMapper mapper) : base(validate, logger)
        {
            _movieService = movieService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all movies for a user
        /// </summary>
        /// <param name="accountId">user books are associated to</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of associated movies</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _movieService.GetAllAsync(accountId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Movie Failed");
        }


        /// <summary>
        /// Get movie
        /// </summary>
        /// <param name="movieId">unique identifier of the movie</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Movie details</returns>
        [HttpGet("{movieId}")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, int movieId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _movieService.GetAsync(movieId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Movie Failed");
        }


        /// <summary>
        /// Create new movie entry
        /// </summary>
        /// <param name="accountId">user movies are associated to</param>
        /// <param name="request">details about the movie</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(int accountId, [FromBody] MovieCreationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _movieService.CreateAsync(accountId, _mapper.Map<Movie>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Create Movie Failed",
            isCreate: true);
        }


        /// <summary>
        /// Update movie entry
        /// </summary>
        /// <param name="accountId">user movies are associated to</param>
        /// <param name="request">details about the movie</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of modification</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int accountId, [FromBody] MovieModificationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _movieService.UpdateAsync(accountId, _mapper.Map<Movie>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Update Movie Failed");
        }


        /// <summary>
        /// Delete movie entry
        /// </summary>
        /// <param name="accountId">user movies are associated to</param>
        /// <param name="movieId">movie to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpDelete("{movieId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int accountId, int movieId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _movieService.DeleteAsync(movieId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Delete Movie Failed");
        }

    }
}
