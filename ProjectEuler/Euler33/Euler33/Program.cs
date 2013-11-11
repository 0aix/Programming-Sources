using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler33
{
    class Program
    {
        static void Main(string[] args)
        {
            int numer = 1;
            int denom = 1;
            for (double n = 10; n < 100; n++)
            {
                for (double d = n + 1; d < 100; d++)
                {
                    string ns = n.ToString();
                    string ds = d.ToString();
                    for (int ni = 0; ni < 2; ni++)
                    {
                        if (ns[ni] == '0')
                            continue;
                        for (int di = 0; di < 2; di++)
                        {
                            if (ns[ni] == ds[di])
                            {
                                double nx = double.Parse(ns.Remove(ni, 1));
                                double dx = double.Parse(ds.Remove(di, 1));
                                if (n / nx == d / dx)
                                {
                                    numer *= (int)nx;
                                    denom *= (int)dx;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 2; i < numer; i++)
            {
                while (numer % i == 0 && denom % i == 0)
                {
                    numer /= i;
                    denom /= i;
                }
            }
            Console.WriteLine(denom);
            Console.ReadLine();
        }
    }
}
