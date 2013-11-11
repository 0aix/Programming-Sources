using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler45
{
    class Program
    {
        static long Triangle(long n)
        {
            return (n * (n + 1)) / 2;
        }

        static long Pentagon(long n)
        {
            return (n * (3 * n - 1)) / 2;
        }

        static long Hexagon(long n)
        {
            return (n * (2 * n - 1));
        }

        static void Main(string[] args)
        {
            List<long> tri = new List<long>();
            List<long> pen = new List<long>();
            List<long> hex = new List<long>();
            for (long n = 0; n < 10000000; n++)
            {
                tri.Add(Triangle(n));
                pen.Add(Pentagon(n));
                hex.Add(Hexagon(n));
            }
            for (int t = 0; t < 10000000; t++)
            {
                for (int p = 0; p < 10000000; p++)
                {
                    if (tri[t] < pen[p])
                        break;
                    if (tri[t] > pen[p])
                        continue;
                    for (int h = 0; h < 10000000; h++)
                    {
                        if (pen[p] < hex[h])
                            break;
                        else if (pen[p] > hex[h])
                            continue;
                        else
                            Console.WriteLine(tri[t]);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
