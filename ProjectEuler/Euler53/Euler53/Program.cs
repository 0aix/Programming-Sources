using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler53
{
    class Program
    {
        public static byte[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

        static bool Divide(int[] n, int[] d)
        {
            double quo = 1000000;
            for (int x = 0; x < 25; x++)
            {
                int power = n[x] - d[x];
                while (power > 0)
                {
                    if ((double)primes[x] > quo)
                        return true;
                    quo /= (double)primes[x];
                    power--;
                }
            }
            return false;
        }

        static int[] Convert(int a)
        {
            int[] set = new int[25];
            for (int x = 0; x < 25; x++)
            {
                while (a % primes[x] == 0)
                {
                    set[x]++;
                    a /= primes[x];
                }
            }
            return set;
        }

        static int[] Multiply(int[] a, int[] b)
        {
            int[] set = new int[25];
            for (int x = 0; x < 25; x++)
            {
                set[x] = a[x] + b[x];
            }
            return set;
        }

        static int[] Factorial(int i)
        {
            int[] set = new int[25];
            for (; i > 1; i--)
            {
                set = Multiply(set, Convert(i));
            }
            return set;
        }

        static void Main(string[] args)
        {
            int count = 0;
            for (int n = 23; n <= 100; n++)
            {
                for (int r = 1; r < n; r++)
                {
                    int[] nfr = new int[25];
                    for (int x = n - r + 1; x <= n; x++)
                    {
                        nfr = Multiply(nfr, Convert(x));
                    }
                    int[] fact = Factorial(r);
                    if (Divide(nfr, Factorial(r)))
                        count++;
                }
            }
            Console.WriteLine(count);
            Console.ReadLine();
        }
    }
}
