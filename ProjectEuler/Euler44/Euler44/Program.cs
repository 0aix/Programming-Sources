using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler44
{
    class Program
    {
        static void Main(string[] args)
        {
            int term = 0;
            List<int> xlist = new List<int>();
            for (int x = 1; x < 5000; x++)
            {
                xlist.Add(x * ((3 * x) - 1) / 2);
            }
            for (int d = 1; d < 5000; d++)
            {
                int dx = d * ((3 * d) - 1);
                for (int y = d + 2; y < 5000; y++)
                {
                    int yx = y * ((3 * y) - 1);
                    if (xlist.Contains((yx - dx) / 2))
                    {
                        double dc = 1 - 4 * 3 * (-1 * ((2 * yx) - dx));
                        double sqrt = Math.Sqrt(dc);
                        if (sqrt != Math.Ceiling(sqrt))
                            continue;
                        if ((int)(1 + sqrt) % 6 != 0)
                            continue;
                        for (int z = y + 1; z < 5000; z++)
                        {
                            int zx = z * ((3 * z) - 1);
                            if (dx == 2 * yx - zx)
                            {
                                if (term == 0)
                                    term = d;
                                else if (d < term)
                                    term = d;
                                Console.WriteLine(term * (3 * term - 1) / 2);
                            }
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
