using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler31
{
    class Program
    {
        static void Main(string[] args)
        {
            int combinations = 1;
            for (int p1 = 0; p1 <= 200; p1++)
            {
                int lim1 = 200 - p1 * 1;
                if (lim1 == 0)
                {
                    combinations++;
                    break;
                }
                for (int p2 = 0; p2 <= lim1 / 2; p2++)
                {
                    int lim2 = lim1 - p2 * 2;
                    if (lim2 == 0)
                    {
                        combinations++;
                        break;
                    }
                    for (int p5 = 0; p5 <= lim2 / 5; p5++)
                    {
                        int lim3 = lim2 - p5 * 5;
                        if (lim3 == 0)
                        {
                            combinations++;
                            break;
                        }
                        for (int p10 = 0; p10 <= lim3 / 10; p10++)
                        {
                            int lim4 = lim3 - p10 * 10;
                            if (lim4 == 0)
                            {
                                combinations++;
                                break;
                            }
                            for (int p20 = 0; p20 <= lim4 / 20; p20++)
                            {
                                int lim5 = lim4 - p20 * 20;
                                if (lim5 == 0)
                                {
                                    combinations++;
                                    break;
                                }
                                for (int p50 = 0; p50 <= lim5 / 50; p50++)
                                {
                                    int lim6 = lim5 - p50 * 50;
                                    if (lim6 == 0)
                                    {
                                        combinations++;
                                        break;
                                    }
                                    for (int p100 = 0; p100 <= lim6 / 100; p100++)
                                    {
                                        if (lim6 - p100 * 100 == 0)
                                        {
                                            combinations++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(combinations);
            Console.ReadLine();
        }
    }
}
