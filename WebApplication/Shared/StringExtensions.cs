using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication.Shared
{
    public static class StringExtensions
    {
        public static string Encrypt(this string toEncrypt, string key, string iv)
        {
            try
            {
                var keyArray = Encoding.UTF8.GetBytes(key);
                var ivArray = Encoding.UTF8.GetBytes(iv);
                var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                var rDel = new RijndaelManaged
                {
                    Key = keyArray, IV = ivArray, Mode = CipherMode.CBC, Padding = PaddingMode.Zeros
                };
                var cTransform = rDel.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static string Decrypt(this string toDecrypt, string key, string iv)
        {
            try
            {
                var keyArray = Encoding.UTF8.GetBytes(key);
                var ivArray = Encoding.UTF8.GetBytes(iv);
                var toEncryptArray = Convert.FromBase64String(toDecrypt);
                var rDel = new RijndaelManaged
                {
                    Key = keyArray, IV = ivArray, Mode = CipherMode.CBC, Padding = PaddingMode.Zeros
                };
                var cTransform = rDel.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}