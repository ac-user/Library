using AutoMapper;
using Library.Services.Models.Media.Book;
using Library.Services.Services;
using Library.Services.Services.Media;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Services.Controllers.Media
{
    [Route("api/Account/{accountId}/Library/Media/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly IContentServiceFactory<Book> _bookService;
        private readonly IMapper _mapper;

        public BooksController(IValidate validate, IContentServiceFactory<Book> bookService, ILogger<BooksController> logger, IMapper mapper) : base(validate, logger)
        {
            _bookService = bookService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all books for a user
        /// </summary>
        /// <param name="accountId">user books are associated to</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of associated books</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
               return await _bookService.GetAllAsync(accountId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Book Failed");                
        }


        /// <summary>
        /// Get book
        /// </summary>
        /// <param name="bookId">unique identifier of the book</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Book details</returns>
        [HttpGet("{bookId}")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int accountId, int bookId, CancellationToken cancellationToken)
        {
            return await ExecuteQueryAsync(async () =>
            {
                return await _bookService.GetAsync(bookId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Get Book Failed");            
        }


        /// <summary>
        /// Create new book entry
        /// </summary>
        /// <param name="accountId">user books are associated to</param>
        /// <param name="request">details about the book</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(int accountId, [FromBody]BookCreationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _bookService.CreateAsync(_mapper.Map<Book>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Create Book Failed", 
            isCreate: true);
        }


        /// <summary>
        /// Update book entry
        /// </summary>
        /// <param name="accountId">user books are associated to</param>
        /// <param name="request">details about the book</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of modification</returns>
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int accountId, [FromBody] BookModificationRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _bookService.UpdateAsync(_mapper.Map<Book>(request), cancellationToken);
            },
            accountId,
            cancellationToken,
            "Update Book Failed");
        }


        /// <summary>
        /// Delete book entry
        /// </summary>
        /// <param name="accountId">user books are associated to</param>
        /// <param name="bookId">book to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int accountId, int bookId, CancellationToken cancellationToken)
        {
            return await ExecuteCommandAsync(async () =>
            {
                return await _bookService.DeleteAsync(bookId, cancellationToken);
            },
            accountId,
            cancellationToken,
            "Delete Book Failed");
        }


    }
}
