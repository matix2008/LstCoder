using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LstCoder
{
    public class Coder
    {
        private HashAlgorithm _hash = null;

        public Coder(string src, string dest, string mask, string method)
        {
            SourceName = src;
            DestName = dest;
            LineMask = mask;
            Method = method;

            switch (Method)
            {
                default: break;
                case "SHA1": _hash = SHA1.Create(); break;
                case "SHA256": _hash = SHA256.Create(); break;
                case "MD5": _hash = MD5.Create(); break;
            }
        }

        public string SourceName { get; }
        public string DestName { get; }
        public string LineMask { get; }
        public string Method { get; }

        public void Go()
        {
            using (var reader = new StreamReader(SourceName))
            {
                using (StreamWriter outputFile = new StreamWriter(DestName, false))
                { 
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string result;

                        if (LineMask != "---")
                            result = Regex.Replace(line, LineMask, EncodeLine);
                        else
                            result = EncodeLine(line);

                        outputFile.WriteLine(result);
                    }
                }
            }
        }

        protected string EncodeLine(Match match)
        {
            return EncodeLine(match.Value);
        }

        protected virtual string EncodeLine(string line)
        {
            string result = line;

            if (_hash != null)
            {
                StringBuilder sb = new StringBuilder();
                Encoding enc = Encoding.UTF8;
                Byte[] result_hash = _hash.ComputeHash(enc.GetBytes(result));

                foreach (Byte b in result_hash)
                    sb.Append(b.ToString("x2"));

                result = sb.ToString();
            }

            return result;
        }
    }
}
