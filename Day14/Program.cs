using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {

        static int index = 0;
        static string salt = "yjdafjpo";
        static string saltTest = "abc";
        public static void Main(string[] args)
        {
            var nrKeysFound = 0;
            while (nrKeysFound < 64) {
                using (MD5 md5Hash = MD5.Create())
                {
                    var source = salt + index;                   
                    var hash = GetMd5Hash(md5Hash, source);
//Console.WriteLine("hash: " + hash);
                    var tripletChar = GetNrInRowChar(hash, 3);
                    if (tripletChar != null) {
                        //Console.WriteLine("Found possible key. tripletChar: " + tripletChar + ", index: " + index + ", hash: " + hash);

                        // Om inom nästa 1000 finns någon som har tripletChar 5 ggr i rad så 
                        var nrOfMatchesFound = 0;
                        for (var j = 1; j <= 1000; j++) { 
                            source = salt + (index + j);                                     
                            hash = GetMd5Hash(md5Hash, source);
//Console.WriteLine("subhash: " + hash);
                            
                            var fivepletChar = GetNrInRowCharMatchingChar(hash, 5, tripletChar);
                            if (fivepletChar != null) { 
                                nrOfMatchesFound++;
                                Console.WriteLine("Maybe key found! fivepletChar: " + fivepletChar + ", index: " + index + ", hash: " + hash + ", index: " + index + ", j: " + j);
                            }
                        }

                        if (nrOfMatchesFound == 1) {
                            nrKeysFound++;
                            Console.WriteLine(">>Found key!");
                        } else if (nrOfMatchesFound > 1) {
                            Console.WriteLine("-- Too many this round!");
                        }
                    }

                    index++;
                }
            }

            Console.WriteLine("index for 64rd key: " + (index-1));
        }

        static string GetNrInRowChar(string hash, int nrInRow) {
            var currChar = hash[0];
            var charCounter = 1;
            for (var i = 1; i < hash.Length; i++) {
                if (hash[i] == currChar ) {
                    charCounter++;

                    if (charCounter == nrInRow) {
                        return currChar + "";
                    }
                } else {
                    currChar = hash[i];
                    charCounter = 1;
                }
            }

            return null;
        }
        static string GetNrInRowCharMatchingChar(string hash, int nrInRow, string charToMatch) {
            var charCounter = 0;
            for (var i = 0; i < hash.Length; i++) {
                if (hash[i] == charToMatch[0]) {
                    charCounter++;

                    if (charCounter == nrInRow) {
                        return hash[i] + "";
                    }
                } else {
                    charCounter = 0;
                }
            }

            return null;
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
