using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;


namespace UMTS
{
    public partial class UMTS : Form
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool GetAsyncKeyState(int vKey);

        public static string round = "";
        public static string eid = "";
        public static CookieContainer cc = new CookieContainer();
        public static int item = 1;
        public static double buy1 = 0.30;
        public static double sell1 = 0.70;
        public static double buy2 = 0.80;
        public static double sell2 = 1.45;
        public static double buy3 = 0.8;
        public static double sell3 = 1.50;
        public static double buy4 = 4.80;
        public static double sell4 = 6.40;
        public static double per1 = 0.20;
        public static double per2 = 0.25;
        public static double per3 = 0.25;
        public static double per4 = 0.30;
        public static double high1 = 0.70;
        public static double low1 = 0.30;
        public static double high2 = 1.45;
        public static double low2 = 0.80;
        public static double high3 = 1.50;
        public static double low3 = 0.80;
        public static double high4 = 6.40;
        public static double low4 = 4.80;
        public static Boolean beep1 = true;
        public static Boolean beep2 = true;
        public static Boolean beep3 = true;
        public static Boolean beep4 = true;
        public static Boolean peak1 = false;
        public static Boolean peak2 = false;
        public static Boolean peak3 = false;
        public static Boolean peak4 = false;
        public static Boolean auto1 = false;
        public static Boolean auto2 = false;
        public static Boolean auto3 = false;
        public static Boolean auto4 = false;
        public static Boolean const1 = false;
        public static Boolean const2 = false;
        public static Boolean const3 = false;
        public static Boolean const4 = false;
         public static int state1 = 0;// 0 = Null; 1 = Buying; 2 = Selling; 3 = Buying/Selling
        public static int state2 = 0;
        public static int state3 = 0;
        public static int state4 = 0;
        public static string resstring = "";
        private static Thread _thread;
        public static Boolean botcheck = false;
        public static string size = "0";
        public static int transaction = 0;
        public static string passcode = "";
        public static string alpha = "zyxwvutsrqponmlkjihgfedcba";
        public static bool invis = false;

        public UMTS()
        {
            InitializeComponent();
        }

        private void Alarm(int type, int item, string s, bool beep, bool trade, bool constant)
        {
            if (trade)
            {
                if (item == 1)
                    tab1_Click(null, null);
                if (item == 2)
                    tab3_Click(null, null);
                if (item == 3)
                    tab2_Click(null, null);
                if (item == 4)
                    tab4_Click(null, null);
                if (type == 1 && double.Parse(Gold.Text) > 0 && double.Parse(size) > 0)
                {
                    if (!constant)
                    {
                        if (item == 1)
                            state1 = 2;
                        if (item == 2)
                            state2 = 2;
                        if (item == 3)
                            state3 = 2;
                        if (item == 4)
                            state4 = 2;
                    }
                    else
                    {
                        if (item == 1)
                            state1 = 3;
                        if (item == 2)
                            state2 = 3;
                        if (item == 3)
                            state3 = 3;
                        if (item == 4)
                            state4 = 3;
                    }
                    if (item == 1)
                        Buytxt.Text = Math.Round(Math.Round(per1 * double.Parse(Gold.Text)) / double.Parse(mfood.Text.Substring(0, 4))).ToString();  
                    if (item == 2)
                        Buytxt.Text = Math.Round(Math.Round(per2 * double.Parse(Gold.Text)) / double.Parse(miron.Text.Substring(0, 4))).ToString(); 
                    if (item == 3)
                        Buytxt.Text = Math.Round(Math.Round(per3 * double.Parse(Gold.Text)) / double.Parse(mwood.Text.Substring(0, 4))).ToString(); 
                    if (item == 4)
                        Buytxt.Text = Math.Round(Math.Round(per4 * double.Parse(Gold.Text)) / double.Parse(moil.Text.Substring(0, 4))).ToString(); 
                    if(double.Parse(Buytxt.Text) > 0)
                        Buy_Click(null, null);
                }
                else if(double.Parse(size) > 0)
                {
                    if (!constant)
                    {
                        if (item == 1)
                            state1 = 1;
                        if (item == 2)
                            state2 = 1;
                        if (item == 3)
                            state3 = 1;
                        if (item == 4)
                            state4 = 1;
                    }
                    else
                    {
                        if (item == 1)
                            state1 = 3;
                        if (item == 2)
                            state2 = 3;
                        if (item == 3)
                            state3 = 3;
                        if (item == 4)
                            state4 = 3;
                    }
                    if (item == 1)
                        Selltxt.Text = Food.Text;
                    if (item == 2)
                        Selltxt.Text = Iron.Text;
                    if (item == 3)
                        Selltxt.Text = Wood.Text;
                    if (item == 4)
                        Selltxt.Text = Oil.Text;
                    if (double.Parse(Selltxt.Text) > 0)
                        Sell_Click(null, null);
                }
            }
            else
            {
                if (type == 1)
                {
                    if (item == 1)
                        state1 = 2;
                    if (item == 2)
                        state2 = 2;
                    if (item == 3)
                        state3 = 2;
                    if (item == 4)
                        state4 = 2;
                    if (beep)
                    {
                        Console.Beep();
                    }
                    else
                    {
                        Console.Beep();
                        SetForegroundWindow(this.Handle);
                    }
                    MessageBox.Show("Buy " + s + " now!", "Universal Market Trading System");
                }
                else
                {
                    if (item == 1)
                        state1 = 1;
                    if (item == 2)
                        state2 = 1;
                    if (item == 3)
                        state3 = 1;
                    if (item == 4)
                        state4 = 1;
                    if (beep)
                    {
                        Console.Beep();
                    }
                    else
                    {
                        Console.Beep();
                        SetForegroundWindow(this.Handle);
                    }
                    MessageBox.Show("Sell " + s + " now!", "Universal Market Trading System");
                }
            }
        }

        private void UpdateLog(string status)
        {
            Log.content += DateTime.Now.ToString() + ": " + status + "\r\n";
        }

        private void Info()
        {
            while (true)
            {
                //int count = 30;
                try
                {
                    //count++;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php");
                    request.CookieContainer = cc;
                    request.Method = "POST";
                    string postData = "";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    resstring = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    //
                    request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&show=");
                    request.CookieContainer = cc;
                    request.Method = "POST";
                    postData = "";
                    byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    char q = '"';
                    int i = 0;
                    if (responseFromServer.Contains("bot_checker"))
                    {
                        botcheck = true;
                        eid = responseFromServer.Substring(responseFromServer.IndexOf("game.php?&cmd=emp&ID=") + ("game.php?&cmd=emp&ID=").Length, 10).Remove(responseFromServer.Substring(responseFromServer.IndexOf("game.php?&cmd=emp&ID=") + ("game.php?&cmd=emp&ID=").Length, 10).IndexOf('"'));
                    }
                    else if (responseFromServer.Contains("Cargo Warehouse space: "))
                    {
                        size = responseFromServer.Substring(responseFromServer.IndexOf("Cargo Warehouse space: ") + ("Cargo Warehouse space: <span style=" + q + "font-weight:bold;" + q + ">").Length, 30).Remove(responseFromServer.Substring(responseFromServer.IndexOf("Cargo Warehouse space: ") + ("Cargo Warehouse space: <span style=" + q + "font-weight:bold;" + q + ">").Length, 30).IndexOf('/'));
                        //i = responseFromServer.IndexOf("sans-serif;" + q + "> ") + ("sans-serif;" + q + "> ").Length;
                        //high1 = double.Parse(responseFromServer.Substring(i, 4));
                        //low1 = double.Parse(responseFromServer.Substring(responseFromServer.IndexOf("sans-serif;" + q + "> ", i + 5) + ("sans-serif;" + q + "> ").Length, 4));
                    }

                    /*if (count >= 30)
                    {
                        request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&show=2");
                        request.CookieContainer = cc;
                        request.Method = "POST";
                        postData = "";
                        byteArray = Encoding.UTF8.GetBytes(postData);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        response = request.GetResponse();
                        dataStream = response.GetResponseStream();
                        reader = new StreamReader(dataStream);
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                        response.Close();

                        i = responseFromServer.IndexOf("sans-serif;" + q + "> ") + ("sans-serif;" + q + "> ").Length;
                        high2 = double.Parse(responseFromServer.Substring(i, 4));
                        low2 = double.Parse(responseFromServer.Substring(responseFromServer.IndexOf("sans-serif;" + q + "> ", i + 5) + ("sans-serif;" + q + "> ").Length, 4));

                        request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&show=3");
                        request.CookieContainer = cc;
                        request.Method = "POST";
                        postData = "";
                        byteArray = Encoding.UTF8.GetBytes(postData);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        response = request.GetResponse();
                        dataStream = response.GetResponseStream();
                        reader = new StreamReader(dataStream);
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                        response.Close();

                        i = responseFromServer.IndexOf("sans-serif;" + q + "> ") + ("sans-serif;" + q + "> ").Length;
                        high3 = double.Parse(responseFromServer.Substring(i, 4));
                        low3 = double.Parse(responseFromServer.Substring(responseFromServer.IndexOf("sans-serif;" + q + "> ", i + 5) + ("sans-serif;" + q + "> ").Length, 4));

                        request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&show=4");
                        request.CookieContainer = cc;
                        request.Method = "POST";
                        postData = "";
                        byteArray = Encoding.UTF8.GetBytes(postData);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        response = request.GetResponse();
                        dataStream = response.GetResponseStream();
                        reader = new StreamReader(dataStream);
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                        response.Close();

                        i = responseFromServer.IndexOf("sans-serif;" + q + "> ") + ("sans-serif;" + q + "> ").Length;
                        high4 = double.Parse(responseFromServer.Substring(i, 4));
                        low4 = double.Parse(responseFromServer.Substring(responseFromServer.IndexOf("sans-serif;" + q + "> ", i + 5) + ("sans-serif;" + q + "> ").Length, 4));

                        count = 0;
                    }*/

                    //

                    Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                char q = '"';

                i = resstring.IndexOf(q + "Gold" + q + " /></a> ") + (q + "Gold" + q + " /></a> ").Length;
                Gold.Text = resstring.Substring(i, 20).Remove(resstring.Substring(i, 20).IndexOf("<"));

                i = resstring.IndexOf(q + "Food" + q + " /></a> ") + (q + "Food" + q + " /></a> ").Length;
                Food.Text = resstring.Substring(i, 20).Remove(resstring.Substring(i, 20).IndexOf("<"));

                i = resstring.IndexOf(q + "Wood" + q + " /></a> ") + (q + "Wood" + q + " /></a> ").Length;
                Wood.Text = resstring.Substring(i, 20).Remove(resstring.Substring(i, 20).IndexOf("<"));

                i = resstring.IndexOf(q + "Iron" + q + " /></a> ") + (q + "Iron" + q + " /></a> ").Length;
                Iron.Text = resstring.Substring(i, 20).Remove(resstring.Substring(i, 20).IndexOf("<"));

                i = resstring.IndexOf(q + "Oil" + q + " /></a> ") + (q + "Oil" + q + " /></a> ").Length;
                Oil.Text = resstring.Substring(i, 20).Remove(resstring.Substring(i, 20).IndexOf("<"));
                if (resstring.Contains("Commodity"))
                {
                    i = resstring.IndexOf("1" + q + ">Food</a>:</td><td>") + ("1" + q + ">Food</a>:</td><td>0.40<img src=" + q + "../images/layout/arrow0").Length;
                    if (resstring[i] == '1')
                        mfood.ForeColor = Color.Green;
                    if (resstring[i] == '2')
                        mfood.ForeColor = Color.Red;
                    if (resstring[i] == '3')
                        mfood.ForeColor = Color.Black;

                    mfood.Text = resstring.Substring(i + 58, 17).Replace("</td><td>", "/");

                    i = resstring.IndexOf("3" + q + ">Wood</a>:</td><td>") + ("1" + q + ">Wood</a>:</td><td>0.40<img src=" + q + "../images/layout/arrow0").Length;
                    if (resstring[i] == '1')
                        mwood.ForeColor = Color.Green;
                    if (resstring[i] == '2')
                        mwood.ForeColor = Color.Red;
                    if (resstring[i] == '3')
                        mwood.ForeColor = Color.Black;

                    mwood.Text = resstring.Substring(i + 58, 17).Replace("</td><td>", "/");

                    i = resstring.IndexOf("2" + q + ">Iron</a>:</td><td>") + ("1" + q + ">Iron</a>:</td><td>0.40<img src=" + q + "../images/layout/arrow0").Length;
                    if (resstring[i] == '1')
                        miron.ForeColor = Color.Green;
                    if (resstring[i] == '2')
                        miron.ForeColor = Color.Red;
                    if (resstring[i] == '3')
                        miron.ForeColor = Color.Black;

                    miron.Text = resstring.Substring(i + 58, 17).Replace("</td><td>", "/");

                    i = resstring.IndexOf("4" + q + ">Oil</a>:</td><td>") + ("1" + q + ">Oil</a>:</td><td>0.40<img src=" + q + "../images/layout/arrow0").Length;
                    if (resstring[i] == '1')
                        moil.ForeColor = Color.Green;
                    if (resstring[i] == '2')
                        moil.ForeColor = Color.Red;
                    if (resstring[i] == '3')
                        moil.ForeColor = Color.Black;

                    moil.Text = resstring.Substring(i + 58, 17).Replace("</td><td>", "/");

                    space.Text = size;

                    if(resstring.Contains("Mercantile:</td><td>"))
                        groupBox2.Text = "Market [" + resstring.Substring(resstring.IndexOf("Mercantile:</td><td>") + ("Mercantile:</td><td>").Length,3).Remove(resstring.Substring(resstring.IndexOf("Mercantile:</td><td>") + ("Mercantile:</td><td>").Length,3).IndexOf("<")) + "]";
                    //
                    
                    //Alarm System
                    if (state1 > 0 && peak1)
                    {
                        if (state1 == 3 && double.Parse(mfood.Text.Substring(0, 4)) <= buy1 + .01 && mfood.ForeColor == Color.Green)
                            Alarm(1, 1, "food", beep1, auto1, const1);
                        if (state1 == 3 && double.Parse(mfood.Text.Substring(5, 4)) >= sell1 - .01 && mfood.ForeColor == Color.Red)
                            Alarm(2, 1, "food", beep1, auto1, const1);
                        if (state1 == 1 && double.Parse(mfood.Text.Substring(0, 4)) <= buy1 + .01 && mfood.ForeColor == Color.Green)
                            Alarm(1, 1, "food", beep1, auto1, const1);
                        if (state1 == 2 && double.Parse(mfood.Text.Substring(5, 4)) >= sell1 - .01 && mfood.ForeColor == Color.Red)
                            Alarm(2, 1, "food", beep1, auto1, const1);
                    }
                    else if (state1 > 0)
                    {
                        if (state1 == 3 && double.Parse(mfood.Text.Substring(0, 4)) <= buy1)
                            Alarm(1, 1, "food", beep1, auto1, const1);
                        if (state1 == 3 && double.Parse(mfood.Text.Substring(5, 4)) >= sell1)
                            Alarm(2, 1, "food", beep1, auto1, const1);
                        if (state1 == 1 && double.Parse(mfood.Text.Substring(0, 4)) <= buy1)
                            Alarm(1, 1, "food", beep1, auto1, const1);
                        if (state1 == 2 && double.Parse(mfood.Text.Substring(5, 4)) >= sell1)
                            Alarm(2, 1, "food", beep1, auto1, const1);
                    }
                    if (state2 > 0 && peak2)
                    {
                        if (state2 == 3 && double.Parse(miron.Text.Substring(0, 4)) <= buy2 + .01 && miron.ForeColor == Color.Green)
                            Alarm(1, 2, "iron", beep2, auto2, const2);
                        if (state2 == 3 && double.Parse(miron.Text.Substring(5, 4)) >= sell2 - .01 && miron.ForeColor == Color.Red)
                            Alarm(2, 2, "iron", beep2, auto2, const2);
                        if (state2 == 1 && double.Parse(miron.Text.Substring(0, 4)) <= buy2 + .01 && miron.ForeColor == Color.Green)
                            Alarm(1, 2, "iron", beep2, auto2, const2);
                        if (state2 == 2 && double.Parse(miron.Text.Substring(5, 4)) >= sell2 - .01 && miron.ForeColor == Color.Red)
                            Alarm(2, 2, "iron", beep2, auto2, const2);
                    }
                    else if (state2 > 0)
                    {
                        if (state2 == 3 && double.Parse(miron.Text.Substring(0, 4)) <= buy2)
                            Alarm(1, 2, "iron", beep2, auto2, const2);
                        if (state2 == 3 && double.Parse(miron.Text.Substring(5, 4)) >= sell2)
                            Alarm(2, 2, "iron", beep2, auto2, const2);
                        if (state2 == 1 && double.Parse(miron.Text.Substring(0, 4)) <= buy2)
                            Alarm(1, 2, "iron", beep2, auto2, const2);
                        if (state2 == 2 && double.Parse(miron.Text.Substring(5, 4)) >= sell2)
                            Alarm(2, 2, "iron", beep2, auto2, const2);
                    }
                    if (state3 > 0 && peak3)
                    {
                        if (state3 == 3 && double.Parse(mwood.Text.Substring(0, 4)) <= buy3 + .01 && mwood.ForeColor == Color.Green)
                            Alarm(1, 3, "wood", beep3, auto3, const3);
                        if (state3 == 3 && double.Parse(mwood.Text.Substring(5, 4)) >= sell3 - .01 && mwood.ForeColor == Color.Red)
                            Alarm(2, 3, "wood", beep3, auto3, const3);
                        if (state3 == 1 && double.Parse(mwood.Text.Substring(0, 4)) <= buy3 + .01 && mwood.ForeColor == Color.Green)
                            Alarm(1, 3, "wood", beep3, auto3, const3);
                        if (state3 == 2 && double.Parse(mwood.Text.Substring(5, 4)) >= sell3 - .01 && mwood.ForeColor == Color.Red)
                            Alarm(2, 3, "wood", beep3, auto3, const3);
                    }
                    else if (state3 > 0)
                    {
                        if (state3 == 3 && double.Parse(mwood.Text.Substring(0, 4)) <= buy3)
                            Alarm(1, 3, "wood", beep3, auto3, const3);
                        if (state3 == 3 && double.Parse(mwood.Text.Substring(5, 4)) >= sell3)
                            Alarm(2, 3, "wood", beep3, auto3, const3);
                        if (state3 == 1 && double.Parse(mwood.Text.Substring(0, 4)) <= buy3)
                            Alarm(1, 3, "wood", beep3, auto3, const3);
                        if (state3 == 2 && double.Parse(mwood.Text.Substring(5, 4)) >= sell3)
                            Alarm(2, 3, "wood", beep3, auto3, const3);
                    }
                    if (state4 > 0 && peak4)
                    {
                        if (state4 == 3 && double.Parse(moil.Text.Substring(0, 4)) <= buy4 + .01 && moil.ForeColor == Color.Green)
                            Alarm(1, 4, "oil", beep4, auto4, const4);
                        if (state4 == 3 && double.Parse(moil.Text.Substring(5, 4)) >= sell4 - .01 && moil.ForeColor == Color.Red)
                            Alarm(2, 4, "oil", beep4, auto4, const4);
                        if (state4 == 1 && double.Parse(moil.Text.Substring(0, 4)) <= buy4 + .01 && moil.ForeColor == Color.Green)
                            Alarm(1, 4, "oil", beep4, auto4, const4);
                        if (state4 == 2 && double.Parse(moil.Text.Substring(5, 4)) >= sell4 - .01 && moil.ForeColor == Color.Red)
                            Alarm(2, 4, "oil", beep4, auto4, const4);
                    }
                    else if (state4 > 0)
                    {
                        if (state4 == 3 && double.Parse(moil.Text.Substring(0, 4)) <= buy4)
                            Alarm(1, 4, "oil", beep4, auto4, const4);
                        if (state4 == 3 && double.Parse(moil.Text.Substring(5, 4)) >= sell4)
                            Alarm(2, 4, "oil", beep4, auto4, const4);
                        if (state4 == 1 && double.Parse(moil.Text.Substring(0, 4)) <= buy4)
                            Alarm(1, 4, "oil", beep4, auto4, const4);
                        if (state4 == 2 && double.Parse(moil.Text.Substring(5, 4)) >= sell4)
                            Alarm(2, 4, "oil", beep4, auto4, const4);
                    }
                    //
                }
            }
            catch (Exception z){}
        }

        private void UMTS_Load(object sender, EventArgs e)
        {
            _thread = new Thread(new ThreadStart(Info));
            _thread.IsBackground = true;
            _thread.Name = "Updater";
            _thread.Start();
        }

        private void UMTS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.canclose = true;
            Application.Exit();
        }

        private bool Updatex()
        {
            if (double.Parse(BuyAt.Text) > double.Parse(SellAt.Text))
            {
                MessageBox.Show("You're supposed to... Buy LOW! Sell HIGH!", "Universal Market Trading System");
                return false;
            }
            try
            {
                if (item == 1)
                {
                    buy1 = double.Parse(BuyAt.Text);
                    sell1 = double.Parse(SellAt.Text);
                    peak1 = Peak.Checked;
                    auto1 = Trade.Checked;
                    beep1 = BeepAl.Checked;
                    per1 = double.Parse(PerAt.Text) / 100;
                    const1 = ctrade.Checked;
                }
                else if (item == 2)
                {
                    buy2 = double.Parse(BuyAt.Text);
                    sell2 = double.Parse(SellAt.Text);
                    peak2 = Peak.Checked;
                    auto2 = Trade.Checked;
                    beep2 = BeepAl.Checked;
                    per2 = double.Parse(PerAt.Text) / 100;
                    const2 = ctrade.Checked;
                }
                else if (item == 3)
                {
                    buy3 = double.Parse(BuyAt.Text);
                    sell3 = double.Parse(SellAt.Text);
                    peak3 = Peak.Checked;
                    auto3 = Trade.Checked;
                    beep3 = BeepAl.Checked;
                    per3 = double.Parse(PerAt.Text) / 100;
                    const3 = ctrade.Checked;
                }
                else
                {
                    buy4 = double.Parse(BuyAt.Text);
                    sell4 = double.Parse(SellAt.Text);
                    peak4 = Peak.Checked;
                    auto4 = Trade.Checked;
                    beep4 = BeepAl.Checked;
                    per4 = double.Parse(PerAt.Text) / 100;
                    const4 = ctrade.Checked;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill out everything.", "Universal Market Trading System");
                return false;
            }
            return true;
        }

        private void tab1_Click(object sender, EventArgs e)
        {
            item = 1;
            AlarmBox.Text = "Alarm - Food";
            TradeBox.Text = "Buy/Sell - Food";
            BuyAt.Text = buy1.ToString();
            SellAt.Text = sell1.ToString();
            Peak.Checked = peak1;
            Trade.Checked = auto1;
            BeepAl.Checked = beep1;
            PopupAl.Checked = !beep1;
            Buytxt.Text = "";
            Selltxt.Text = "";
            PerAt.Text = (per1 * 100).ToString();
            ctrade.Checked = const1;
            if (state1 > 0)
            {
                button2.Text = "Stop";
            }
            else
            {
                button2.Text = "Start";
            }
        }

        private void tab2_Click(object sender, EventArgs e)
        {
            item = 3;
            AlarmBox.Text = "Alarm - Wood";
            TradeBox.Text = "Buy/Sell - Wood";
            BuyAt.Text = buy3.ToString();
            SellAt.Text = sell3.ToString();
            Peak.Checked = peak3;
            Trade.Checked = auto3;
            BeepAl.Checked = beep3;
            PopupAl.Checked = !beep3;
            Buytxt.Text = "";
            Selltxt.Text = "";
            PerAt.Text = (per3 * 100).ToString();
            ctrade.Checked = const3;
            if (state3 > 0)
            {
                button2.Text = "Stop";
            }
            else
            {
                button2.Text = "Start";
            }
        }

        private void tab3_Click(object sender, EventArgs e)
        {
            item = 2;
            AlarmBox.Text = "Alarm - Iron";
            TradeBox.Text = "Buy/Sell - Iron";
            BuyAt.Text = buy2.ToString();
            SellAt.Text = sell2.ToString();
            Peak.Checked = peak2;
            Trade.Checked = auto2;
            BeepAl.Checked = beep2;
            PopupAl.Checked = !beep2;
            Buytxt.Text = "";
            Selltxt.Text = "";
            PerAt.Text = (per2 * 100).ToString();
            ctrade.Checked = const2;
            if (state2 > 0)
            {
                button2.Text = "Stop";
            }
            else
            {
                button2.Text = "Start";
            }
        }

        private void tab4_Click(object sender, EventArgs e)
        {
            item = 4;
            AlarmBox.Text = "Alarm - Oil";
            TradeBox.Text = "Buy/Sell - Oil";
            BuyAt.Text = buy4.ToString();
            SellAt.Text = sell4.ToString();
            Peak.Checked = peak4;
            Trade.Checked = auto4;
            BeepAl.Checked = beep4;
            PopupAl.Checked = !beep4;
            Buytxt.Text = "";
            Selltxt.Text = "";
            PerAt.Text = (per4 * 100).ToString();
            ctrade.Checked = const4;
            if (state4 > 0)
            {
                button2.Text = "Stop";
            }
            else
            {
                button2.Text = "Start";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Updatex())
                MessageBox.Show("Settings saved.", "Universal Market Trading System");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                if (item == 1)
                    Buytxt.Text = (Math.Floor(double.Parse(Gold.Text) / double.Parse(mfood.Text.Substring(0, 4)))).ToString();
                if (item == 2)
                    Buytxt.Text = (Math.Floor(double.Parse(Gold.Text) / double.Parse(miron.Text.Substring(0, 4)))).ToString();
                if (item == 3)
                    Buytxt.Text = (Math.Floor(double.Parse(Gold.Text) / double.Parse(mwood.Text.Substring(0, 4)))).ToString();
                if (item == 4)
                    Buytxt.Text = (Math.Floor(double.Parse(Gold.Text) / double.Parse(moil.Text.Substring(0, 4)))).ToString();
                if (Buytxt.Text == "Infinity")
                    Buytxt.Text = "0";
                if (double.Parse(Buytxt.Text) > double.Parse(size))
                    Buytxt.Text = size;
            }
            catch (Exception b)
            { }
        }

        private void bot_Tick(object sender, EventArgs e)
        {
            if (botcheck)
            {
                if (Checker.eid == "")
                {
                    _thread.Suspend();
                    Checker checker = new Checker();
                    Checker.eid = eid;
                    Checker.cc = cc;
                    Checker.round = round;
                    checker.Show();
                    this.Visible = false;
                }
            }
            else
            {
                ThreadState state = ThreadState.Background | ThreadState.Suspended;
                if (_thread.ThreadState == state)
                {
                    this.Visible = true;
                    _thread.Resume();
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            try
            {
                if (item == 1)
                    Selltxt.Text = Food.Text;
                if (item == 2)
                    Selltxt.Text = Iron.Text;
                if (item == 3)
                    Selltxt.Text = Wood.Text;
                if (item == 4)
                    Selltxt.Text = Oil.Text;
                if (double.Parse(Selltxt.Text) > double.Parse(size))
                    Selltxt.Text = size;
            }
            catch (Exception b)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Start")
            {
                if (Updatex())
                {
                    if (item == 1)
                        state1 = 3;
                    if (item == 2)
                        state2 = 3;
                    if (item == 3)
                        state3 = 3;
                    if (item == 4)
                        state4 = 3;
                    button2.Text = "Stop";
                    MessageBox.Show("Settings saved and Alarm started.", "Universal Market Trading System");
                }
            }
            else
            {
                if (item == 1)
                    state1 = 0;
                if (item == 2)
                    state2 = 0;
                if (item == 3)
                    state3 = 0;
                if (item == 4)
                    state4 = 0;
                button2.Text = "Start";
                MessageBox.Show("Alarm stopped.", "Universal Market Trading System");
            }
        }

        private void Buy_Click(object sender, EventArgs e)
        {
            string limit = "0";
            try
            {
                if (item == 1)
                    limit = (Math.Floor(double.Parse(Gold.Text) / double.Parse(mfood.Text.Substring(0, 4)))).ToString();
                if (item == 2)
                    limit = (Math.Floor(double.Parse(Gold.Text) / double.Parse(miron.Text.Substring(0, 4)))).ToString();
                if (item == 3)
                    limit = (Math.Floor(double.Parse(Gold.Text) / double.Parse(mwood.Text.Substring(0, 4)))).ToString();
                if (item == 4)
                    limit = (Math.Floor(double.Parse(Gold.Text) / double.Parse(moil.Text.Substring(0, 4)))).ToString();
                if (limit == "Infinity")
                    limit = "0";
                if (double.Parse(Buytxt.Text) > double.Parse(size))
                    limit = size;
            }
            catch (Exception b)
            { }
            bool success = false;
            try
            {
                transaction++;
                if (double.Parse(Buytxt.Text) > 20000000)
                {
                    Buytxt.Text = "20000000";
                }
                if (double.Parse(Buytxt.Text) > double.Parse(limit))
                {
                    Buytxt.Text = limit.ToString();
                }
                if (double.Parse(Buytxt.Text) <= 0)
                {
                    UpdateLog("Transaction Error!" + " [" + transaction.ToString() + "]");
                    return;
                }
                _thread.Suspend();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&do=buy");
                request.CookieContainer = cc;
                request.Method = "POST";
                string postData = "buy_amount=" + (double.Parse(Buytxt.Text)).ToString() + "&show=" + item.ToString() + "&Submit=Buy";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                resstring = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                double val = 0;
                string res = "";
                if (item == 1)
                {
                    val = (double.Parse(Buytxt.Text) * double.Parse(mfood.Text.Substring(0, 4)));
                    res = "food";
                }
                if (item == 2)
                {
                    val = (double.Parse(Buytxt.Text) * double.Parse(miron.Text.Substring(0, 4)));
                    res = "iron";
                }
                if (item == 3)
                {
                    val = (double.Parse(Buytxt.Text) * double.Parse(mwood.Text.Substring(0, 4)));
                    res = "wood";
                }
                if (item == 4)
                {
                    val = (double.Parse(Buytxt.Text) * double.Parse(moil.Text.Substring(0, 4)));
                    res = "oil";
                }
                UpdateLog("We bought " + Buytxt.Text + " " + res + " for " + Math.Ceiling(val).ToString() + " gold." + " [" + transaction.ToString() + "]");
                Log.profit -= (int)Math.Ceiling(val);
                success = true;
                _thread.Resume();
            }
            catch (Exception z)
            {
                if(!success)
                    UpdateLog("Transaction Error!" + " [" + transaction.ToString() + "]");
            }
        }

        private void Sell_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(Selltxt.Text) > double.Parse(size))
                    Selltxt.Text = size;
            }
            catch (Exception b)
            { }
            bool success = false;
            try
            {
                transaction++;
                if (double.Parse(Selltxt.Text) > 20000000)
                {
                    Selltxt.Text = "20000000";
                }
                if (double.Parse(Selltxt.Text) <= 0)
                {
                    UpdateLog("Transaction Error!" + " [" + transaction.ToString() + "]");
                    return;
                }
                _thread.Suspend();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&do=sell");
                request.CookieContainer = cc;
                request.Method = "POST";
                string postData = "sell_amount=" + (double.Parse(Selltxt.Text)).ToString() + "&show=" + item.ToString() + "&Submit=Sell";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                resstring = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                double val = 0;
                string res = "";
                if (item == 1)
                {
                    val = (double.Parse(Selltxt.Text) * double.Parse(mfood.Text.Substring(5, 4)));
                    res = "food";
                }
                if (item == 2)
                {
                    val = (double.Parse(Selltxt.Text) * double.Parse(miron.Text.Substring(5, 4)));
                    res = "iron";
                }
                if (item == 3)
                {
                    val = (double.Parse(Selltxt.Text) * double.Parse(mwood.Text.Substring(5, 4)));
                    res = "wood";
                }
                if (item == 4)
                {
                    val = (double.Parse(Selltxt.Text) * double.Parse(moil.Text.Substring(5, 4)));
                    res = "oil";
                }
                UpdateLog("We sold " + Selltxt.Text + " " + res + " for " + Math.Floor(val).ToString() + " gold." + " [" + transaction.ToString() + "]");
                Log.profit += (int)Math.Floor(val);
                success = true;
                _thread.Resume();
            }
            catch (Exception z)
            {
                if(!success)
                    UpdateLog("Transaction Error!" + " [" + transaction.ToString() + "]");
            }
        }

        private void UMTS_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                invis = true;
                locked.Enabled = true;
            }
        }

        private void locked_Tick(object sender, EventArgs e)
        {
            try
            {
                if (GetAsyncKeyState(8))
                    passcode = "";
                int i = 64;
                while (i < 90)
                {
                    i++;
                    if (passcode.Length > 0)
                    {
                        if (GetAsyncKeyState(i) && passcode[passcode.Length - 1] != alpha[90 - i])
                            passcode += alpha[90 - i];
                    }
                    else
                    {
                        if (GetAsyncKeyState(i))
                            passcode += alpha[90 - i];
                    }
                }
                if (passcode.Contains("unlockumts"))
                {
                    passcode = "";
                    this.Visible = true;
                    invis = false;
                    locked.Enabled = false;
                }
            }
            catch (Exception a) { }
        }
    }
}
