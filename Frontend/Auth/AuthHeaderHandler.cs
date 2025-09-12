using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace Frontend.Auth
{
    public class AuthHeaderHandler(ILocalStorageService localStorage) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpResponseMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath)
        }
    }
}
