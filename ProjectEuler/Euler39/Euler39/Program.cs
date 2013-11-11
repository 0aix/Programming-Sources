using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler39
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] perimeter = new int[1001];
            for (int a = 1; a < 1000; a++)
            {
                for (int b = 1; b < 1000; b++)
                {
                    for (int c = 1; c < 1000; c++)
                    {
                        if (a * a + b * b == c * c && a + b + c <= 1000)
                            perimeter[a + b + c]++;
                    }
                }
            }
            //Solutions are doubled -> 3, 4, 5 <==> 4, 3, 5
            int max = 0;
            int p = 0;
            for (int i = 0; i <= 1000; i++)
            {
                if (perimeter[i] > max)
                {
                    p = i;
                    max = perimeter[i];
                }
            }
            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
