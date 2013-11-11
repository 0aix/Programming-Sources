using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler29
{
    class Program
    {
        public static Byte[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
        public static Byte[,] set = new Byte[101,25];
        public static List<int[]> list = new List<int[]>();

        static void Main(string[] args)
        {
            for (int x = 2; x <= 100; x++)
            {
                int n = x;
                for (int i = 0; primes[i] <= n; i++)
                {
                    while (n % primes[i] == 0 && primes[i] <= n)
                    {
                        set[x,i]++;
                        n = n / primes[i];
                    }
                    if (n == 1)
                        break;
                }
            }
            for (int x = 2; x <= 100; x++)
            {
                for (int y = 2; y <= 100; y++)
                {
                    int[] tempset = new int[25];
                    for (int i = 0; i < 25; i++)
                    {
                        tempset[i] = set[x, i] * y;
                    }
                    bool add = true;
                    for (int i = 0; i < list.Count; i++)
                    {
                        int shared = 0;
                        for (int n = 0; n < 25; n++)
                        {
                            if (tempset[n] == list[i][n])
                                shared++;
                        }
                        if (shared == 25)
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add)
                        list.Add(tempset);
                }
            }
            Console.WriteLine(list.Count);
            Console.ReadLine();
        }
    }
}
