using Library.UI.Model;

namespace Library.UI.Utilities
{
    public class CommandUtility : IDisposable
    {
        public int TimeoutInMilliseconds { get; set; } = 30000;
        private bool _disposed;

        /// <summary>
        /// Executes the function that modifies data
        /// </summary>
        /// <typeparam name="T">Type of request</typeparam>
        /// <param name="function">function to call</param>
        /// <param name="parameter">request given to the function</param>
        /// <param name="onSuccess">what happens on success</param>
        /// <param name="onSuccessId">what happens on success and passes back an Id</param>
        /// <param name="onFailure">what happens on failure</param>
        /// <param name="onFailureMessage">what happens on failure and passes back messages</param>
        public async Task ExecuteAsync<T>(Func<T, CancellationToken, Task<CommandResponseStatus>> function,
                                          T parameter,
                                          Action? onSuccess = null,
                                          Action<int>? onSuccessId = null,
                                          Action? onFailure = null,
                                          Action<List<string>>? onFailureMessage = null)
        {
            bool success = false;
            List<string> messages = new();
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource(TimeoutInMilliseconds);
                var response = await function(parameter, cts.Token);
                cts.Dispose();
                success = response.IsSuccess;
                if (success)
                {
                    onSuccess?.Invoke();
                    onSuccessId?.Invoke(response.Id);
                }
                else
                {
                    messages = response.Messages;
                }
            }
            catch(Exception e) 
            {
                messages.Add(e.Message);
            }

            
            if(!success)
            {
                onFailure?.Invoke();
                onFailureMessage?.Invoke(messages);                
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                TimeoutInMilliseconds = 30000;
                _disposed = true;
            }
        }
    }
}
