using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler38
{
    class Program
    {
        static void Main(string[] args)
        {
            int top = 0;
            for (int i = 1; i < 10000; i++)
            {
                bool[] num = {true, true, true, true, true, true, true, true, true};
                string panstr = "";
                int len = 0;
                for (int n = 1; len < 9; n++)
                {
                    string str = (i * n).ToString();
                    if (str.Contains('0'))
                        break;
                    len += str.Length;
                    if(len > 9)
                        break;
                    panstr += str;
                    for (int x = 0; x < str.Length; x++)
                        num[str[x] - 49] = !num[str[x] - 49];
                }
                bool fail = false;
                for (int n = 0; n < 9; n++)
                {
                    if (num[n])
                    {
                        fail = true;
                        break;
                    }
                }
                if (!fail)
                {
                    int pdigit = int.Parse(panstr);
                    if (pdigit > top)
                        top = pdigit;
                }
            }
            Console.WriteLine(top);
            Console.ReadLine();
        }
    }
}
