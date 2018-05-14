using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;

namespace ConnectMultiChain.Models
{
    public class KeyVaultCrypto : IKeyVaultCrypto
    {
        private readonly KeyVaultClient client;
        private readonly string keyId;

        public KeyVaultCrypto(KeyVaultClient client, string keyId)
        {
            this.client = client;
            this.keyId = keyId;
        }

        public async Task<string> DecryptAsync(string encryptedText)
        {
            //var encryptedBytes = Convert.FromBase64String(encryptedText);
            var encryptedBytes = Encoding.Unicode.GetBytes(encryptedText);
            var decryptionResult = await client.DecryptAsync(keyId, "RSA-OAEP", encryptedBytes);
            
            var decryptedText = Encoding.Unicode.GetString(decryptionResult.Result);
            return decryptedText;
        }

        public async Task<string> EncryptDecryptAsync(string value)
        {
            try
            {
                var result = await client.EncryptAsync(keyId, "RSA-OAEP", Encoding.Unicode.GetBytes(value));
                var encryptString = Encoding.ASCII.GetString(result.Result);

                var decrypt = await client.DecryptAsync(keyId, "RSA-OAEP", result.Result);
                var message = Encoding.Unicode.GetString(decrypt.Result);

                return message;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            
        }

        public async Task<string> CreateKey()
        {
            // Let's create a secret and read it back

            string vaultBaseUrl = "https://testingconnectaspnet.vault.azure.net";

            var key = await client.CreateKeyWithHttpMessagesAsync(vaultBaseUrl, "DemoCreateKeyFromAspNet", "RSA");
            var keyIdentifier = key.Body.KeyIdentifier;

            // Print indented JSON response
            return keyIdentifier.Identifier;
        }

        public async Task<string> GetKey()
        {
            var bundle = await client.GetKeyAsync(keyId);
            var key = bundle.Key;
            return key.ToString();
        }

        public async Task<string> Sign(string value)
        {
            string vaultBaseUrl = "https://testingconnectaspnet.vault.azure.net";
            var result = await client.SignAsync(keyId, "EC", Encoding.Unicode.GetBytes(value));
            return result.ToString();
        }

        public Task<string> Verify()
        {
            throw new NotImplementedException();
        }
    }
}