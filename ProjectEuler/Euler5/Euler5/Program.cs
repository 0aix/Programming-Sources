using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler5
{
    class Program
    {
        static int x2 = 0;
        static int x3 = 0;
        static int x5 = 0;
        static int x7 = 0;
        static int x11 = 0;
        static int x13 = 0;
        static int x17 = 0;
        static int x19 = 0;
        static void Main(string[] args)
        {
            for (int x = 2; x <= 20; x++)
            {
                int term = x;
                int y = 0;
                for (y = 0; term % 2 == 0; y++)
                {
                    term /= 2;
                }
                if (y > x2)
                    x2 = y;
                for (y = 0; term % 3 == 0; y++)
                {
                    term /= 3;
                }
                if (y > x3)
                    x3 = y;
                for (y = 0; term % 5 == 0; y++)
                {
                    term /= 5;
                }
                if (y > x5)
                    x5 = y;
                for (y = 0; term % 7 == 0; y++)
                {
                    term /= 7;
                }
                if (y > x7)
                    x7 = y;
                for (y = 0; term % 11 == 0; y++)
                {
                    term /= 11;
                }
                if (y > x11)
                    x11 = y;
                for (y = 0; term % 13 == 0; y++)
                {
                    term /= 13;
                }
                if (y > x13)
                    x13 = y;
                for (y = 0; term % 17 == 0; y++)
                {
                    term /= 17;
                }
                if (y > x17)
                    x17 = y;
                for (y = 0; term % 19 == 0; y++)
                {
                    term /= 19;
                }
                if (y > x19)
                    x19 = y;
            }
            int y2 = (int)Math.Pow(2, x2);
            int y3 = (int)Math.Pow(3, x3);
            int y5 = (int)Math.Pow(5, x5);
            int y7 = (int)Math.Pow(7, x7);
            int y11 = (int)Math.Pow(11, x11);
            int y13 = (int)Math.Pow(13, x13);
            int y17 = (int)Math.Pow(17, x17);
            int y19 = (int)Math.Pow(19, x19);
            int fin = y2 * y3 * y5 * y7 * y11 * y13 * y17 * y19;
            Console.WriteLine(fin.ToString());
            Console.ReadLine();
        }
    }
}
