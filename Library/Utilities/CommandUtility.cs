using Library.Models;

namespace Library.Utilities
{
    public class CommandUtility
    {
        public int TimeoutInSeconds { get; set; }


        /// <summary>
        /// Executes the function that modifies data
        /// </summary>
        /// <typeparam name="T">Type of request</typeparam>
        /// <param name="function">function to call</param>
        /// <param name="parameter">request given to the function</param>
        /// <param name="onSuccess">what happens on success</param>
        /// <param name="onFailure">what happens on failure</param>
        public async Task ExecuteAsync<T>(Func<T, CancellationToken, Task<CommandResponseStatus>> function, T parameter, Action onSuccess, Action? onFailure = null)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var response = await function(parameter, cts.Token);
            cts.Dispose();

            if (response.IsSuccess)
            {
                onSuccess();
            }
            else
            {

                onFailure?.Invoke();
            }

        }
    }
}
