using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Class.MultichainLib;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Core;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace ConnectMultiChain.Controllers
{
    public class MultiChainController : Controller
    {
        // GET: MultiChain
        public ActionResult Index()
        {
            return View();
        }

        public string GetInfo(string rpcPassword, string ipAddress, int port, string chainName)
        {
            try
            {
                JsonRpcClient jsonRpcClient = new JsonRpcClient("multichainrpc", rpcPassword, ipAddress, port, chainName);
                Admin admin = new Admin(jsonRpcClient);
                string json = "";
                json = admin.GetInfo();
                return json;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public async Task<string> ConnectKeyVaultAsync()
        {
            var keyVaultClient = new KeyVaultClient(AuthenticateVaultAsync);
            var bundle = keyVaultClient.GetKeyAsync("https://testingconnectaspnet.vault.azure.net/keys/TestingConnect/d3f4f11bc2864a858bf0e2e93f05839c");
            //var result = await keyVaultClient.GetSecretAsync("https://testingconnectaspnet.vault.azure.net/secrets/DatabaseConnectingString/802972b885194e90ba82422ee3a880fc");
            //var connectionString = result.Value;
            //KeyIdentifier key = new KeyIdentifier("https://testingconnectaspnet.vault.azure.net/keys/TestingConnect/d3f4f11bc2864a858bf0e2e93f05839c");
            
            //key.

            RsaKey rsaKey = new RsaKey("https://testingconnectaspnet.vault.azure.net/keys/TestingConnect/d3f4f11bc2864a858bf0e2e93f05839c");
            var encrypt = rsaKey.EncryptAsync(Encoding.ASCII.GetBytes("baofc"));
            var stringEncrypt = Encoding.ASCII.GetString(encrypt.Result.Item1);
            var decypt = await rsaKey.DecryptAsync(encrypt.Result.Item1, encrypt.Result.Item2);
            
            string someString = Encoding.ASCII.GetString(decypt);

            

            //return connectionString;
            return someString;
        }

        public string CreateSecretKeyVaul()
        {
            // Let's create a secret and read it back

            string vaultBaseUrl = "https://testingconnectaspnet.vault.azure.net";
            string secret = "DemoCreateSecsetFromAspNet";

            // Await SetSecretAsync
            KeyVaultClient keyclient = new KeyVaultClient(AuthenticateVaultAsync);
            var result = keyclient.SetSecretAsync(vaultBaseUrl, secret, "This is the private key demo").Result;

            // Print indented JSON response
            string prettyResult = JsonConvert.SerializeObject(result, Formatting.Indented);

            // Read back secret
            string secretUrl = $"{vaultBaseUrl}/secrets/{secret}";
            var secretWeJustWroteTo = keyclient.GetSecretAsync(secretUrl).Result;
            return secretWeJustWroteTo.Value + "\n" + prettyResult;
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