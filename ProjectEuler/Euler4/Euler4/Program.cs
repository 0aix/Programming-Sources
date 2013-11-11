using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler4
{
    class Program
    {
        static void Main(string[] args)
        {
            string lxy = "0";
            for (int x = 100; x < 1000; x++)
            {
                for (int y = 100; y < 1000; y++)
                {
                    string z = (x * y).ToString();
                    if (z.Length == 6)
                    {
                        if (z[0] == z[5] && z[1] == z[4] && z[2] == z[3])
                        {
                            if(int.Parse(z) > int.Parse(lxy))
                                lxy = z;
                        }
                    }
                }
            }
            Console.WriteLine(lxy);
            Console.ReadLine();
        }
    }
}
