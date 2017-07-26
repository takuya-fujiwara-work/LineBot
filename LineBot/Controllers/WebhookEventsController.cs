using LineBot.Models;
using LineBot.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace LineBot.Controllers
{
    public class WebhookEventsController : ApiController
    {
        private const string X_LINE_SIGNATURE = "X-Line-Signature";
        private const string LINE_HOST = "https://api.line.me/";
        private const string LINE_REPLY_PATH = "v2/bot/message/reply";

        private static readonly string CHANNEL_SECRET = System.Configuration.ConfigurationManager.AppSettings["CHANNEL_SECRET"];
        private static readonly string CHANNEL_ACCESS_TOKEN = System.Configuration.ConfigurationManager.AppSettings["CHANNEL_ACCESS_TOKEN"];

        [HttpPost]
        public HttpResponseMessage Post([FromBody]WebhookEvent e)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            if(e.events == null || e.events.Length == 0) { return response; }
            Event target = e.events.First();

            // verify signature
            var request = Request;
            var headers = request.Headers;
            
            if (!headers.Contains(X_LINE_SIGNATURE)) { return response; }
            var signature = headers.GetValues(X_LINE_SIGNATURE).First();

            if (!SignatureVerifier.verify(CHANNEL_SECRET, (string)(request.Properties["body"]), signature)) { return response; }
           

            // create contents(now echo only)
            var text = ContentsCreator.CreateReply(target);


            // post reply
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(LINE_HOST);
                client.DefaultRequestHeaders
                    .Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CHANNEL_ACCESS_TOKEN);
                var r = client.PostAsJsonAsync(LINE_REPLY_PATH, text).Result;
            }

            return response;
        }
    }
}
