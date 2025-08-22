using AutoMapper;
using Library.Models.Media;
using Library.Services.Services;
using Library.Services.Services.Media;
using Microsoft.AspNetCore.Mvc;

namespace Library.Services.Controllers.Media
{
    [Route("api/Account/{accountId}/Library/Media/[controller]")]
    [ApiController]
    public class CollectionsController : BaseController
    {
        private readonly ICollectionService _service;
        private readonly IMapper _mapper;

        public CollectionsController(IValidate validate, ICollectionService service, ILogger<CollectionsController> logger, IMapper mapper) : base(validate, logger)
        {
            _service = service;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all collections for a user
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of associated collections</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Collection>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _service.GetAllAsync(accountId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Collection Failed");
        }


        /// <summary>
        /// Get collection
        /// </summary>
        /// <param name="collectionId">unique identifier of the collection</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Collection details</returns>
        [HttpGet("{collectionId}")]
        [ProducesResponseType(typeof(Collection), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, int collectionId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _service.GetAsync(collectionId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Collection Failed");
        }


        /// <summary>
        /// Create new collection
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="request">details about the collection</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(int accountId, [FromBody] CollectionCreationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.CreateAsync(accountId, _mapper.Map<Collection>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Create Collection Failed",
            isCreate: true);
        }

        /// <summary>
        /// Create new subcollection association
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="collectionId">collection content is associated to</param>
        /// <param name="mediaId">content to add</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpPut("{collectionId}/Content/SubCollections/{subId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(int accountId, int collectionId, int subId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.CreateAsync(collectionId, subId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Create SubCollection Association Failed",
            isCreate: true);
        }

        /// <summary>
        /// Create new content association
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="collectionId">collection content is associated to</param>
        /// <param name="mediaType">content type to add</param>
        /// <param name="mediaId">content to add</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpPut("{collectionId}/Content/{mediaType}/{mediaId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(int accountId, int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.CreateAsync(collectionId, mediaType, mediaId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Create Collection Media Association Failed",
            isCreate: true);
        }


        /// <summary>
        /// Update collection entry
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="request">details about the collection</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of modification</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int accountId, [FromBody] Collection request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.UpdateAsync(accountId, request, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Update Collection Failed");
        }


        /// <summary>
        /// Delete collection entry
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="collectionId">collection to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpDelete("{collectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int accountId, int collectionId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.DeleteAsync(collectionId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Delete Collection Failed");
        }

        /// <summary>
        /// Delete collection content entry
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="collectionId">collection content is associated to</param>
        /// <param name="mediaType">type of content to remove</param>
        /// <param name="mediaId">content to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpDelete("{collectionId}/Content/{mediaType}/{mediaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int accountId, int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.DeleteAsync(collectionId, mediaType, mediaId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Delete Collection Association Failed");
        }
        
        /// <summary>
        /// Delete collection subcollection entry
        /// </summary>
        /// <param name="accountId">user collections are associated to</param>
        /// <param name="collectionId">collection content is associated to</param>
        /// <param name="subId">content to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpDelete("{collectionId}/Content/SubCollections/{subId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSubAsync(int accountId, int collectionId, int subId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _service.DeleteAsync(collectionId, subId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Delete Collection Association Failed");
        }

    }
}
