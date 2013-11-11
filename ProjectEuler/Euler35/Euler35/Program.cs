using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler35
{
    class Program
    {

        static void Main(string[] args)
        {
            bool[] composites = new bool[1000000];
            List<int> primes = new List<int>();
            for (int x = 2; x < 1000000; x++)
            {
                if(!composites[x])
                {
                    for(int z = x + x; z < 1000000; z += x)
                    {
                        composites[z] = true;
                    }
                    primes.Add(x);
                }
            }
            int count = 0;
            for(int x = 0; x < primes.Count; x++)
            {
                string num = primes[x].ToString();
                bool circ = true;
                for (int i = 0; i < num.Length; i++)
                {
                    string temp = "";
                    for (int n = 0; n < num.Length; n++)
                    {
                        if(i + n >= num.Length)
                            temp += num[i + n - num.Length];
                        else
                            temp += num[i + n];
                    }
                    if (!primes.Contains(int.Parse(temp)))
                    {
                        circ = false;
                        break;
                    }
                }
                if (circ)
                    count++;
            }
            Console.WriteLine(count);
            Console.ReadLine();
        }
    }
}
