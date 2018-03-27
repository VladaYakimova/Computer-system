using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, double> frequencies = new Dictionary<char, double>();

            if (args.Length != 1)
            {
                Console.WriteLine("Wrong input parameters");
                return;
            }
            GetFrequencies(frequencies, args[0]);
            double enth = Enthropy(frequencies);
            double infoAmount = enth * frequencies.Keys.Count;




            PrintFrequencies(frequencies);
            Console.WriteLine("Enthropy: " + enth + "\n");
            Console.WriteLine("Info amount: " + infoAmount + "\n");
            Console.WriteLine("File size: " + new FileInfo(args[0]).Length);
        }
        static void GetFrequencies(Dictionary<char, double> frequencies, string pathToFile)
        {
            int length = 0;
            using (StreamReader sr = new StreamReader(new FileStream(pathToFile, FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    length += line.Length;
                    for (int i = 0; i < line.Length; ++i)
                    {
                        double val;
                        if (frequencies.TryGetValue(line[i], out val))
                        {
                            frequencies[line[i]] = val + 1.0;
                        }
                        else
                        {
                            frequencies.Add(line[i], 1.0);
                        }
                    }
                }
            }
            List<char> keys = new List<char>(frequencies.Keys);
            for (int i = 0; i < keys.Count; ++i)
            {
                frequencies[keys[i]] /= length;
            }
        }

        static double Enthropy(Dictionary<char, double> frequencies)
        {
            double enthropy = 0.0;
            foreach (char key in frequencies.Keys)
            {
                enthropy += frequencies[key] * Math.Log(frequencies[key], 2.0);
            }
            return -enthropy;
        }

        static void PrintFrequencies(Dictionary<char, double> frequencies)
        {
            foreach (char key in frequencies.Keys)
            {
                Console.WriteLine(key + " : " + frequencies[key]);
            }
            Console.WriteLine();
        }
    }
}
