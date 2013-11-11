using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Euler54
{
    class Program
    {
        static List<char> royal = new List<char>() { 'T', 'J', 'Q', 'K', 'A' };
        static List<char> suit = new List<char>(){ 'D', 'C', 'H', 'S' };

        static void Main(string[] args)
        {
            FileStream file = File.OpenRead("poker.txt");
            StreamReader reader = new StreamReader(file);

            int wins = 0;

            for (int i = 0; i < 1000; i++)
            {
                int[,] count = new int[2, 13];
                int[,] cards = new int[2, 5];
                int[,] suits = new int[2, 5];
                string[] hands = reader.ReadLine().Split(' ');

                for (int x = 0; x < 2; x++)
                {
                    for (int n = 0; n < 5; n++)
                    {
                        if (royal.Contains(hands[x * 5 + n][0]))
                        {
                            int index = royal.IndexOf(hands[x * 5 + n][0]);
                            count[x, index + 8]++;
                            //cards[x, n] = index + 10;
                        }
                        else
                        {
                            int value = int.Parse(hands[x * 5 + n][0].ToString());
                            count[x, value - 2]++;
                            //cards[x, n] = value;
                        }
                        suits[x, n] = suit.IndexOf(hands[x * 5 + n][1]);
                    }
                }

                int[] color = { -1, -1 };
                int[] rank = { 0, 0 };
                int[] high = { 0, 0 };

                for (int x = 0; x < 2; x++)
                {
                    bool flush = true;

                    for (int n = 1; n < 5; n++)
                    {
                        if (suits[x, 0] != suits[x, n])
                        {
                            flush = false;
                            break;
                        }
                    }

                    int three = 0;
                    int pair = 0;

                    for (int n = 0; n < 13; n++)
                    {
                        if (count[x, n] == 4)
                        {
                            rank[x] = 7;
                            high[x] = n + 2;
                            break;
                        }
                        if (count[x, n] == 3)
                        {
                            rank[x] = 3;
                            high[x] = n + 2;
                            three++;
                        }
                        if (count[x, n] == 2)
                        {
                            if (three == 0)
                            {
                                rank[x] = 1;
                                high[x] = n + 2;
                            }
                            pair++;

                            if (pair == 2)
                            {
                                rank[x] = 2;
                                high[x] = n + 2;
                            }
                            if (pair == 1 && three == 1)
                            {
                                rank[x] = 6;
                            }
                        }
                    }

                    bool straight = true;

                    if (rank[x] == 0)
                    {
                        int start = -1;
                        int consecutive = 0;

                        for (int n = 0; n < 13; n++)
                        {
                            if (start == -1)
                            {
                                if (count[x, n] == 1)
                                {
                                    consecutive++;
                                    start = n;
                                }
                            }
                            else if (count[x, start] == count[x, n])
                            {
                                consecutive++;
                                start = n;

                                if (consecutive == 5)
                                    break;
                            }
                            else
                            {
                                straight = false;
                                break;
                            }
                        }

                        if (straight)
                        {
                            rank[x] = 4;
                            high[x] = start + 2;
                        }
                    }
                    else
                        straight = false;

                    if (flush && straight && high[x] == 14)
                    {
                        rank[x] = 9;
                        color[x] = suits[x, 0];
                    }
                    else if (flush && straight)
                    {
                        rank[x] = 8;
                        color[x] = suits[x, 0];
                    }
                    else if (flush && rank[x] < 5)
                    {
                        rank[x] = 5;
                        color[x] = suits[x, 0];
                    }
                }

                if (rank[0] > rank[1])
                    wins++;
                else if (rank[0] == rank[1])
                {
                    if (high[0] > high[1])
                        wins++;
                    else if (high[0] == high[1])
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            if (rank[x] == 1 || rank[x] == 2 || rank[x] == 3 || rank[x] == 6 || rank[x] == 7)
                                count[x, high[x] - 2] = 0;
                        }
                        int p1 = 12;
                        int p2 = 12;

                        while (true)
                        {
                            int h1 = 0;
                            int h2 = 0;
                            for (int x = p1; x > 0; x--)
                            {
                                if (count[0, x] > 0)
                                {
                                    h1 = x;
                                    count[0, x]--;
                                    p1 = x;
                                    break;
                                }
                            }
                            for (int x = p2; x > 0; x--)
                            {
                                if (count[1, x] > 0)
                                {
                                    h2 = x;
                                    count[1, x]--;
                                    p2 = x;
                                    break;
                                }
                            }
                            if (h1 > h2)
                            {
                                wins++;
                                break;
                            }
                            else if (h1 < h2)
                                break;
                        }
                    }
                }
            }

            Console.WriteLine(wins);
            Console.ReadLine();
        }
    }
}
