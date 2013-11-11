using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler60
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] composites = new bool[10000];
            List<int> primes = new List<int>();

            for (int x = 2; x < 10000; x++)
            {
                if (composites[x] == false)
                {
                    primes.Add(x);
                    for (int z = x + x; z < 10000; z += x)
                        composites[z] = true;
                }
            }

            primes.Remove(2);
            primes.Remove(5);

            List<int> a = new List<int>();
            List<int> b = new List<int>();

            for (int i = 0; i < primes.Count; i++)
            {
                for (int n = 0; n < primes.Count; n++)
                {
                    if (prime(concat(primes[i], primes[n])))
                    {
                        a.Add(primes[i]);
                        b.Add(primes[n]);
                    }
                }
            }
            Console.ReadLine();
        }

        static int concat(int a, int b)
        {
            for (int i = b; i > 0; i /= 10)
                a *= 10;
            return a + b;
        }

        static bool prime(int n)
        {
            //if (n < 2)
            //    return false;
            //if (n == 2)
            //    return true;
            if (n % 2 == 0)
                return false;
            //if (n < 9)
            //    return true;
            if (n % 3 == 0)
                return false;

            int c = 5;
            while (c * c <= n)
            {
                if (n % c == 0)
                    return false;
                if (n % (c + 2) == 0)
                    return false;

                c += 6;
            }

            return true;
        }
    }
}
