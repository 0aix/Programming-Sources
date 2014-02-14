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
    class Parser
    {
        public int KeyID;
        private Thread thread;
        private MySqlConnection conn;

        public Parser(int id)
        {
            KeyID = id;
            conn = LoLKeep.GetConnection();
            thread = new Thread(Run);
            thread.Start();
        }

        public void Run()
        {
            while (true)
            {
                if (LoLKeep.KeyStatus(KeyID, conn))
                {
                    LoLKeep.Queue(KeyID, conn);
                    ParseGame(LoLKeep.GetQueue(KeyID, conn));
                }
                else
                {
                    if (!LoLKeep.CheckQueue(KeyID, conn))
                        LoLKeep.DeleteQueue(KeyID, conn);
                    LoLKeep.DeleteKey(KeyID, conn);
                    Console.WriteLine("Key: " + KeyID + " terminated");
                    break;
                }
            }
        }

        public void ParseGame(UInt32 ID)
        {
            Console.Title = "Case: " + ID + " - Key: " + KeyID;
            if (LoLKeep.Check(ID, conn))
            {
                Case caseData = new Case(ID);
                for (int game = 1; game <= 5; game++)
                {
                    GameRecord gameData;
                    Tribunal tribunalData = new Tribunal();
                    caseData.Games++;
                    if ((gameData = GetGame(ID, game)) != null)
                    {
                        if (gameData.game_mode == "Classic" && gameData.game_mode_raw == "Classic" && gameData.players.Count == 10)
                        {
                            for (int n = 0; n < 10; n++)
                            {
                                Record recordData = new Record();
                                Player temp = gameData.players[n];
                                recordData.Champion = temp.champion_name;
                                recordData.Team = temp.team != "Team1";
                                recordData.ReportedTeam = temp.association_to_offender != "enemy";
                                recordData.Offender = temp.association_to_offender == "offender";
                                switch (temp.outcome)
                                {
                                    case "Win":
                                        recordData.Outcome = 0;
                                        break;
                                    case "Loss":
                                        recordData.Outcome = 1;
                                        break;
                                    case "Leave":
                                        recordData.Outcome = 2;
                                        break;
                                }
                                recordData.Time = temp.time_played;
                                recordData.K = temp.scores.kills;
                                recordData.D = temp.scores.deaths;
                                recordData.A = temp.scores.assists;
                                recordData.G = temp.gold_earned;
                                LoLKeep.Insert(recordData, conn);
                                if (recordData.Offender)
                                    tribunalData.Record(recordData);
                                if (recordData.ReportedTeam)
                                    tribunalData.AddKDA(recordData);
                            }
                            tribunalData.Report = gameData.most_common_report_reason;
                            LoLKeep.Insert(tribunalData, conn);
                        }
                        caseData.Ally += gameData.allied_report_count;
                        caseData.Enemy += gameData.enemy_report_count;
                        int count = gameData.reports.Count;
                        for (int n = 0; n < count; n++)
                        {
                            switch (gameData.reports[n].offense)
                            {
                                case "OFFENSIVE_LANGUAGE":
                                    caseData.OFFENSIVE_LANGUAGE++;
                                    break;
                                case "VERBAL_ABUSE":
                                    caseData.VERBAL_ABUSE++;
                                    break;
                                case "INTENTIONAL_FEEDING":
                                    caseData.INTENTIONAL_FEEDING++;
                                    break;
                                case "ASSISTING_ENEMY":
                                    caseData.ASSISTING_ENEMY++;
                                    break;
                                case "UNSKILLED_PLAYER":
                                    caseData.UNSKILLED_PLAYER++;
                                    break;
                                case "NO_COMMUNICATION_WITH_TEAM":
                                    caseData.NO_COMMUNICATION_WITH_TEAM++;
                                    break;
                                case "LEAVING_AFK":
                                    caseData.LEAVING_AFK++;
                                    break;
                                case "NEGATIVE_ATTITUDE":
                                    caseData.NEGATIVE_ATTITUDE++;
                                    break;
                                case "INAPPROPRIATE_NAME":
                                    caseData.INAPPROPRIATE_NAME++;
                                    break;
                                case "SPAMMING":
                                    caseData.SPAMMING++;
                                    break;
                            }
                        }
                    }
                    else if (game > 1)
                        break;
                    else
                    {
                        Console.WriteLine("Key: " + KeyID + " - Ended at " + ID);
                        LoLKeep.TerminateKey(KeyID);
                        return;
                    }
                }
                if (GetDecision(ID, caseData))
                    caseData.Decided = true;
                caseData.Reports = (byte)(caseData.Ally + caseData.Enemy);
                LoLKeep.Insert(caseData, conn);
            }
        }

        public bool GetDecision(UInt32 ID, Case caseData)
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

        public GameRecord GetGame(UInt32 ID, int game)
        {
            GameRecord gameData = GetGameData(ID, game);
            if (gameData == null)
                gameData = GetReformGameData(ID, game);
            return gameData;
        }

        public GameRecord GetGameData(UInt32 ID, int game)
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

        public GameRecord GetReformGameData(UInt32 ID, int game)
        {
            while (true)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://na.leagueoflegends.com/tribunal/en/get_reform_game/" + ID + "/" + game);
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
    }
}
