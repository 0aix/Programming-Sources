using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler43
{
    class Program
    {
        static long total = 0;

        static void Loop(string str, string pool, int limit)
        {
            int lim = 0;
            if (pool.Length == 8)
                lim = 2;
            else if (pool.Length == 6)
                lim = 3;
            else if (pool.Length == 0)
            {
                if (int.Parse(str.Substring(2, 3)) % 3 != 0)
                    return;
                if (int.Parse(str.Substring(4, 3)) % 7 != 0)
                    return;
                if (int.Parse(str.Substring(5, 3)) % 11 != 0)
                    return;
                if (int.Parse(str.Substring(6, 3)) % 13 != 0)
                    return;
                if (int.Parse(str.Substring(7, 3)) % 17 != 0)
                    return;
                total += long.Parse(str);
                return;
            }
            if (limit == 1)
            {
                for (int i = 1; i < 9; i++)
                {
                    Loop(str + pool[i], pool.Replace(pool[i].ToString(), ""), 0);
                }
            }
            else if (limit == 2)
            {
                for (int i = 0; i < pool.Length; i++)
                {
                    if (pool[i] == '0' || pool[i] == '2' || pool[i] == '4' || pool[i] == '6' || pool[i] == '8')
                        Loop(str + pool[i], pool.Replace(pool[i].ToString(), ""), 0);
                }
            }
            else if (limit == 3)
            {
                for (int i = 0; i < pool.Length; i++)
                {
                    if (pool[i] == '0' || pool[i] == '5')
                        Loop(str + pool[i], pool.Replace(pool[i].ToString(), ""), 0);
                }
            }
            else
            {
                for (int i = 0; i < pool.Length; i++)
                    Loop(str + pool[i], pool.Replace(pool[i].ToString(), ""), lim);
            }
            return;
        }

        static void Main(string[] args)
        {
            Loop("", "0123456789", 1);
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
