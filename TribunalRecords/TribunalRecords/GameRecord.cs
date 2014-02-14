using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TribunalRecords
{
    public class GameRecord
    {
        public string game_creation_time { get; set; }
        public string game_mode { get; set; }
        public string game_type { get; set; }
        public int premade { get; set; }
        public List<ChatLog> chat_log { get; set; }
        public object offender_id { get; set; }
        public List<Player> players { get; set; }
        public List<Report> reports { get; set; }
        public string most_common_report_reason { get; set; }
        public byte allied_report_count { get; set; }
        public byte enemy_report_count { get; set; }
        public int platform_game_id { get; set; }
        public object version { get; set; }
        public TranslatedReportedBy translated_reported_by { get; set; }
        public TranslatedReportReasons translated_report_reasons { get; set; }
        public string game_mode_raw { get; set; }
        public byte case_total_reports { get; set; }
        public object offender { get; set; }
    }
    
    public class ChatLog
    {
        public string date { get; set; }
        public string time { get; set; }
        public string summoner_name { get; set; }
        public string sent_to { get; set; }
        public string message { get; set; }
        public string association_to_offender { get; set; }
        public string champion_name { get; set; }
        public int name_change { get; set; }
    }

    public class Scores
    {
        public byte kills { get; set; }
        public byte deaths { get; set; }
        public byte assists { get; set; }
        public int nodes_captured { get; set; }
        public int nodes_neutralized { get; set; }
        public int player_score { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public int total_price { get; set; }
    }

    public class Player
    {
        public string skin { get; set; }
        public string team { get; set; }
        public string level { get; set; }
        public Scores scores { get; set; }
        public string minions_killed { get; set; }
        public string total_damage_dealt { get; set; }
        public string total_damage_received { get; set; }
        public UInt16 gold_earned { get; set; }
        public List<Item> items { get; set; }
        public string outcome { get; set; }
        public UInt16 time_played { get; set; }
        public string association_to_offender { get; set; }
        public string champion_name { get; set; }
        public int summoner_spell_1 { get; set; }
        public int summoner_spell_2 { get; set; }
        public string champion_url { get; set; }
        public int champion_id { get; set; }
        public object summoner_name { get; set; }
        public string champion_icon { get; set; }
        public string summoner_spell_1_icon { get; set; }
        public string summoner_spell_2_icon { get; set; }
        public List<string> item_icons { get; set; }
    }

    public class Report
    {
        public string offense { get; set; }
        public string comment { get; set; }
        public string association_to_offender { get; set; }
    }

    public class TranslatedReportedBy
    {
        public string ally { get; set; }
        public string enemy { get; set; }
    }

    public class TranslatedReportReasons
    {
        public string OFFENSIVE_LANGUAGE { get; set; }
        public string VERBAL_ABUSE { get; set; }
        public string INTENTIONAL_FEEDING { get; set; }
        public string ASSISTING_ENEMY { get; set; }
        public string UNSKILLED_PLAYER { get; set; }
        public string NO_COMMUNICATION_WITH_TEAM { get; set; }
        public string LEAVING_AFK { get; set; }
        public string NEGATIVE_ATTITUDE { get; set; }
        public string INAPPROPRIATE_NAME { get; set; }
        public string SPAMMING { get; set; }
    }
}
