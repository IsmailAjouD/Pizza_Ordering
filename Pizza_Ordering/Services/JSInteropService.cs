using Microsoft.JSInterop;
using Pizza_Ordering.web.Services.Contracts;

namespace Pizza_Ordering.web.Services
{
    public class JSInteropService : IJSInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JSInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task LoadScriptsAsync()
        {
            await _jsRuntime.InvokeVoidAsync("loadScripts");
        }
    }
}
