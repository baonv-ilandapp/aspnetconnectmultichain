using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConnectMultiChain.Models
{
    public class KeyVault
    {
        public static async Task<string> AuthenticateVault(string authority, string resource, string scope)
        {
            var clientCredential = new ClientCredential("99d24e89-6d30-4fbf-bd14-cef2261f3db0", "w416febbAXv7kZKm0AoC439J2N17zYCkDzRevo1WCzA=");
            var authenticationContext = new AuthenticationContext(authority);
            var resutl = await authenticationContext.AcquireTokenAsync(resource, clientCredential);
            return resutl.AccessTokenType;
        }
    }
}