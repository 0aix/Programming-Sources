using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler36
{
    class Program
    {
        static bool Palindrome(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            int sum = 0;
            for (int x = 1; x < 1000000; x++)
            {
                string dec = x.ToString();
                string bin = Convert.ToString(x, 2);
                if (Palindrome(dec) && Palindrome(bin))
                    sum += x;
            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
