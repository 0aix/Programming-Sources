using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler57
{
    class Program
    {
        static void Main(string[] args)
        {
            int total = 0;
            UInt64 numerator = 1;
            UInt64 denominator = 1;

            for (int i = 0; i < 1000; i++)
            {
                denominator += numerator;
                numerator = denominator - numerator + denominator;

                int nlength = numerator.ToString().Length;
                int dlength = denominator.ToString().Length;

                if (nlength > dlength)
                    total++;

                //Use significant digits to lower digit count
                if (nlength > 15 && dlength > 15)
                {
                    numerator /= 10;
                    denominator /= 10;
                }
            }
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
