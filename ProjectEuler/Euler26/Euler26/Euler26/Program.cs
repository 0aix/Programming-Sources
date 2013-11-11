using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler26
{
    class Program
    {
        static void Main(string[] args)
        {
            int streak = 0;
            int num = 0;
            for(int x = 1; x < 1000; x++)
            {
                List<int> division = new List<int>();
                int n = 1;
                bool repeat = false;
                while (true)
                {
                    if (n % x == 0)
                        break;
                    if (x > n)
                        n *= 10;
                    n %= x;
                    n *= 10;
                    division.Add(n);
                    for (int i = 0; i < division.Count; i++)
                    {
                        for (int z = i + 1; z < division.Count; z++)
                        {
                            if (division[i] == division[z])
                            {
                                repeat = true;
                                if (z - i > streak)
                                {
                                    streak = z - i;
                                    num = x;
                                }
                                break;
                            }
                        }
                        if (repeat)
                            break;
                    }
                    if (repeat)
                        break;
                }
            }
            Console.WriteLine(num);
            Console.ReadLine();
        }
    }
}
