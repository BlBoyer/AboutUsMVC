#nullable enable
using System.Security.Cryptography;
using System.Text;
namespace AboutUs.Services
{
    class SecureAddress
    {
        private static HashAlgorithm sha = SHA256.Create();
        //method to get byte[] from string
        private byte[] getHash(string keyString)
        {
            return Encoding.ASCII.GetBytes(keyString);
        }
        //method to get byte arrays, and turn into hash, and return our final string for storage
        public string mesh(string stringIn1, string stringIn2)
        {
            if (stringIn1.Length>0 && stringIn2.Length>0){
                //byte[] arr1 = getHash(stringIn1);
                //byte[] arr2 = getHash(stringIn2);
                //string stringValue = arr1.ToString() + arr2.ToString();
                string stringValue = stringIn1 + stringIn2;
                byte[] resultHash = sha.ComputeHash(getHash(stringValue));
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < resultHash.Length; i++)  
                {  
                    result.Append(resultHash[i].ToString("x2")); 
                }
                return result.ToString();
            }
            else return "No input detected...";
        }
        public string singleCode(string stringIn)
        {
            if (stringIn.Length>0){
                byte[] resultHash = Encoding.ASCII.GetBytes(stringIn);
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < resultHash.Length; i++)  
                {  
                    result.Append(resultHash[i].ToString("x2")); 
                }
                Console.WriteLine($"Encoded :{result.ToString()}");
                return result.ToString();
            }
            else return "No input Detected...";
        }
        public string decodeUrl(string stringIn)
        {
            var output = new StringBuilder();
            for (int i=0;i<stringIn.Length-1;i++)
            {
                string byteChars = $"{stringIn[i]}{stringIn[i+1]}";
                output.Append((char)Convert.ToByte(byteChars, 16));
                i+=1;
            }
                Console.WriteLine($"Decoded: {output.ToString()}");
            return output.ToString();
        }
    }
}