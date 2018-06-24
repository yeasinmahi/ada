using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using AkijRest.IdentityServer.Models;
using AkijRest.SolutionConstant;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace AkijRest.IdentityServer.Helpers
{
    public static class AudienceStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList
            = new ConcurrentDictionary<string, Audience>();

        static AudienceStore()
        {
            AudiencesList.TryAdd
            (
                AudienceConstant.ClientId,
                new Audience
                {
                    ClientId = AudienceConstant.ClientId,
                    Base64Secret = AudienceConstant.Base64Secret,
                    Name = "ResourceServer.Api 1"
                }
            );
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience
            {
                ClientId = clientId,
                Base64Secret = base64Secret,
                Name = name
            };

            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }


        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;

            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }

            return null;
        }
    }
}