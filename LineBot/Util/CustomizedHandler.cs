using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace LineBot.Util
{
    public class CustomizedHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if(request.Content != null)
            {
                var body = await request.Content.ReadAsStringAsync();
                request.Properties["body"] = body;
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}