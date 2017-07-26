using LineBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBot.Util
{
    public class ContentsCreator
    {
        public static RequestBodyForReplyMessage CreateReply(Event e)
        {
            if (!e.type.Equals("message") || !e.message.type.Equals("text")) { return null; }

            var text = new RequestBodyForReplyMessage();
            text.replyToken = e.replyToken;
            text.messages = new SendMessageObject[] { new SendMessageObject { type = "text", text = e.message.text } };
            return text;
        }
    }
}