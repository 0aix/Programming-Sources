using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler10
{
    class Program
    {
        //static List<int> primes = new List<int>();
        static Boolean[] primes = new Boolean[2000000];
        static List<int> plist = new List<int>();
        static void Main(string[] args)
        {
            for (int x = 2; x < 2000000; x++)
            {
                if (primes[x] == false)
                {
                    plist.Add(x);
                    for (int z = x + x; z < 2000000; z+=x)
                    {
                        primes[z] = true;
                    }
                }
            }
            long sum = 0;
            for (int x = 0; x < plist.Count; x++)
            {
                sum += plist[x];
            }
            Console.WriteLine(sum.ToString());
            Console.ReadLine();
        }
    }
}
