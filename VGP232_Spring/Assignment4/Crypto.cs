using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Assignment4
{
    public enum CryptoAlgorithm { AES, RSA };

    public class Crypto
    {
        AesCryptoServiceProvider aes;
        RSACryptoServiceProvider rsa;
        CryptoAlgorithm mode;

        public string publicKeyPathRSA;
        public string privateKeyPathRSA;

        public void Initialize(CryptoAlgorithm mMode)
        {
            mode = mMode;
            if(mode == CryptoAlgorithm.AES)
            {
                aes = new AesCryptoServiceProvider();
            }
            else
            {
                rsa = new RSACryptoServiceProvider();
            }
        }

        public void Terminate()
        {
            if (aes != null)
            {
                aes.Dispose();
            }

            else if (rsa != null)
            {
                rsa.Dispose();
            }
        }

        public void ExportPrivateKey(string privateKeyPath)
        {
            rsa.PersistKeyInCsp = false;
            string privateKey = rsa.ToXmlString(true);
            using (var w = new StreamWriter(privateKeyPath))
            {
                w.WriteLine(privateKey);
            }
        }

        public void ExportPublicKey(string publicKeyPath)
        {
            rsa.PersistKeyInCsp = false;
            string publicKey = rsa.ToXmlString(false);
            using (var w = new StreamWriter(publicKeyPath))
            {
                w.WriteLine(publicKey);
            }
        }

        public void ExportAESKey(string path)
        {
            aes.GenerateKey();
            byte[] key = aes.Key;
            File.WriteAllBytes(path, key);
        }

        public void ExportAESIV(string path)
        {
            aes.GenerateIV();
            byte[] iv = aes.IV;
            File.WriteAllBytes(path, iv);
        }

        public void SaveK1(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                if (path == null || path.Length == 0)
                {
                    return;
                }

                else
                {
                    ExportPrivateKey(path);
                }
            }

            else
            {
                if (path == null || path.Length == 0)
                {
                    return;
                }

                else
                {
                    ExportAESKey(path);
                }
            }
        }

        public void SaveK2(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                if (path == null || path.Length == 0)
                {
                    return;
                }

                else
                {
                    ExportPublicKey(path);
                }
            }

            else
            {
                if (path == null || path.Length == 0)
                {
                    return;
                }

                else
                {
                    ExportAESIV(path);
                }
            }
        }

        public void LoadK1(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                string xmlString = File.ReadAllText(path);
                rsa.FromXmlString(xmlString);
            }

            else
            {
                byte[] sharedKey = File.ReadAllBytes(path);
                aes.Key = sharedKey;
            }
        }

        public void LoadK2(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                string xmlString = File.ReadAllText(path);
                rsa.FromXmlString(xmlString);
            }

            else
            {
                byte[] iv = File.ReadAllBytes(path);
                aes.IV = iv;
            }
        }

        public byte[] Encrypt(byte[] input)
        {
            if (mode == CryptoAlgorithm.AES)
            {
                if (input == null || input.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }

            else
            {
                try
                {
                    byte[] encryptedData;
                    rsa.PersistKeyInCsp = false;
                    string publicKey = File.ReadAllText(publicKeyPathRSA);
                    rsa.FromXmlString(publicKey);
                    encryptedData = rsa.Encrypt(input, false);
                    return encryptedData;
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public byte[] Decrypt(byte[] input)
        {
            if (mode == CryptoAlgorithm.AES)
            {
                if (input == null || input.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }

            else
            {
                try
                {
                    byte[] decryptedData;
                    rsa.PersistKeyInCsp = false;
                    byte[] privateKey = File.ReadAllBytes(privateKeyPathRSA);
                    string privateKey2 = Encoding.UTF8.GetString(privateKey);
                    rsa.FromXmlString(privateKey2);
                    decryptedData = rsa.Decrypt(input, true);
                    return decryptedData;
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
