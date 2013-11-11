using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler37
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] composites = new bool[1000000];
            List<int> primes = new List<int>();
            composites[0] = true;
            composites[1] = true;
            for (int x = 2; x < 1000000; x++)
            {
                if (!composites[x])
                {
                    primes.Add(x);
                    for (int z = x + x; z < 1000000; z += x)
                    {
                        composites[z] = true;
                    }
                }
            }
            int sum = 0;
            for (int x = 4; x < primes.Count; x++)
            {
                string num = primes[x].ToString();
                bool success = true;
                for (int n = 1; n < num.Length; n++)
                {
                    if (composites[int.Parse(num.Substring(0, n))])
                    {
                        success = false;
                        break;
                    }
                }
                if (!success)
                    continue;
                for (int n = 1; n < num.Length; n++)
                {
                    if (composites[int.Parse(num.Substring(n, num.Length - n))])
                    {
                        success = false;
                        break;
                    }
                }
                if (!success)
                    continue;
                else
                    sum += primes[x];
            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
