using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler47
{
    class Program
    {
        static void Main(string[] args)
        {
            int x1 = 644;
            int x2 = 645;
            int x3 = 646;
            int x4 = 647;
            List<int> p1 = new List<int>() { 2, 7, 23 };
            List<int> p2 = new List<int>() { 3, 5, 43 };
            List<int> p3 = new List<int>() { 2, 17, 19 };
            List<int> p4 = new List<int>() { 647 };

            for (; x4 < 10000000; )
            {
                if (p4.Count != 4)
                {
                    x1 = x4 + 1;
                    x2 = x4 + 2;
                    x3 = x4 + 3;
                    x4 = x4 + 4;
                    p1.Clear();
                    int x = x1;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p1.Contains(i))
                                p1.Add(i);
                        }
                    }
                    p2.Clear();
                    x = x2;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p2.Contains(i))
                                p2.Add(i);
                        }
                    }
                    p3.Clear();
                    x = x3;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p3.Contains(i))
                                p3.Add(i);
                        }
                    }
                    p4.Clear();
                    x = x4;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p4.Contains(i))
                                p4.Add(i);
                        }
                    }
                    continue;
                }
                else if (p3.Count != 4)
                {
                    x1 = x4;
                    x2 = x4 + 1;
                    x3 = x4 + 2;
                    x4 = x4 + 3;
                    p1 = new List<int>(p4);
                    p2.Clear();
                    int x = x2;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p2.Contains(i))
                                p2.Add(i);
                        }
                    }
                    p3.Clear();
                    x = x3;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p3.Contains(i))
                                p3.Add(i);
                        }
                    }
                    p4.Clear();
                    x = x4;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p4.Contains(i))
                                p4.Add(i);
                        }
                    }
                    continue;
                }
                else if (p2.Count != 4)
                {
                    x1 = x3;
                    x2 = x4;
                    x3 = x4 + 1;
                    x4 = x4 + 2;
                    p1 = new List<int>(p3);
                    p2 = new List<int>(p4);
                    p3.Clear();
                    int x = x3;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p3.Contains(i))
                                p3.Add(i);
                        }
                    }
                    p4.Clear();
                    x = x4;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p4.Contains(i))
                                p4.Add(i);
                        }
                    }
                    continue;
                }
                else if (p1.Count != 4)
                {
                    x1 = x2;
                    x2 = x3;
                    x3 = x4;
                    x4++;
                    p1 = new List<int>(p2);
                    p2 = new List<int>(p3);
                    p3 = new List<int>(p4);
                    p4.Clear();
                    int x = x4;
                    for (int i = 2; i <= x; i++)
                    {
                        for (; x % i == 0; )
                        {
                            x /= i;
                            if (!p4.Contains(i))
                                p4.Add(i);
                        }
                    }
                    continue;
                }
                else
                {
                    Console.WriteLine(x1);
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
