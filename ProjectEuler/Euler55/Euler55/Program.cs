using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler55
{
    class Number
    {
        public List<byte> value;

        public Number(string num)
        {
            value = new List<byte>();
            for (int i = num.Length - 1; i >= 0; i--)
                value.Add(byte.Parse(num[i].ToString()));
        }

        public Number(Number num)
        {
            value = new List<byte>();
            for (int i = 0; i < num.value.Count; i++)
                value.Add(num.value[i]);
        }

        public Number()
        {
            value = new List<byte>();
        }

        public void Reverse()
        {
            value.Reverse();
        }

        public string ToString()
        {
            string str = "";
            for (int i = value.Count - 1; i >= 0; i--)
                str += value[i].ToString();
            return str;
        }

        public bool Palindrome()
        {
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i] != value[value.Count - i - 1])
                    return false;
            }
            return true;
        }

        public static Number Add(Number a, Number b)
        {
            Number num = new Number();
            if (a.value.Count > b.value.Count)
                b.value.Add(0);
            else if (b.value.Count > a.value.Count)
                a.value.Add(0);
            for (int i = 0; i < a.value.Count; i++)
                num.value.Add(0);
            for (int i = 0; i < a.value.Count; i++)
            {
                num.value[i] += (byte)(a.value[i] + b.value[i]);
                while (num.value[i] >= 10)
                {
                    num.value[i] -= 10;
                    if (i == num.value.Count - 1)
                        num.value.Add(1);
                    else
                        num.value[i + 1]++;
                }
            }
            return num;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<int> set = new List<int>();
            for (int i = 1; i < 10000; i++)
                set.Add(i);
            List<int> lychrels = new List<int>();
            while (0 < set.Count)
            {
                string str = set[0].ToString();
                Number num = new Number(str);
                bool fail = false;
                for (int n = 1; n < 50; n++)
                {
                    Number temp = new Number(num);
                    temp.Reverse();
                    num = Number.Add(num, temp);
                    if (num.Palindrome())
                    {
                        fail = true;
                        break;
                    }
                }
                if (!fail)
                    lychrels.Add(int.Parse(str));
                set.Remove(int.Parse(str));
            }
            Console.Write(lychrels.Count);
            Console.ReadLine();
        }
    }
}
