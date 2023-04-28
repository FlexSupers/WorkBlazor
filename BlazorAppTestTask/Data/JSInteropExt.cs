using Microsoft.JSInterop;

namespace BlazorAppTestTask.Data
{
    public static class JSInteropExt
    {
        public static async Task SaveAsFileAsync(this IJSRuntime js, string filename, byte[] data, string type = "application/octet-stream")
        {
            await js.InvokeAsync<object>("script.saveAsFile", filename, type, Convert.ToBase64String(data));
        }
    }
}
