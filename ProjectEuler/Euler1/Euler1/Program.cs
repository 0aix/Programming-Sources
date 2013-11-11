using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler1
{
    class Program
    {
        static int collection = 0;
        static void Main(string[] args)
        {
            for (int x = 0; x < 1000; x++)
            {
                if (x % 3 == 0 || x % 5 == 0)
                {
                    collection += x;
                }
            }
            Console.WriteLine(collection.ToString());
            Console.ReadLine();
        }
    }
}
