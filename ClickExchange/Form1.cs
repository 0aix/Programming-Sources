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
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;


namespace ClickExchange
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey); 

        static CookieContainer ccc = new CookieContainer();
        static CookieContainer ccx = new CookieContainer();
        static string user = "iluveplushies";
        static string pass = "itsasecret";
        static string ceid = "116/116020.gif";
        static string[] bmplist = new string[10000];
        static int[] answers = new int[10000];
        static int stack = 0;
        static StreamWriter tw;
        static StreamReader tr;
        static StreamReader tra;
        static bool autom = false;
        static Thread thr = new Thread(Macro);
        public static int pokeballs = 0;
        public int autoindex = 0;
        public string binbmp = "";
        System.Diagnostics.Process firefox = new System.Diagnostics.Process();
        public bool capt = false;
        public int death = 0;
        public int bdeath = 0;
        string gbdeath = "";
        public bool start = true;
        public bool gbc = false;
        public string bond = "";
        public string form = "";

        public Form1()
        {
            InitializeComponent();
        }

        private string Compare(Bitmap b)
        {
            try
            {
                Color col;
                string hb = "";
                for (int x = 0; x < b.Width; x++)
                {
                    //for (int y = 16; y < 131; y++)
                    //{
                        col = b.GetPixel(x, 80);
                        if (col.R > 250 && col.G > 250 && col.B > 250)
                        {
                            hb += "1";
                        }
                        else
                        {
                            hb += "0";
                        }
                    //}
                }

                int wrong = 0;
                for (int i = 0; i < stack; i++)
                {
                    wrong = 0;
                    for (int z = 0; z < hb.Length; z++)
                    {
                        if (wrong > 20)
                            break;
                        if (hb[z] != bmplist[i][z])
                            wrong++;
                    }
                    if (wrong <= 20)
                    {
                        autoindex = i;
                        binbmp = hb;
                        return "";
                    }
                }
                binbmp = hb;
                return hb;
            }
            catch (Exception e) { return ""; }
        }

        private void Login()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/login.php?act=dologin");
                request.CookieContainer = ccc;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                string postData = "username=" + user + "&password=" + pass;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                death = 0;
                if (start)
                {
                    start = false;
                    FormLoad2();
                }
            }
            catch (Exception q) 
            {
                if (death == 50)
                {
                    death = 0;
                    timer2.Enabled = true;
                }
                else
                {
                    death++;
                    Login();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tr = new StreamReader("Answers.txt");
            tra = new StreamReader("Bmps.txt");
            string a = tr.ReadLine();
            try
            {
                for (int i = 0; i > -1; i++)
                {
                    bmplist[i] = tra.ReadLine();
                    answers[i] = int.Parse(a[i].ToString());
                    stack++;
                }
            }
            catch (Exception z) { }
            tr.Close();
            tra.Close();
            firefox.StartInfo.FileName = "firefox";
            Login();
        }

        private void Invest1()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/gbc_exchange.php");
                request.CookieContainer = ccc;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                string q = "'";
                string zz = q + "form_token" + q + " value=" + q;
                int i = responseFromServer.IndexOf(zz) + zz.Length;
                int y = 0;
                for (int x = 0; responseFromServer[i + x] != q[0]; x++)
                {
                    y = x;
                }
                form = responseFromServer.Substring(i, y + 1);
                Invest2(form);
            }
            catch (Exception q)
            {
                bdeath = 7;
                timer3.Enabled = true;
            }
        }

        private void Invest2(string form)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/gbc_exchange.php?act=buygbc");
                request.CookieContainer = ccc;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                string postData = "form_token=" + form + "&gbc2buy=1";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                int i = responseFromServer.IndexOf("'/images/adoptables/") + "'/images/adoptables/".Length;
                int z = 0;
                for (int x = 0; responseFromServer[i + x] != '.'; x++)
                {
                    z = x;
                }
                string a = responseFromServer.Substring(i, z + 1);
                bond = a;
            }
            catch (Exception q)
            {
                bdeath = 8;
                timer3.Enabled = true;
            }
        }

        private void Set()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/clickexchange.php?act=choose");
                request.CookieContainer = ccc;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                string postData = "adoptID=" + bond + "&blackBC=1";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception q)
            {
                bdeath = 9;
                timer3.Enabled = true;
            }
        }

        private void Sell()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/gbc_exchange.php?act=tradein");
                request.CookieContainer = ccc;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                string postData = "id=" + bond;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception q)
            {
                bdeath = 10;
                timer3.Enabled = true;
            }
        }

        private void FormLoad2()
        {
            ccx = ccc;
            thr.Start();
            timer1.Enabled = true;
            if (gbc)
            {
                Invest1();
                Set();
            }
            GoBack("clickexchange.php");
        }

        private void Captcha()
        {
            ccc = new CookieContainer();
            Login();
            if (!checkBox1.Checked)
            {
                MessageBox.Show("Captcha!");
            }
            else
            {
                capt = true;
            }
            GoBack("clickexchange.php");
        }

        private void GoBackX()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/clickexchange.php");
                request.CookieContainer = ccc;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                int i = responseFromServer.IndexOf("a href='/clickexchange.php") + 9;
                int z = 0;
                string a = "'";
                for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                {
                    z = y;
                }
                string back = "";
                if (i > 50)
                {
                    back = responseFromServer.Substring(i, z + 1);
                }
                else
                {
                    back = "clickexchange.php";
                }
                GoBack(back);
            }
            catch (Exception e)
            {
                bdeath = 5;
                timer3.Enabled = true;
            }
        }

        private void GoBack(string b)
        {
            if (!capt)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/" + b);
                    request.CookieContainer = ccc;
                    request.Method = "POST";
                    request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                    //request.Proxy = new WebProxy("83.219.90.23", 80);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    if (responseFromServer.Contains("recaptcha"))
                    {
                        Captcha();
                    }
                    else
                    {
                        int i = responseFromServer.IndexOf("Credits: </strong>") + 18;
                        int z = 0;
                        for (int x = 0; responseFromServer[i + x] != '<'; x++)
                        {
                            z = x;
                        }
                        label2.Text = "Credits: " + responseFromServer.Substring(i, z + 1);
                        if (gbc)
                        {
                            try
                            {
                                int creds = int.Parse(responseFromServer.Substring(i, z + 1));
                                if (creds > 75)
                                {
                                    Sell();
                                    Invest1();
                                    Set();
                                }
                            }
                            catch (Exception q) { }
                        }
                        i = responseFromServer.IndexOf("Level:</strong> ") + 16;
                        z = 0;
                        for (int x = 0; responseFromServer[i + x] != '<'; x++)
                        {
                            z = x;
                        }
                        label1.Text = "Level: " + responseFromServer.Substring(i, z + 1);
                        Question.ImageLocation = "http://www.pokeplushies.com/images/cepics/" + ceid;
                    }
                }
                catch (Exception q)
                {
                    bdeath = 6;
                    gbdeath = b;
                    timer3.Enabled = true;
                }
            }
            else
            {
                firefox.StartInfo.Arguments = "http://www.pokeplushies.com/clickexchange.php";
                firefox.Start();
                Thread.Sleep(2000);
                Question.ImageLocation = "http://www.pokeplushies.com/images/cepics/" + ceid;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            if (!capt)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=1");
                    request.CookieContainer = ccc;
                    request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                    //request.Proxy = new WebProxy("83.219.90.23", 80);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    if (responseFromServer.Contains("You are not welcome")) //Proxy
                    {
                        ccc = new CookieContainer();
                        Login();
                        ccx = ccc;
                        if (gbc)
                        {
                            Invest1();
                            Set();
                        }
                        GoBack("clickexchange.php");
                    }
                    else
                    {
                        int i = responseFromServer.IndexOf("a href='/clickexchange.php") + 9;
                        int z = 0;
                        string a = "'";
                        for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                        {
                            z = y;
                        }
                        string back = "";
                        if (i > 50)
                        {
                            back = responseFromServer.Substring(i, z + 1);
                        }
                        else
                        {
                            back = "clickexchange.php";
                        }
                        //
                        i = responseFromServer.IndexOf("/50</div>");
                        if (i > 1)
                        {
                            z = 0;
                            for (int x = 0; responseFromServer[i - x] != '>'; x++)
                            {
                                z = x;
                            }
                            label3.Text = "Clicks: " + responseFromServer.Substring(i - z, z);
                        }
                        //
                        //
                        if (!autom)
                        {
                            string hval = binbmp;
                            if (hval.Length > 1)
                            {
                                tw = new StreamWriter("Answers.txt", true);
                                tw.Write("1");
                                tw.Close();
                                tw = new StreamWriter("Bmps.txt", true);
                                tw.WriteLine(hval);
                                tw.Close();
                                bmplist[stack] = hval;
                                answers[stack] = 1;
                                stack++;
                            }
                        }
                        autom = false;
                        //
                        GoBack(back);
                    }
                }
                catch (Exception q)
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    if (tw != null)
                        tw.Close();
                    bdeath = 1;
                    timer3.Enabled = true;
                }
            }
            else
            {
                firefox.StartInfo.Arguments = "http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=1";
                firefox.Start();
                Thread.Sleep(2000);
                capt = false;
                GoBackX();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            if (!capt)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=2");
                    request.CookieContainer = ccc;
                    request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                    //request.Proxy = new WebProxy("83.219.90.23", 80);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    if (responseFromServer.Contains("You are not welcome")) //Proxy
                    {
                        ccc = new CookieContainer();
                        Login();
                        ccx = ccc;
                        if (gbc)
                        {
                            Invest1();
                            Set();
                        }
                        GoBack("clickexchange.php");
                    }
                    else
                    {
                        int i = responseFromServer.IndexOf("a href='/clickexchange.php") + 9;
                        int z = 0;
                        string a = "'";
                        for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                        {
                            z = y;
                        }
                        string back = "";
                        if (i > 50)
                        {
                            back = responseFromServer.Substring(i, z + 1);
                        }
                        else
                        {
                            back = "clickexchange.php";
                        }
                        //
                        i = responseFromServer.IndexOf("/50</div>");
                        if (i > 1)
                        {
                            z = 0;
                            for (int x = 0; responseFromServer[i - x] != '>'; x++)
                            {
                                z = x;
                            }
                            label3.Text = "Clicks: " + responseFromServer.Substring(i - z, z);
                        }
                        //
                        //
                        if (!autom)
                        {
                            string hval = binbmp;
                            if (hval.Length > 1)
                            {
                                tw = new StreamWriter("Answers.txt", true);
                                tw.Write("2");
                                tw.Close();
                                tw = new StreamWriter("Bmps.txt", true);
                                tw.WriteLine(hval);
                                tw.Close();
                                bmplist[stack] = hval;
                                answers[stack] = 2;
                                stack++;
                            }
                        }
                        autom = false;
                        //
                        GoBack(back);
                    }
                }
                catch (Exception q)
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    if (tw != null)
                        tw.Close();
                    bdeath = 2;
                    timer3.Enabled = true;
                }
            }
            else
            {
                firefox.StartInfo.Arguments = "http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=2";
                firefox.Start();
                Thread.Sleep(2000);
                capt = false;
                GoBackX();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            if (!capt)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=3");
                    request.CookieContainer = ccc;
                    request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                    //request.Proxy = new WebProxy("83.219.90.23", 80);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    if (responseFromServer.Contains("You are not welcome")) //Proxy
                    {

                        ccc = new CookieContainer();
                        Login();
                        ccx = ccc;
                        if (gbc)
                        {
                            Invest1();
                            Set();
                        }
                        GoBack("clickexchange.php");
                    }
                    else
                    {
                        int i = responseFromServer.IndexOf("a href='/clickexchange.php") + 9;
                        int z = 0;
                        string a = "'";
                        for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                        {
                            z = y;
                        }
                        string back = "";
                        if (i > 50)
                        {
                            back = responseFromServer.Substring(i, z + 1);
                        }
                        else
                        {
                            back = "clickexchange.php";
                        }
                        //
                        i = responseFromServer.IndexOf("/50</div>");
                        if (i > 1)
                        {
                            z = 0;
                            for (int x = 0; responseFromServer[i - x] != '>'; x++)
                            {
                                z = x;
                            }
                            label3.Text = "Clicks: " + responseFromServer.Substring(i - z, z);
                        }
                        //
                        //
                        if (!autom)
                        {
                            string hval = binbmp;
                            if (hval.Length > 1)
                            {
                                tw = new StreamWriter("Answers.txt", true);
                                tw.Write("3");
                                tw.Close();
                                tw = new StreamWriter("Bmps.txt", true);
                                tw.WriteLine(hval);
                                tw.Close();
                                bmplist[stack] = hval;
                                answers[stack] = 3;
                                stack++;
                            }
                        }
                        autom = false;
                        //
                        GoBack(back);
                    }
                }
                catch (Exception q)
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    if (tw != null)
                        tw.Close();
                    bdeath = 3;
                    timer3.Enabled = true;
                }
            }
            else
            {
                firefox.StartInfo.Arguments = "http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=3";
                firefox.Start();
                Thread.Sleep(2000);
                capt = false;
                GoBackX();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            if (!capt)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=4");
                    request.CookieContainer = ccc;
                    request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                    //request.Proxy = new WebProxy("83.219.90.23", 80);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    if (responseFromServer.Contains("You are not welcome")) //Proxy
                    {
                        ccc = new CookieContainer();
                        Login();
                        ccx = ccc;
                        if (gbc)
                        {
                            Invest1();
                            Set();
                        }
                        GoBack("clickexchange.php");
                    }
                    else
                    {
                        int i = responseFromServer.IndexOf("a href='/clickexchange.php") + 9;
                        int z = 0;
                        string a = "'";
                        for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                        {
                            z = y;
                        }
                        string back = "";
                        if (i > 50)
                        {
                            back = responseFromServer.Substring(i, z + 1);
                        }
                        else
                        {
                            back = "clickexchange.php";
                        }
                        //
                        i = responseFromServer.IndexOf("/50</div>");
                        if (i > 1)
                        {
                            z = 0;
                            for (int x = 0; responseFromServer[i - x] != '>'; x++)
                            {
                                z = x;
                            }
                            label3.Text = "Clicks: " + responseFromServer.Substring(i - z, z);
                        }
                        //
                        //
                        if (!autom)
                        {
                            string hval = binbmp;
                            if (hval.Length > 1)
                            {
                                tw = new StreamWriter("Answers.txt", true);
                                tw.Write("4");
                                tw.Close();
                                tw = new StreamWriter("Bmps.txt", true);
                                tw.WriteLine(hval);
                                tw.Close();
                                bmplist[stack] = hval;
                                answers[stack] = 4;
                                stack++;
                            }
                        }
                        autom = false;
                        //
                        GoBack(back);
                    }
                }
                catch (Exception q)
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    if (tw != null)
                        tw.Close();
                    bdeath = 4;
                    timer3.Enabled = true;
                }
            }
            else
            {
                firefox.StartInfo.Arguments = "http://www.pokeplushies.com/clickexchange.php?act=doCE&answer=4";
                firefox.Start();
                Thread.Sleep(2000);
                capt = false;
                GoBackX();
            }
        }

        public static void Page(string b)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/" + b);
                request.CookieContainer = ccx;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception q) { }
        }

        public static void Dig()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/dirtdigger.php");
                request.CookieContainer = ccx;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                if (responseFromServer.Contains("act=play&dirt=1"))
                {
                    int i = responseFromServer.IndexOf("dirtdigger.php?act=play");
                    int z = 0;
                    string a = "'";
                    for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                    {
                        z = y;
                    }
                    string url = responseFromServer.Substring(i, z + 1);
                    Page(url);
                }
            }
            catch (Exception q) { }
        }

        public static void GuessX(string b)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/guessthenumber.php?act=enter");
                request.CookieContainer = ccx;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                string postData = "guess=5124&code=" + b;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception q) { }
        }

        public static void Guess()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/guessthenumber.php");
                request.CookieContainer = ccx;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                if (responseFromServer.Contains("guessthenumber.php?act=enter"))
                {
                    string a = "'";
                    int i = responseFromServer.IndexOf("name=" + a + "code" + a + " value=" + a) + 19;
                    int z = 0;
                    for (int y = 1; responseFromServer[i + y].ToString() != a; y++)
                    {
                        z = y;
                    }
                    string code = responseFromServer.Substring(i, z + 1);
                    GuessX(code);
                }
            }
            catch (Exception q) { }
        }

        public static void Macro()
        {
            while (true)
            {
                Dig();
                Guess();
                PokeCheck();
                Thread.Sleep(10000);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pokeballs == 0)
                this.Text = "No Pokeballs!";
            if (pokeballs == 1)
            {
                this.Text = "Pokeballs!";
                //Console.Beep();
            }
            if (pokeballs == 2)
                this.Text = "Gotta Wait!";
        }

        public static void PokeCheck()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.pokeplushies.com/pokemon_center.php");
                request.CookieContainer = ccx;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.15) Gecko/20101026 Firefox/3.5.15";
                //request.Proxy = new WebProxy("83.219.90.23", 80);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                if (responseFromServer.Contains("There are no pokeballs here"))
                {
                    pokeballs = 0;
                }
                else if (responseFromServer.Contains("Would you like to get right back"))
                {
                    pokeballs = 2;
                }
                else
                {
                    pokeballs = 1;
                }
            }
            catch (Exception q) { }
        }

        private void Question_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Bitmap b = new Bitmap(Question.Image);
            if (Compare(b).Length < 1)
            {
                int btn = answers[autoindex];

                autom = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                if (btn == 1)
                {
                    button1_Click(null, null);
                }
                else if (btn == 2)
                {
                    button2_Click(null, null);
                }
                else if (btn == 3)
                {
                    button3_Click(null, null);
                }
                else
                {
                    button4_Click(null, null);
                }
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Restart();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Login();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if(bdeath == 1)
            {
                button1_Click(null,null);
            }
            else if (bdeath == 2)
            {
                button2_Click(null, null);
            }
            else if (bdeath == 3)
            {
                button3_Click(null, null);
            }
            else if (bdeath == 4)
            {
                button4_Click(null, null);
            }
            else if (bdeath == 5)
            {
                GoBackX();
            }
            else if (bdeath == 6)
            {
                GoBack(gbdeath);
            }
            else if (bdeath == 7)
            {
                Invest1();
            }
            else if (bdeath == 8)
            {
                Invest2(form);
            }
            else if (bdeath == 9)
            {
                Set();
            }
            else if (bdeath == 10)
            {
                Sell();
            }
            timer3.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                this.TopMost = true;
            if (!checkBox1.Checked)
                this.TopMost = false;
        }

        private void Form1_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            thr.Abort();
            Application.Exit();
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
