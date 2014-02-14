using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TribunalRecords
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tribunal Records";
            LoLKeep.AlterIncrement(LoLKeep.NextCase());
            //Worker.Start();
            string[] s;
            while ((s = Console.ReadLine().ToLower().Split(' ')) != null)
            {
                if (s[0] == "help")
                {
                    Console.WriteLine(" - generate");
                    Console.WriteLine(" - update");
                    Console.WriteLine(" - keys");
                    Console.WriteLine(" - terminate { }");
                    Console.WriteLine(" - start");
                    Console.WriteLine(" - stop");
                    Console.WriteLine(" - exit");
                }
                else if (s[0] == "generate")
                {
                    int count = 1;
                    if (s.Length > 1)
                        int.TryParse(s[1], out count);
                    for (int i = 0; i < count; i++)
                    {
                        Parser p = new Parser(LoLKeep.GenerateKeyID());
                        Console.WriteLine(":: " + p.KeyID);
                    }
                }
                else if (s[0] == "update")
                {
                    if (LoLKeep.Keys == 0)
                    {
                        LoLKeep.AlterIncrement(LoLKeep.NextCase());
                        Console.WriteLine("Updated");
                    }
                    else
                        Console.WriteLine("No keys may be active");
                }
                else if (s[0] == "keys")
                {
                    LoLKeep.GetKeys();
                }
                else if (s[0] == "terminate")
                {
                    if (s.Length > 1)
                    {
                        int KeyID = 0;
                        if (s[1] == "all")
                            LoLKeep.TerminateAllKeys();
                        else if (int.TryParse(s[1], out KeyID))
                            LoLKeep.TerminateKey(KeyID);
                        LoLKeep.GetKeys();
                    }
                    else
                    {
                        Console.WriteLine(":: Key required");
                    }
                }
                else if (s[0] == "start")
                {
                    if (!Worker.run)
                        Worker.Start();
                }
                else if (s[0] == "stop")
                {
                    Worker.Stop();
                }
                else if (s[0] == "exit")
                {
                    Worker.Stop();
                    break;
                }
                else
                {
                    Console.WriteLine(":: Command not recognized");
                    Console.WriteLine(" - help");
                    Console.WriteLine(" - generate");
                    Console.WriteLine(" - keys");
                    Console.WriteLine(" - terminate { }");
                    Console.WriteLine(" - exit");
                }
            }
        }
    }
}
