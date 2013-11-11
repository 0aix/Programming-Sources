using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler40
{
    class Program
    {
        static int Digit(int i)
        {
            int x = 0;
            int y = 0;
            for (int n = 0; ; n++)
            {
                x *= 10;
                x += (int)(9 * Math.Pow(10, n));
                y = (int)(Math.Pow(10, n) - 1);
                if (i <= x)
                {
                    int pos = i / (n + 1);
                    int rem = i % (n + 1);
                    int num = y + pos;
                    if (rem == 0)
                        rem += n;
                    else
                    {
                        num++;
                        rem--;
                    }
                    char digit = num.ToString()[rem];
                    return int.Parse(digit.ToString());
                }
                i -= x;
            }
        }

        static void Main(string[] args)
        {
            int product = 1;
            for (int i = 1; i <= 1000000; i*=10)
            {
                product *= Digit(i);
            }
            Console.WriteLine(product);
            Console.ReadLine();
        }
    }
}
