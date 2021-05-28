using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Week_6
{
    public enum CryptoAlgorithm { AES, RSA };
    public class Crypto
    {
        AesCryptoServiceProvider aes;
        //RSACryptoServiceProvider res;

        CryptoAlgorithm mType;


        public void Initialize(CryptoAlgorithm type)
        {
            mType = type;
            //if
            aes = new AesCryptoServiceProvider();
        }

        public void Terminate()
        {
            if(aes != null)
            {
                aes.Dispose();
            }

            //Do the same for rsa
        }

        public byte[] Encrypt(byte[] data)
        {
            if(mType == CryptoAlgorithm.AES)
            {
                if(data == null || data.Length == 0)
                {
                    return null;
                }

                using(var ms = new MemoryStream())
                {
                    using(var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                //Here you will have to do for RSA
                return null;
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            if (mType == CryptoAlgorithm.AES)
            {
                if (data == null || data.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                //Here you will have to do for RSA
                return null;
            }
        }
    }
}
