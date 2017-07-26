using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBot.Models
{
    public class RequestBodyForReplyMessage
    {
        public string replyToken { get; set; }

        public SendMessageObject[] messages { get; set; }
    }

    public class SendMessageObject
    {
        public string type { get; set; }

        public string text { get; set; }
    }
}