using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler6
{
    class Program
    {
        static void Main(string[] args)
        {
            long sum = 0;
            long square = 0;
            for (int x = 1; x <= 100; x++)
            {
                sum += (long)Math.Pow(x, 2);
            }
            for (int x = 1; x <= 100; x++)
            {
                square += x;
            }
            square = (long)Math.Pow(square, 2);
            Console.WriteLine((square - sum).ToString());
            Console.ReadLine();
        }
    }
}
