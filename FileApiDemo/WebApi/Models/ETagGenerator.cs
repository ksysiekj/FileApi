using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Models
{
    public static class ETagGenerator
    {
        private static readonly Encoder StringEncoder = Encoding.UTF8.GetEncoder();

        public static string GeneratorETag(DateTime modifyDate)
        {
            //use file name and modify date as the unique identifier
            string fileString = modifyDate.ToString("d", CultureInfo.InvariantCulture);
            //get string bytes
            byte[] stringBytes = new byte[StringEncoder.GetByteCount(fileString.ToCharArray(), 0, fileString.Length, true)];
            StringEncoder.GetBytes(fileString.ToCharArray(), 0, fileString.Length, stringBytes, 0, true);
            //hash string using MD5 and return the hex-encoded hash
            using (MD5CryptoServiceProvider md5Enc = new MD5CryptoServiceProvider())
            {
                return "\"" + BitConverter.ToString(md5Enc.ComputeHash(stringBytes)).Replace("-", string.Empty) + "\"";
            }
        }
    }
}