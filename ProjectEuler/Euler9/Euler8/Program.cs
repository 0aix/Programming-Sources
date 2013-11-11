using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler9
{
    class Program
    {
        static void Main(string[] args)
        {
            int product = 0;
            for (double x = 1; x < 1000; x++)
            {
                bool success = false;
                for (double y = 1; y < 1000; y++)
                {
                    double z = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (!z.ToString().Contains('.'))
                    {
                        if (x + y + z == 1000)
                        {
                            product = (int)(x * y * z);
                            success = true;
                            break;
                        }
                    }
                    if (success)
                        break;
                }
                if (success)
                    break;
            }
            Console.WriteLine(product.ToString());
            Console.ReadLine();
        }
    }
}
