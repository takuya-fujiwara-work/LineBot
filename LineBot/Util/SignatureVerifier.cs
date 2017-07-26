using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LineBot.Util
{
    public static class SignatureVerifier
    {
        public static bool verify(string channelSecret, string body, string signature)
        {
            HMACSHA256 sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(channelSecret));
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(body));
            string calcSignature = Convert.ToBase64String(hash);

            return calcSignature.Equals(signature);
        }
    }
}