using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler32
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> products = new List<int>();
            int sum = 0;
            for (int a = 1; a < 10000; a++)
            {
                string na = a.ToString();
                for (int b = 1; b < Math.Pow(10, 9 - na.Length - 4); b++)
                {
                    string nb = b.ToString();
                    int pro = a * b;
                    if (!products.Contains(pro))
                    {
                        string concat = na + nb + pro;
                        if (concat.Length > 9)
                            continue;
                        else
                        {
                            bool success = true;
                            for (int i = 1; i <= 9; i++)
                            {
                                if (!concat.Contains(i.ToString()))
                                {
                                    success = false;
                                    break;
                                }
                            }
                            if (success)
                            {
                                products.Add(pro);
                                sum += pro;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
