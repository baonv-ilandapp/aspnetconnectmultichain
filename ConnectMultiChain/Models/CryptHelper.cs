using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace ConnectMultiChain.Models
{
    public class CryptHelper
    {
        public static AsymmetricCipherKeyPair GenerateRSAKeyPair()
        {
            var gen = new RsaKeyPairGenerator();
            gen.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
            return gen.GenerateKeyPair();
        }

        public static string GetRSAPublicKey(AsymmetricCipherKeyPair keyPair)
        {
            using (TextWriter t = new StringWriter())
            {
                var writer = new PemWriter(t);
                writer.WriteObject(keyPair.Public);
                return t.ToString();
            }
        }

        public static string GetRSAPrivateKey(AsymmetricCipherKeyPair keyPair)
        {
            using (TextWriter t = new StringWriter())
            {
                var writer = new PemWriter(t);
                writer.WriteObject(keyPair.Private);
                return t.ToString();
            }
        }
    }
}