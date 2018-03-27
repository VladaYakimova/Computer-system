using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Base64Encode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Неправильний синтаксис. \n Правильно ------ Base64Encode.exe fileOut fileIn");
                return;
            }
            Base64(args[0], args[1]);
        }
        static void Base64(string fileOut, string fileIn)
        {
            const string base64chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            List<byte> chars = new List<byte>();
            int c;
            int tail = 0;
            using (BinaryReader br = new BinaryReader(new FileStream(fileOut, FileMode.Open)))
            {
                chars = new List<byte>(br.ReadBytes((int)br.BaseStream.Length));
            }
            c = chars.Count % 3;
            if (c > 0)
            {
                for (; c < 3; c++)
                {
                    chars.Add(0);
                    ++tail;
                }
            }

            string res = "";

            for (int i = 0; i < chars.Count; i += 3)
            {
                res += base64chars[chars[i] >> 2];

                int b2 = (chars[i] & 3) << 4;
                b2 |= chars[i + 1] >> 4;
                res += base64chars[b2];

                int b3 = (chars[i + 1] & 15) << 2;
                b3 |= chars[i + 2] >> 6;
                res += base64chars[b3];

                res += base64chars[chars[i + 2] & 63];
            }

            res = res.Substring(0, res.Length - tail);

            for (int i = 0; i < tail; ++i)
                res += '=';

            using (StreamWriter sw = new StreamWriter(new FileStream(fileIn, FileMode.Create)))
            {
                sw.Write(res);
            }
        }
    }
}

