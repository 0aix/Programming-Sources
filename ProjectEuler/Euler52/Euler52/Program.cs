using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler52
{
    class Program
    {
        static int[] Count(int i)
        {
            string num = i.ToString();
            int[] digits = new int[10];
            for (int n = 0; n < num.Length; n++)
            {
                digits[int.Parse(num[n].ToString())]++;
            }
            return digits;
        }

        static bool Equals(int[] a, int[] b)
        {
            for (int x = 0; x < 10; x++)
            {
                if (a[x] != b[x])
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            int basex = 10;
            for (int x = 1; ; x++)
            {
                if ((x * 6).ToString().Length != x.ToString().Length)
                {
                    x = basex;
                    basex *= 10;
                }
                else
                {
                    int[] digits = Count(x);
                    bool fail = false;
                    for (int i = 2; i <= 6; i++)
                    {
                        int[] digitsx = Count(x * i);
                        if (!Equals(digits, digitsx))
                        {
                            fail = true;
                            break;
                        }
                    }
                    if (!fail)
                    {
                        Console.WriteLine(x);
                        break;
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
