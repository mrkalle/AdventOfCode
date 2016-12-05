using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {

        static int index = 0;
        static string startword = "reyedfim";
        public static void Main(string[] args)
        {
            var result = "";
            var passwordLength = 8;
            var nrsFound = 0;
            while (nrsFound < passwordLength) {
                using (MD5 md5Hash = MD5.Create())
                {
                    var source = startword + index;
                    index++;
                    var hash = GetMd5Hash(md5Hash, source);
                    if (hash.StartsWith("00000")) {
                        result += hash[5];
                        nrsFound++;
                        
                        Console.WriteLine("index: " + index + ", nrFound: " + nrsFound);
                    }
                }
            }

            Console.WriteLine("password: " + result);
        }

        
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
