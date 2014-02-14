using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TribunalRecords
{
    public static class LoLKeep
    {
        private static string server = "localhost";
        private static string port = "3306";
        private static string database = "LoLKeep";
        private static string uid = "root";
        private static string password = "root";
        private static MySqlConnection dbconn = new MySqlConnection("Server=" + server + "; Port=" + port + "; Database=" + database + "; Uid=" + uid + "; Pwd=" + password + ";");
        private static Random r = new Random();
        public static int Keys = 0;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=" + server + "; Port=" + port + "; Database=" + database + "; Uid=" + uid + "; Pwd=" + password + ";");
        }

        public static int GenerateKeyID()
        {
            while (true)
            {
                int KeyID = r.Next(100000, 999999);
                if (CheckKey(KeyID))
                {
                    InsertKey(KeyID);
                    return KeyID;
                }
            }
        }

        private static void InsertKey(int KeyID)
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO keyids (KeyID) VALUES (@KeyID);", dbconn))
                    {
                        cmd.Parameters.Add("@KeyID", MySqlDbType.Int32).Value = KeyID;
                        cmd.ExecuteNonQuery();
                        dbconn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static void DeleteKey(int KeyID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM keyids WHERE KeyID='" + KeyID + "';", conn))
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

        private static bool CheckKey(int KeyID)
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM keyids WHERE KeyID='" + KeyID + "';", dbconn))
                    {
                        int res = Convert.ToInt32(cmd.ExecuteScalar());
                        dbconn.Close();
                        return res == 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static void GetKeys()
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM keyids;", dbconn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        int i = 0;
                        while (reader.Read())
                        {
                            Console.WriteLine(" - " + reader.GetInt32(0) + " " + (reader.GetBoolean(1) ? "Terminating" : "Active"));
                            i++;
                        }
                        Console.WriteLine(":: " + i + " keys");
                        Keys = i;
                        reader.Close();
                        dbconn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static void TerminateKey(int KeyID)
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE keyids SET X=1 WHERE KeyID='" + KeyID + "';", dbconn))
                    {
                        cmd.ExecuteNonQuery();
                        dbconn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static void TerminateAllKeys()
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE keyids SET X=1;", dbconn))
                    {
                        cmd.ExecuteNonQuery();
                        dbconn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static UInt32 NextCase()
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    List<UInt32> list = new List<UInt32>();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT ID FROM cases;", dbconn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            list.Add(reader.GetUInt32(0));
                        }
                        reader.Close();
                        dbconn.Close();
                    }
                    list.Sort();
                    UInt32 start = list[0];
                    int count = list.Count;
                    for (int i = 1; i < count; i++)
                    {
                        if (start + (UInt32)i != list[i])
                            return start + (UInt32)i;
                    }
                    return start + (UInt32)count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static void AlterIncrement(UInt32 ID)
        {
            while (true)
            {
                try
                {
                    dbconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ALTER TABLE queue CHANGE ID ID INT(10) UNSIGNED NOT NULL; ALTER TABLE queue CHANGE ID ID INT(10) UNSIGNED NOT NULL AUTO_INCREMENT; ALTER TABLE queue AUTO_INCREMENT=" + ID + ";", dbconn))
                    {
                        cmd.ExecuteNonQuery();
                        dbconn.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dbconn.Close();
                }
            }
        }

        public static bool KeyStatus(int KeyID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM keyids WHERE KeyID='" + KeyID + "' AND X=1;", conn))
                    {
                        int res = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                        return res == 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }

        public static void Queue(int KeyID, MySqlConnection conn)
        {
            if (!CheckQueue(KeyID, conn))
                DeleteQueue(KeyID, conn);
            InsertQueue(KeyID, conn);
        }

        public static bool CheckQueue(int KeyID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM queue WHERE KeyID='" + KeyID + "';", conn))
                    {
                        int res = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                        return res == 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }

        public static void InsertQueue(int KeyID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO queue (KeyID) VALUES (@KeyID);", conn))
                    {
                        cmd.Parameters.Add("@KeyID", MySqlDbType.Int32).Value = KeyID;
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

        public static void DeleteQueue(int KeyID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM queue WHERE KeyID='" + KeyID + "';", conn))
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

        public static UInt32 GetQueue(int KeyID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT ID FROM queue WHERE KeyID='" + KeyID + "';", conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        UInt32 id = 0;
                        if (reader.Read())
                            id = reader.GetUInt32(0);
                        else
                            throw new Exception();
                        reader.Close();
                        conn.Close();
                        return id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }

        public static bool Check(UInt32 ID, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM cases WHERE ID='" + ID + "';", conn))
                    {
                        int res = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                        return res == 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    conn.Close();
                }
            }
        }

        public static void Insert(Case data, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO cases (ID, Games, Reports, Ally, Enemy, OFFENSIVE_LANGUAGE, VERBAL_ABUSE, INTENTIONAL_FEEDING, ASSISTING_ENEMY, UNSKILLED_PLAYER, NO_COMMUNICATION_WITH_TEAM, LEAVING_AFK, NEGATIVE_ATTITUDE, INAPPROPRIATE_NAME, SPAMMING, Decision, Agreement, Punishment, Decided) VALUES (@ID, @Games, @Reports, @Ally, @Enemy, @OFFENSIVE_LANGUAGE, @VERBAL_ABUSE, @INTENTIONAL_FEEDING, @ASSISTING_ENEMY, @UNSKILLED_PLAYER, @NO_COMMUNICATION_WITH_TEAM, @LEAVING_AFK, @NEGATIVE_ATTITUDE, @INAPPROPRIATE_NAME, @SPAMMING, @Decision, @Agreement, @Punishment, @Decided);", conn))
                    {
                        cmd.Parameters.Add("@ID", MySqlDbType.UInt32).Value = data.ID;
                        cmd.Parameters.Add("@Games", MySqlDbType.UByte).Value = data.Games;
                        cmd.Parameters.Add("@Reports", MySqlDbType.UByte).Value = data.Reports;
                        cmd.Parameters.Add("@Ally", MySqlDbType.UByte).Value = data.Ally;
                        cmd.Parameters.Add("@Enemy", MySqlDbType.UByte).Value = data.Enemy;
                        cmd.Parameters.Add("@OFFENSIVE_LANGUAGE", MySqlDbType.UByte).Value = data.OFFENSIVE_LANGUAGE;
                        cmd.Parameters.Add("@VERBAL_ABUSE", MySqlDbType.UByte).Value = data.VERBAL_ABUSE;
                        cmd.Parameters.Add("@INTENTIONAL_FEEDING", MySqlDbType.UByte).Value = data.INTENTIONAL_FEEDING;
                        cmd.Parameters.Add("@ASSISTING_ENEMY", MySqlDbType.UByte).Value = data.ASSISTING_ENEMY;
                        cmd.Parameters.Add("@UNSKILLED_PLAYER", MySqlDbType.UByte).Value = data.UNSKILLED_PLAYER;
                        cmd.Parameters.Add("@NO_COMMUNICATION_WITH_TEAM", MySqlDbType.UByte).Value = data.NO_COMMUNICATION_WITH_TEAM;
                        cmd.Parameters.Add("@LEAVING_AFK", MySqlDbType.UByte).Value = data.LEAVING_AFK;
                        cmd.Parameters.Add("@NEGATIVE_ATTITUDE", MySqlDbType.UByte).Value = data.NEGATIVE_ATTITUDE;
                        cmd.Parameters.Add("@INAPPROPRIATE_NAME", MySqlDbType.UByte).Value = data.INAPPROPRIATE_NAME;
                        cmd.Parameters.Add("@SPAMMING", MySqlDbType.UByte).Value = data.SPAMMING;
                        cmd.Parameters.Add("@Decision", MySqlDbType.TinyText).Value = data.Decision;
                        cmd.Parameters.Add("@Agreement", MySqlDbType.TinyText).Value = data.Agreement;
                        cmd.Parameters.Add("@Punishment", MySqlDbType.TinyText).Value = data.Punishment;
                        cmd.Parameters.Add("@Decided", MySqlDbType.Bit).Value = data.Decided;
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

        public static void Insert(Tribunal data, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO tribunal (Champion, Team, Outcome, Time, Report, OK, OD, OA, OG, TK, TD, TA, TG) VALUES (@Champion, @Team, @Outcome, @Time, @Report, @OK, @OD, @OA, @OG, @TK, @TD, @TA, @TG);", conn))
                    {
                        cmd.Parameters.Add("@Champion", MySqlDbType.TinyText).Value = data.Champion;
                        cmd.Parameters.Add("@Team", MySqlDbType.Bit).Value = data.Team;
                        cmd.Parameters.Add("@Outcome", MySqlDbType.UByte).Value = data.Outcome;
                        cmd.Parameters.Add("@Time", MySqlDbType.UInt16).Value = data.Time;
                        cmd.Parameters.Add("@Report", MySqlDbType.TinyText).Value = data.Report;
                        cmd.Parameters.Add("@OK", MySqlDbType.UByte).Value = data.OK;
                        cmd.Parameters.Add("@OD", MySqlDbType.UByte).Value = data.OD;
                        cmd.Parameters.Add("@OA", MySqlDbType.UByte).Value = data.OA;
                        cmd.Parameters.Add("@OG", MySqlDbType.UInt16).Value = data.OG;
                        cmd.Parameters.Add("@TK", MySqlDbType.UInt16).Value = data.TK;
                        cmd.Parameters.Add("@TD", MySqlDbType.UInt16).Value = data.TD;
                        cmd.Parameters.Add("@TA", MySqlDbType.UInt16).Value = data.TA;
                        cmd.Parameters.Add("@TG", MySqlDbType.UInt32).Value = data.TG;
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

        public static void Insert(Record data, MySqlConnection conn)
        {
            while (true)
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO record (Champion, Team, ReportedTeam, Offender, Outcome, Time, K, D, A, G) VALUES (@Champion, @Team, @ReportedTeam, @Offender, @Outcome, @Time, @K, @D, @A, @G);", conn))
                    {
                        cmd.Parameters.Add("@Champion", MySqlDbType.TinyText).Value = data.Champion;
                        cmd.Parameters.Add("@Team", MySqlDbType.Bit).Value = data.Team;
                        cmd.Parameters.Add("@ReportedTeam", MySqlDbType.Bit).Value = data.ReportedTeam;
                        cmd.Parameters.Add("@Offender", MySqlDbType.Bit).Value = data.Offender;
                        cmd.Parameters.Add("@Outcome", MySqlDbType.UByte).Value = data.Outcome;
                        cmd.Parameters.Add("@Time", MySqlDbType.UInt16).Value = data.Time;
                        cmd.Parameters.Add("@K", MySqlDbType.UByte).Value = data.K;
                        cmd.Parameters.Add("@D", MySqlDbType.UByte).Value = data.D;
                        cmd.Parameters.Add("@A", MySqlDbType.UByte).Value = data.A;
                        cmd.Parameters.Add("@G", MySqlDbType.UInt16).Value = data.G;
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
    }
}
