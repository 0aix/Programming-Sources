using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler23
{
    class Program
    {
        public static List<int> abundants = new List<int>();
        public static Boolean[] integers = new Boolean[28124];

        static Boolean Abundant(int n)
        {
            int x = n;
            int sum = 0;
            for (int i = 1; i < x; i++)
            {
                if (n % i == 0)
                {
                    sum += i;
                    if(i != n / i)
                        sum += n / i;
                    x = n / i;
                }
            }
            sum -= n;
            if (sum > n)
                return true;
            else
                return false;
        }

        static void Main(string[] args)
        {
            for (int i = 1; i < 28123; i++)
            {
                if (Abundant(i))
                    abundants.Add(i);
            }
            for (int x = 0; x < abundants.Count; x++)
            {
                for (int y = 0; y < abundants.Count; y++)
                {
                    int sum = abundants[x] + abundants[y];
                    if (sum <= 28123)
                        integers[sum] = true;
                }
            }
            int total = 0;
            for (int i = 0; i <= 28123; i++)
            {
                if (!integers[i])
                    total += i;
            }
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
