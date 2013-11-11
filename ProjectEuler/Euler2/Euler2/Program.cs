using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler2
{
    class Program
    {
        static int prev1 = 1;
        static int prev2 = 0;
        static int sum = 0;
        static void Main(string[] args)
        {
            int x = 1;
            while (x < 4000000)
            {
                if(x % 2 == 0)
                    sum += x;
                prev2 = prev1;
                prev1 = x;
                x = prev1 + prev2;
            }
            Console.WriteLine(sum.ToString());
            Console.ReadLine();
        }
    }
}
