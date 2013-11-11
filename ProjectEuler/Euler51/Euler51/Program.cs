using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler51
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] composite = new bool[500000];
            List<int> primes = new List<int>();

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    for (int x = 2; x < 500000; x++)
                    {
                        if (!composite[x])
                        {
                            primes.Add(x);
                            for (int n = x + x; n < 500000; n += x)
                                composite[n] = true;
                        }
                    }
                }
                else
                {   
                    composite[0] = true;
                    for (int x = 1; x < 500000; x++)
                        composite[x] = false;

                    for (int x = 0; x < primes.Count; x++)
                    {
                        int limit = (i + 1) * 500000;
                        for (int n = (i * 500000) - ((i * 500000) % primes[x]) + primes[x]; n < limit; n += primes[x])
                        {
                            composite[n - 500000] = true;
                        }
                    }

                    for (int x = 0; x < 500000; x++)
                    {
                        if (!composite[x])
                            primes.Add(x + i * 500000);
                    }
                }
            }

            //100000 to 1000000 -- 3 DIGITS

            List<int> filter1 = new List<int>();
            List<int> d1 = new List<int>();
            List<int> d2 = new List<int>();
            List<int> d3 = new List<int>();

            int index;
            for (index = 0; primes[index] < 100000; index++);
            int lim = 0;
            try
            {
                for (lim = 0; primes[lim] < 1000000; lim++);
            }
            catch (Exception e) { }

            for (int i = index; i < lim; i++)
            {
                string str = primes[i].ToString();
                for (int x = 0; x < str.Length - 1; x++)
                {
                    for (int n = x + 1; n < str.Length - 1; n++)
                    {
                        for (int z = n + 1; z < str.Length - 1; z++)
                        {
                            if (str[x] == str[n] && str[n] == str[z])
                            {
                                filter1.Add(primes[i]);
                                d1.Add(x);
                                d2.Add(n);
                                d3.Add(z);
                            }
                        }
                    }
                }
            }
            primes.Clear();

            List<List<int>> filters = new List<List<int>>();
            List<int> digit1 = new List<int>();
            List<int> digit2 = new List<int>();
            List<int> digit3 = new List<int>();

            while (filter1.Count > 0)
            {
                filters.Add(new List<int>());
                digit1.Add(d1[0]);
                digit2.Add(d2[0]);
                digit3.Add(d3[0]);
                int count = filters.Count - 1;

                for (int i = filter1.Count - 1; i >= 0; i--)
                {
                    if (d1[0] == d1[i] && d2[0] == d2[i] && d3[0] == d3[i])
                    {
                        filters[count].Insert(0, filter1[i]);
                        filter1.RemoveAt(i);
                        d1.RemoveAt(i);
                        d2.RemoveAt(i);
                        d3.RemoveAt(i);
                    }
                }
                if (filters[count].Count == 1)
                {
                    filters.RemoveAt(count);
                    digit1.RemoveAt(count);
                    digit2.RemoveAt(count);
                    digit3.RemoveAt(count);
                }
            }

            List<List<string>> strings = new List<List<string>>();
            List<List<string>> answers = new List<List<string>>();
            List<List<int>> times = new List<List<int>>();

            for (int i = 0; i < filters.Count; i++)
            {
                strings.Add(new List<string>());
                int dt1 = digit1[i];
                int dt2 = digit2[i];
                int dt3 = digit3[i];
                for (int x = 0; x < filters[i].Count; x++)
                {
                    string str = filters[i][x].ToString();
                    str = str.Remove(dt3, 1).Remove(dt2, 1).Remove(dt1, 1);
                    strings[i].Add(str);
                }

                answers.Add(new List<string>());
                times.Add(new List<int>());

                for (int x = 0; x < strings[i].Count; x++)
                {
                    if (!answers[i].Contains(strings[i][x]))
                    {
                        answers[i].Add(strings[i][x]);
                        times[i].Add(1);
                    }
                    else
                        times[i][answers[i].IndexOf(strings[i][x])]++;
                }
            }

            for (int i = 0; i < answers.Count; i++)
            {
                for (int x = 0; x < answers[i].Count; x++)
                {
                    if (times[i][x] == 8)
                    {
                        int n = strings[i].IndexOf(answers[i][x]);
                        Console.WriteLine(filters[i][n]);
                    }
                }
            }

            //Answer is 121313

            Console.ReadLine();
        }
    }
}
