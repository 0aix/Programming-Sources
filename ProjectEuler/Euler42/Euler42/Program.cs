using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Euler42
{
    class Program
    {
        static bool Triangle(int i)
        {
            for (int n = 1; ; n++)
            {
                i -= n;
                if (i == 0)
                    return true;
                if (i < 0)
                    return false;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                StreamReader reader = new StreamReader("words.txt");
                string text = reader.ReadToEnd();
                reader.Close();
                text = text.Replace('"'.ToString(), "");
                string[] words = text.Split(',');
                int count = 0;
                for (int i = 0; i < words.Length; i++)
                {
                    int sum = 0;
                    for (int n = 0; n < words[i].Length; n++)
                    {
                        sum += words[i][n] - 64;
                    }
                    if (Triangle(sum))
                        count++;
                }
                Console.WriteLine(count);
                Console.ReadLine();
            }
            catch (Exception e) { }
        }
    }
}
