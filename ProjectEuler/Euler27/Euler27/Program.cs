using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler27
{
    class Program
    {
        static int Quad(int n, int a, int b)
        {
            return (n * n) + (a * n) + b;
        }

        static void Main(string[] args)
        {
            bool[] composites = new bool[1000000];
            for (int x = 2; x < 1000000; x++)
            {
                if (!composites[x])
                {
                    for (int z = x + x; z < 1000000; z += x)
                    {
                        composites[z] = true;
                    }
                }
            }
            int ax = -999;
            int bx = -999;
            int streak = 0;
            for (int a = -999; a < 1000; a++)
            {
                for (int b = 2; b < 1000; b++)
                {
                    if(!composites[b])
                    {
                        int count = 0;
                        for(int n = 0; ; n++)
                        {
                            int i = Quad(n, a, b);
                            if (i < 0)
                                break;
                            if (!composites[i])
                                count++;
                            else
                                break;
                        }
                        if (count > streak)
                        {
                            ax = a;
                            bx = b;
                            streak = count;
                        }
                    }
                }
            }
            Console.WriteLine(ax * bx);
            Console.ReadLine();
        }
    }
}
