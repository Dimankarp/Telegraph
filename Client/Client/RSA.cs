using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
   public class RSA
    {

       public static  void Generate(out RSAParameters PublicKey, out RSAParameters PrivateKey)
        {
            using (RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(2048))
            {
                PublicKey = RSAProvider.ExportParameters(false);
                PrivateKey = RSAProvider.ExportParameters(true);
            }
        }



        public static byte[] Encryption(string Text, RSAParameters PublicKey)
        {
            byte[] Data = Encoding.Unicode.GetBytes(Text);
            using (RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(2048))
            {
                RSAProvider.ImportParameters(PublicKey);
                Data = RSAProvider.Encrypt(Data, false);
            }
            return Data;
        }

        public static string Decryption(byte[] Text, RSAParameters PrivateKey)
        {
            using (RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(2048))
            {
                RSAProvider.ImportParameters(PrivateKey);
                Text = RSAProvider.Decrypt(Text, false);
            }
            return Encoding.Unicode.GetString(Text);
        }





    }


    }

