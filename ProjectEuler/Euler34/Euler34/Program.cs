using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler34
{
    class Program
    {
        static int Factorial(int i)
        {
            int n = 1;
            for (; i > 1; i--)
            {
                n *= i;
            }
            return n;
        }
        static void Main(string[] args)
        {
            int total = 0;
            for (int x = 10; x < 2000000; x++)
            {
                int sum = 0;
                string num = x.ToString();
                for (int i = 0; i < num.Length; i++)
                {
                    sum += Factorial(int.Parse(num[i].ToString()));
                }
                if (sum == x)
                    total += x;
            }
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
