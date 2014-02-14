using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace TribunalRecords
{
    static class Worker
    {
        private static MySqlConnection conn;
        private static List<UInt32> cases = new List<UInt32>();
        private static Thread thread;
        public static bool run = false;

        public static void Start()
        {
            conn = LoLKeep.GetConnection();
            run = true;
            thread = new Thread(Run);
            thread.Start();
        }

        public static void Stop()
        {
            run = false;
        }

        private static void Run()
        {
            while (true)
            {
                PopulateList();
                int count = cases.Count;
                for (int i = 0; i < count; i++)
                {
                    if (run)
                    {
                        if (GetGameData(cases[i], 1) == null)
                        {
                            Case decision = new Case(cases[i]);
                            if (GetDecision(cases[i], decision))
                            {
                                UpdateDecision(decision.ID, decision);
                            }
                            UpdateFix(decision.ID);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Worker terminated");
                        return;
                    }
                }
            }
        }

        private static void UpdateDecision(UInt32 ID, Case caseData)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE cases SET Decision='" + caseData.Decision + "', Agreement='" + caseData.Agreement + "', Punishment='" + caseData.Punishment + "', Decided=1 WHERE ID='" + ID + "';", conn))
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }

        private static void UpdateFix(UInt32 ID)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE cases SET Fixed=1 WHERE ID=" + ID + ";", conn))
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }

        private static bool GetDecision(UInt32 ID, Case caseData)
        {
            while (true)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://na.leagueoflegends.com/tribunal/en/case/" + ID);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string data = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                    if (data.Contains("Unable to locate specified case."))
                        return false;
                    string punishment = "";
                    for (int i = data.IndexOf("verdict-stat") + 14; data[i] != '<'; i++)
                        punishment += data[i];
                    caseData.Punishment = punishment;
                    string agreement = "";
                    for (int i = data.IndexOf("verdict-stat agreement") + 24; data[i] != '<'; i++)
                        agreement += data[i];
                    caseData.Agreement = agreement;
                    string decision = "";
                    for (int i = data.LastIndexOf("verdict-stat") + 14; data[i] != '<'; i++)
                        decision += data[i];
                    caseData.Decision = decision;
                    break;
                }
                catch (Exception) { }
            }
            return true;
        }

        private static GameRecord GetGameData(UInt32 ID, int game)
        {
            while (true)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://na.leagueoflegends.com/tribunal/en/get_game/" + ID + "/" + game);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string data = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                    return JsonConvert.DeserializeObject<GameRecord>(data);
                }
                catch (Exception ex)
                {
                    if (ex.ToString().Contains("(500)"))
                        return null;
                }
            }
        }

        private static void PopulateList()
        {
            while (true)
            {
                try
                {
                    cases.Clear();
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT ID FROM cases WHERE Decided=0 AND Fixed=0;", conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cases.Add(reader.GetUInt32(0));
                        }
                        reader.Close();
                        conn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }
    }
}
