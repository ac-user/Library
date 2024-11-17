using Library.Services.Services.Media;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using Library.Services.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Services.Controllers
{
    public class BaseController
    {
        private readonly IValidate _validate;
        private readonly ILogger<BaseController> _logger;

        public BaseController(IValidate validate, ILogger<BaseController> logger)
        {
            _validate = validate;
            _logger = logger;
        }

        /// <summary>
        /// Generic controller based function for handling query based requests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation"></param>
        /// <param name="accountId"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExecuteQueryAsync<T>(Func<Task<T>> operation, int accountId, CancellationToken cancellationToken, string failureMessage)
        {
            try
            {
                if (await _validate.ValidateAccountAsync(accountId, cancellationToken))
                {
                    var result = await operation();
                    if(result != null)
                    {
                        return new OkObjectResult(result);
                    }
                    else
                    {
                        return new NoContentResult();
                    }
                }
                return new BadRequestObjectResult("Verify your account status");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, failureMessage);
                return new ObjectResult(new ProblemDetails()
                {
                    Title = failureMessage,
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Generic controller function for handling command based requests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation"></param>
        /// <param name="accountId"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="failureMessage"></param>
        /// <param name="isCreate"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExecuteCommandAsync<T>(Func<Task<T>> operation, int accountId, CancellationToken cancellationToken, string failureMessage, bool isCreate = false)
        {
            try
            {
                if (await _validate.ValidateAccountAsync(accountId, cancellationToken))
                {
                    var result = await operation() as ResponseStatus;
                    if (result != null && result.IsSuccess)
                    {
                        if(isCreate)
                        {
                            return new CreatedResult("",result.Id);
                        }
                        else
                        {
                            return new OkResult();
                        }
                    }
                    else
                    {
                        return new ObjectResult(new ProblemDetails()
                        {
                            Title = failureMessage,
                            Detail = String.Join('.', result.Messages),
                            Status = (int)HttpStatusCode.PreconditionFailed
                        });
                    }
                }
                return new BadRequestObjectResult("Verify your account status");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, failureMessage);
                return new ObjectResult(new ProblemDetails()
                {
                    Title = failureMessage,
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
