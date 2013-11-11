using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler7
{
    class Program
    {
        static List<int> primes = new List<int>();
        static void Main(string[] args)
        {
            primes.Add(2);
            for (int x = 3; primes.Count < 10001; x++)
            {
                bool fail = false;
                for (int z = 0; z < primes.Count; z++)
                {
                    if (x % primes[z] == 0)
                    {
                        fail = true;
                        break;
                    }
                }
                if (!fail)
                    primes.Add(x);
            }
            Console.WriteLine(primes[10000].ToString());
            Console.ReadLine();
        }
    }
}
