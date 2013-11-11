using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler58
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;

            for (int i = 2;; i++)
            {
                int diagonal = (i - 1) * 4 + 1;
                int length = 2 * (i - 1) + 1;
                int square = length * length;

                for (int x = 1; x < 4; x++)
                {
                    if (prime(square - (i - 1) * 2 * x))
                        count++;
                }

                if ((double)count / (double)diagonal < 0.10)
                {
                    Console.WriteLine(length);
                    break;
                }
            }
            Console.ReadLine();
        }

        static bool prime(int n)
        {
            if (n < 2)
                return false;
            if (n == 2)
                return true;
            if (n % 2 == 0)
                return false;
            if (n < 9)
                return true;
            if (n % 3 == 0)
                return false;

            int c = 5;
            while (c * c <= n)
            {
                if (n % c == 0)
                    return false;
                if (n % (c + 2) == 0)
                    return false;

                c += 6;
            }

            return true;
        }
    }
}
