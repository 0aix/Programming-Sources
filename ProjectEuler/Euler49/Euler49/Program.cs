using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler49
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] composite = new bool[10000];
            List<int> primes = new List<int>();
            for (int x = 2; x < 10000; x++)
            {
                if (!composite[x])
                {
                    if (x >= 1000)
                        primes.Add(x);
                    for (int i = 0; i < 10000; i++)
                    {
                        if (i % x == 0)
                            composite[i] = true;
                    }
                }
            }
            List<int> clear = new List<int>();
            for (int i = 0; i < primes.Count; i++)
            {
                string str = primes[i].ToString();
                List<int> chain = new List<int>();
                for (int a = 0; a < 4; a++)
                {
                    for (int b = 0; b < 4; b++)
                    {
                        if (b == a)
                            continue;
                        for (int c = 0; c < 4; c++)
                        {
                            if (c == a || c == b)
                                continue;
                            for (int d = 0; d < 4; d++)
                            {
                                if (d == a || d == b || d == c)
                                    continue;
                                int num = int.Parse(str[a].ToString() + str[b].ToString() + str[c].ToString() + str[d].ToString());
                                if (clear.Contains(num))
                                    break;
                                if (primes.Contains(num) && !chain.Contains(num))
                                    chain.Add(num);
                            }
                        }
                    }
                }
                if (chain.Count >= 3)
                {
                    chain.Sort();
                    for (int x = 0; x < chain.Count - 2; x++)
                    {
                        for (int y = x + 1; y < chain.Count - 1; y++)
                        {
                            for (int z = y + 1; z < chain.Count; z++)
                            {
                                if (chain[x] - chain[y] == chain[y] - chain[z])
                                {
                                    clear.AddRange(chain);
                                    Console.WriteLine(chain[x].ToString() + chain[y].ToString() + chain[z].ToString());
                                }
                            }
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
