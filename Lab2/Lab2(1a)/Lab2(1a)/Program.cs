using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakimova
{
    class Program
    {
        static void Main(string[] args)
        {
            int multiplicand = Int32.Parse(Console.ReadLine());
            int multiplier = Int32.Parse(Console.ReadLine());
            Console.WriteLine(Multiplication(multiplicand, multiplier));
            Console.ReadKey();
        }
        static long Multiplication(int multiplic, int multiplier)
        {
            bool isMultiplierLessThenZero = false;
            if (multiplier < 0)
            {
                isMultiplierLessThenZero = true;
                multiplier = ~multiplier + 1;
            }
            long product = 0;
            long multiplicand = multiplic;
            for (int i = 0; i < 32; i++)
            {
                if ((multiplier & 1) == 1)
                    product += multiplicand;
                multiplier >>= 1;
                multiplicand <<= 1;
            }

            if (isMultiplierLessThenZero)
            {
                product = ~product + 1;
            }
            return product;
        }
    }
}
