using Microsoft.JSInterop;

namespace Library.UI.Utilities
{
    public class NotificationUtility
    {
        private readonly IJSRuntime _jsRuntime;
        
        public string Title { get; set; }
        public string Message { get; set; }

        public NotificationUtility(IJSRuntime jSRuntime) 
        {
            _jsRuntime = jSRuntime;
        }

        public void ShowNotification(string title, string message)
        {
            Title = title;
            Message = message;
            _jsRuntime.InvokeVoidAsync("showToast");
        }
    }
}
