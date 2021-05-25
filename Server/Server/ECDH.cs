using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Server
{
    public class ECDH
    {
        internal static ECDiffieHellmanCng ECDHProvider;
        public static void GeneratePublicKey(out byte[] PublicKey)
        {
            ECDHProvider.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            ECDHProvider.HashAlgorithm = CngAlgorithm.Sha256;
            PublicKey = ECDHProvider.PublicKey.ToByteArray();
        }

        public static void GeneratePrivateKey(byte[] FirstPublicKey, byte[] SecondPublicKey, out byte[] PrivateKey)
        {
            CngKey SecondKey = CngKey.Import(SecondPublicKey, CngKeyBlobFormat.EccPublicBlob);
            PrivateKey = ECDHProvider.DeriveKeyMaterial(SecondKey);
        }

        public static byte[] Encryption(string Text, byte[] PrivateKey, out byte[] IV)
        {
            using (Aes AESProvider = new AesCryptoServiceProvider())
            {
                AESProvider.Key = PrivateKey;
                IV = AESProvider.IV;
                using (MemoryStream CipherText = new MemoryStream())
                using (CryptoStream CS = new CryptoStream(CipherText, AESProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Encoding.Unicode.GetBytes(Text);
                    CS.Write(plaintextMessage, 0, plaintextMessage.Length);
                    CS.Close();
                    return CipherText.ToArray();
                }
            }
        }

        public static string Decryption(byte[] EncryptedText, byte[] PrivateKey, byte[] IV)
        {
            using (Aes AESProvider = new AesCryptoServiceProvider())
            {
                AESProvider.Key = PrivateKey;
                AESProvider.IV = IV;
                using (MemoryStream PlainText = new MemoryStream())
                {
                    using (CryptoStream CS = new CryptoStream(PlainText, AESProvider.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        CS.Write(EncryptedText, 0, EncryptedText.Length);
                        CS.Close();
                        return Encoding.Unicode.GetString(PlainText.ToArray());
                    }
                }
            }
        }
    }
}
