using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler3
{
    class Program
    {
        static void Main(string[] args)
        {
            long term = 600851475143;
            long pfactor = 0;
            for (long x = 2; x <= term; x++)
            {
                if (term % x == 0)
                {
                    for (; term % x == 0; )
                    {
                        term /= x;
                    }
                    pfactor = x;
                }
            }
            Console.WriteLine(pfactor.ToString());
            Console.ReadLine();
        }
    }
}
