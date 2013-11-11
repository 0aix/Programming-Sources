using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler56
{
    class Program
    {
        static List<byte> Multiply(List<byte> a, List<byte> b)
        {
            List<byte> product = new List<byte>();
            for (int i = 1; i <= b.Count; i++)
            {
                for (int n = 1; n <= a.Count; n++)
                {
                    if ((n + i) - 2 >= product.Count)
                        product.Add(0);
                    product[(n + i) - 2] += (byte)(a[n - 1] * b[i - 1]);
                }
            }
            for (int i = 0; i < product.Count; i++)
            {
                while (product[i] >= 10)
                {
                    product[i] -= 10;
                    if (i + 1 < product.Count)
                        product[i + 1]++;
                    else
                        product.Add(1);
                }
            }
            return product;
        }

        static List<byte> Convert(byte i)
        {
            List<byte> x = new List<byte>();
            x.Add(i);
            for (int n = 0; n < x.Count; n++)
            {
                while (x[n] >= 10)
                {
                    x[n] -= 10;
                    if (n + 1 < x.Count)
                        x[n + 1]++;
                    else
                        x.Add(1);
                }
            }
            return x;
        }

        static void Main(string[] args)
        {
            int sum = 0;
            for (byte a = 1; a < 100; a++)
            {
                for (byte b = 1; b < 100; b++)
                {
                    List<byte> multi = new List<byte>() { 1 };
                    for (int i = 0; i < b; i++)
                    {
                        multi = Multiply(multi, Convert(a));
                    }
                    int temp = 0;
                    for (int i = 0; i < multi.Count; i++)
                    {
                        temp += multi[i];
                    }
                    if (temp > sum)
                        sum = temp;
                }
            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
