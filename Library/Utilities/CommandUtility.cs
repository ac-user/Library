using Library.Models;

namespace Library.Utilities
{
    public class CommandUtility : IDisposable
    {
        public int TimeoutInSeconds { get; set; }
        private bool _disposed;

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
            bool success = false;
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                var response = await function(parameter, cts.Token);
                cts.Dispose();
                success = response.IsSuccess;
            }
            catch(Exception e) { }

            if (success)
            {
                onSuccess();
            }
            else
            {
                onFailure?.Invoke();
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
                if (disposing)
                {
                    // Clean up managed resources here if any
                }

                // Clean up unmanaged resources here if any

                _disposed = true;
            }
        }
    }
}
