using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Euler79
{
    class Program
    {
        static List<char> a = new List<char>();
        static List<char> b = new List<char>();
        static List<char> c = new List<char>();

        static string Loop(string str, string basestr)
        {
            if (basestr.Length != 0)
            {
                for (int i = 0; i < basestr.Length; i++)
                {
                    string result = Loop(str + basestr[i], basestr.Remove(i, 1));
                    if (result != "")
                        return result;
                }
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    if (str.IndexOf(a[i]) >= str.IndexOf(b[i]) || str.IndexOf(b[i]) >= str.IndexOf(c[i]))
                        return "";
                }
                return str;
            }
            return "";
        }

        static void Main(string[] args)
        {
            FileStream file = File.OpenRead("keylog.txt");
            StreamReader reader = new StreamReader(file);
            List<string> log = new List<string>();
            while (!reader.EndOfStream)
                log.Add(reader.ReadLine());
            int[] digits = new int[10];
            for (int i = 0; i < 50; i++)
            {
                int[] temp = new int[10];
                for (int n = 0; n < 3; n++)
                    temp[int.Parse(log[i][n].ToString())]++;
                for (int n = 0; n < 10; n++)
                {
                    if (temp[n] > digits[n])
                        digits[n] = temp[n];
                }
                a.Add(log[i][0]);
                b.Add(log[i][1]);
                c.Add(log[i][2]);
            }
            //Already confirmed there's a max of 1 digit for each
            string basestr = "";
            for (int i = 0; i < 10; i++)
            {
                if (digits[i] > 0)
                    basestr += i.ToString();
            }
            string str = Loop("", basestr);
            Console.WriteLine(str);
            Console.ReadLine();
        }
    }
}
