using System.Threading.Tasks;

namespace ConnectMultiChain.Models
{
    public interface IKeyVaultCrypto
    {
        Task<string> DecryptAsync(string encryptedText);
        Task<string> EncryptDecryptAsync(string value);
        Task<string> CreateKey();
        Task<string> GetKey();
        Task<string> Sign(string value);
        Task<string> Verify();
    }
}