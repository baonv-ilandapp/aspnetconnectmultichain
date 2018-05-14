using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConnectMultiChain.Models;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConnectMultiChain.Controllers
{
    public class KeyvaultController : Controller
    {
        private readonly IKeyVaultCrypto _keyVaultCrypto;

        public KeyvaultController()
        {
            var keyVaultClient = new KeyVaultClient(AuthenticateVaultAsync);
            var keyId = "https://testingconnectaspnet.vault.azure.net/keys/CreateKeyFromAspNet/64b353768f334e21a54213168dfe4855";
            _keyVaultCrypto = new KeyVaultCrypto(keyVaultClient,keyId);
        }

        public async Task<string> EncryptData()
        {
            var encrypt = await _keyVaultCrypto.EncryptDecryptAsync("vanbao");
            return encrypt;
        }

        public async Task<string> GetKey()
        {
            var key = await _keyVaultCrypto.GetKey();
            return key;
        }
        public async Task<string> DecryptData(string encrypt)
        {
            var decrypt = await _keyVaultCrypto.DecryptAsync(encrypt);
            return decrypt;
        }

        public async Task<string> CreateKeyFromAspNet()
        {
            var identifier = await _keyVaultCrypto.CreateKey();
            return identifier;
        }
        private async Task<string> AuthenticateVaultAsync(string authority, string resource, string scope)
        {
            var clientCredential = new ClientCredential("99d24e89-6d30-4fbf-bd14-cef2261f3db0", "w416febbAXv7kZKm0AoC439J2N17zYCkDzRevo1WCzA=");
            var authenticationContext = new AuthenticationContext(authority);
            var resutl = await authenticationContext.AcquireTokenAsync(resource, clientCredential);
            return resutl.AccessToken;
        }
    }
}