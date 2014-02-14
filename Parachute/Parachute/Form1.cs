using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Parachute
{
    public partial class Form1 : Form
    {
        private int last_tick = 0;
        private Panel[] dots;
        private bool[] used;
        private Random r = new Random();
        private bool cleared = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Set everything
            dots = new Panel[4];
            used = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                Panel temp = new Panel();
                temp.Name = "Dot" + i;
                temp.Size = new Size(2, 2);
                temp.Location = new Point(-1, -1);
                temp.BackColor = Color.Black;
                temp.Visible = true;
                this.Background.Controls.Add(temp);
                dots[i] = temp;
            }
        }

        private void ClearDots()
        {
            for (int i = 0; i < 4; i++)
            {
                dots[i].Location = new Point(-1, -1);
                used[i] = false;
            }
            cleared = true;
        }

        //Launch
        private double LaunchAccel()
        {
            return -32.0;
        }

        private double LaunchVel()
        {
            return -32.0 * t + 800.0;
        }

        private double LaunchPos()
        {
            return (-16.0 * t + 800.0) * t;
        }

        //Falling
        private double t = 0.0;
        private double a1 = Math.Sqrt(0.003 / 175.0);
        private double a2 = Math.Sqrt(0.56 / 175.0);
        private double m_k1 = 175.0 / 32.0 / 0.003;
        private double m_k2 = 175.0 / 32.0 / 0.56;
        private const double g = 32.0;
        private double t1 = 0.0;
        private double c = 0.0;
        private double p = 0.0;
        private double t2 = 0.0;
        private double c2 = 0.0;
        private double p2 = 0.0;
        private bool chute = false;
        private bool special = false;

        private double FallAccel()
        {
            if (!chute || state == 3)
                return g - 1 / m_k1 * Math.Pow(FallVel(), 2.0);
            else
                return g - 1 / m_k2 * Math.Pow(FallVel(), 2.0);
        }

        private double FallVel()
        {
            if (!chute)
                return Math.Tanh(a1 * g * (t - 25.0)) / a1;
            else if (state == 2)
            {
                if (!special)
                    return 1 / Math.Tanh(a2 * g * ((t - 25.0) - t1) + c) / a2;
                else
                    return Math.Tanh(a2 * g * ((t - 25.0) - t1) + c) / a2;
            }
            else
            {
                return Math.Tanh(a1 * g * ((t - 25.0) - t2) + c2) / a1;
            }
        }

        private double FallPos()
        {
            if (!chute)
                return m_k1 * Math.Log(Math.Cosh(a1 * g * (t - 25.0)));
            else if (state == 2)
                return m_k2 * Math.Log(Math.Sinh(a2 * g * ((t - 25.0) - t1) + c)) + p;
            else
                return m_k1 * Math.Log(Math.Cosh(a1 * g * ((t - 25.0) - t2) + c2)) + p2;
        }

        private void FindFallConstC()
        {
            if (FallVel() > 1 / a2)
                c = ArcTanh(a1 / a2 / Math.Tanh(a1 * g * t1));
            else if (FallVel() < 1 / a2)
            {
                c = ArcTanh(a2 * Math.Tanh(a1 * g * t1) / a1);
                special = true;
            }
            //else is pretty much impossible
        }

        private void FindFallConstC2()
        {
            c2 = ArcTanh(a1 * 17.7);
        }

        private double ArcTanh(double x)
        {
            return 0.5 * Math.Log((1.0 + x) / (1.0 - x));
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (button.Text == "Launch")
            {
                last_tick = Environment.TickCount;
                state = 0;
                t = 0.0;
                realtime = 0;
                cleared = false;
                chute = false;
                special = false;
                Cannon.BackgroundImage = Parachute.Properties.Resources.flying1;
                Counter.Start();
                button.Text = "Open Parachute";
                button.Enabled = false;
                phase0.ForeColor = Color.White;
            }
            else if (button.Enabled && button.Text == "Open Parachute")
            {
                OpenChute();
                button.Text = "Reset";
                phase1.ForeColor = Color.DimGray;
                phase2.ForeColor = Color.White;
            }
            else if (button.Text == "Reset")
            {
                Counter.Stop();
                ClearDots();
                Time.Text = "";
                Alt.Text = "";
                Vel.Text = "";
                Acc.Text = "";
                LTime.Text = "";
                FAlt.Text = "";
                FTime.Text = "";
                PAlt.Text = "";
                PTime.Text = "";
                phase0.ForeColor = Color.DimGray;
                phase1.ForeColor = Color.DimGray;
                phase2.ForeColor = Color.DimGray;
                phase3.ForeColor = Color.DimGray;
                phase4.Text = "";
                button.Text = "Launch";
                Cannon.BackgroundImage = Parachute.Properties.Resources.cannon1;
                Cannon.Location = new Point(0, 500);
                Cannon.Size = new Size(50, 50);
                Hole.Location = new Point(300, 500);
                Background.Location = new Point(0, -100);
            }
        }

        private int state = 0;
        private int realtime = 0;

        private void Counter_Tick(object sender, EventArgs e)
        {
            int temp_tick = Environment.TickCount;
            realtime += GetSpeed() * (temp_tick - last_tick);
            if (state == 0)
            {
                if (realtime >= 25000)
                {
                    realtime = 25000;
                    button.Enabled = true;
                    state = 1;
                    phase0.ForeColor = Color.DimGray;
                    phase1.ForeColor = Color.White;
                    Cannon.BackgroundImage = Parachute.Properties.Resources.flying2;
                }
                t = realtime / 1000.0;
                Time.Text = t.ToString("0.00");
                Alt.Text = LaunchPos().ToString("0.0");
                Vel.Text = LaunchVel().ToString("0.0");
                Acc.Text = LaunchAccel().ToString("0.0");
                LTime.Text = t.ToString("0.00");
                FAlt.Text = LaunchPos().ToString("0.0");
            }
            else if (state == 1)
            {
                t = realtime / 1000.0;
                Time.Text = t.ToString("0.00");
                Alt.Text = (10000.0 - FallPos()).ToString("0.0");
                Vel.Text = (-1 * FallVel()).ToString("0.0");
                Acc.Text = (-1 * FallAccel()).ToString("0.0");
                FTime.Text = (t - 25.0).ToString("0.00");
                PAlt.Text = (10000 - FallPos()).ToString("0.0");
                if (double.Parse(Alt.Text) <= 0.0)
                {
                    Alt.Text = "0.0";
                    PAlt.Text = "0.0";
                    phase1.ForeColor = Color.DimGray;
                    phase4.ForeColor = Color.Red;
                    Cannon.BackgroundImage = Parachute.Properties.Resources.dead1;
                    phase4.Text = "CRASHED";
                    button.Text = "Reset";
                    ClearDots();
                    Counter.Stop();
                }
            }
            else if (state == 2)
            {
                t = realtime / 1000.0;
                Time.Text = t.ToString("0.00");
                Alt.Text = (10000.0 - FallPos()).ToString("0.0");
                Vel.Text = (-1 * FallVel()).ToString("0.0");
                Acc.Text = (-1 * FallAccel()).ToString("0.0");
                PTime.Text = (t - 25.0 - t1).ToString("0.00");
                if (t > 145.0 && !(t1 >= 40.0 && t1 <= 41.0))
                {
                    t2 = t - 25.0;
                    FindFallConstC2();
                    p2 = FallPos();
                    Cannon.BackgroundImage = Parachute.Properties.Resources.falling1;
                    Cannon.Size = new Size(50, 50);
                    state = 3;
                    phase2.ForeColor = Color.DimGray;
                    phase3.ForeColor = Color.DimGray;
                    phase4.ForeColor = Color.Red;
                    phase4.Text = "SHOT";
                }
                else if (double.Parse(Alt.Text) < 250.0)
                {
                    phase2.ForeColor = Color.DimGray;
                    phase3.ForeColor = Color.White;
                }
                if (double.Parse(Alt.Text) <= 0.0)
                {
                    Alt.Text = "0.0";
                    PAlt.Text = "0.0";
                    phase3.ForeColor = Color.DimGray;
                    if (t1 >= 40.0 && t1 <= 41.0)
                    {
                        Cannon.BackgroundImage = Parachute.Properties.Resources.land1;
                        Cannon.Size = new Size(50, 50);
                        phase4.ForeColor = Color.Green;
                        phase4.Text = "SUCCESS";
                        state = 4;
                    }
                    else
                    {
                        Cannon.BackgroundImage = Parachute.Properties.Resources.crash1;
                        Cannon.Size = new Size(50, 50);
                        phase4.ForeColor = Color.Orange;
                        phase4.Text = "CRASHED";
                        state = 4;
                    }
                    ClearDots();
                    Counter.Stop();
                }
            }
            else if (state == 3)
            {
                t = realtime / 1000.0;
                Time.Text = t.ToString("0.00");
                Alt.Text = (10000.0 - FallPos()).ToString("0.0");
                Vel.Text = (-1 * FallVel()).ToString("0.0");
                Acc.Text = (-1 * FallAccel()).ToString("0.0");
                PTime.Text = (t - 25.0 - t1).ToString("0.00");
                if (double.Parse(Alt.Text) <= 0.0)
                {
                    Alt.Text = "0.0";
                    PAlt.Text = "0.0";
                    Cannon.BackgroundImage = Parachute.Properties.Resources.dead1;
                    phase4.Text = "DEAD";
                    ClearDots();
                    Counter.Stop();
                }
            }
            double pos = double.Parse(Alt.Text);
            double dist = t * 80.0;
            if (dist > 11600.0)
                dist = 11600.0;
            Point xy = new Point();
            Point sxy = new Point();
            Point hxy = new Point();
            hxy.X = 300;
            hxy.Y = 500;
            if (dist <= 12.5)
            {
                xy.X = (int)(dist * 10.0);
            }
            else if (dist >= 12.5 && dist <= 11587.5)
            {
                xy.X = 125;
            }
            else if (dist >= 11587.5)
            {//11600 = 250 // 11587.5 = 375
                xy.X = 125;
                hxy.X = (int)((11600.0 - dist) * 10.0 + 250.0);
            }
            if (state == 0)
            {
                if (pos <= 25.0)
                {
                    xy.Y = (int)(500.0 - pos * 10.0);
                }
                else if (pos >= 25.0 && pos <= 9985.0)
                {
                    double shift = (pos - 25.0) * 10.0;
                    if (shift > 100.0)
                        shift = 100.0;
                    sxy.Y = (int)(shift - 100.0);
                    xy.Y = 150;
                }
                else if (pos >= 9985.0)
                {
                    xy.Y = (int)(150.0 - (pos - 9985.0) * 10);
                }
            }
            else if (state > 0)
            {
                if (pos >= 9975.0)
                {
                    xy.Y = (int)(250.0 - (pos - 9975.0) * 10);
                }
                else if (pos >= 15.0 && pos <= 9975.0)
                {
                    double shift = (pos - 15.0) * 10.0;
                    if (shift > 100.0)
                        shift = 100.0;
                    sxy.Y = (int)(shift - 100.0);
                    xy.Y = 250;
                }
                else if (pos <= 15.0)
                {
                    sxy.Y = -100;
                    xy.Y = (int)(500.0 - pos * 10.0);
                }
            }
            if (sxy.Y < 0 && pos <= 20.0)
            {
                hxy.Y = 500;
            }
            if (state == 2)
            {
                xy.X -= 25;
                xy.Y -= 50;
            }
            Cannon.Location = xy;
            Background.Location = sxy;
            Hole.Location = hxy;
            if (!cleared)
            {
                double dx = -10.0 * GetSpeed();
                double dy = 0.0;
                if (state == 0)
                    dy = LaunchVel() / 4.0 * GetSpeed();
                else
                    dy = -1 * FallVel() / 4.0 * GetSpeed();
                for (int i = 0; i < 4; i++)
                {
                    if (!used[i])
                    {
                        dots[i].Location = new Point(r.Next(300), r.Next(500));
                        used[i] = true;
                    }
                    else
                    {
                        dots[i].Location = new Point((int)(dots[i].Location.X + dx), (int)(dots[i].Location.Y + dy));
                        if (dots[i].Location.X < 0 || dots[i].Location.X > 300 || dots[i].Location.Y < 0 || dots[i].Location.Y > 500)
                            used[i] = false;
                    }
                }
            }
            last_tick = temp_tick;
        }

        private void OpenChute()
        {
            Counter.Stop();
            int temp_tick = Environment.TickCount;
            realtime += GetSpeed() * (temp_tick - last_tick);
            t = realtime / 1000.0;
            t1 = t - 25.0;
            last_tick = temp_tick;
            FindFallConstC();
            p = FallPos();
            Cannon.BackgroundImage = Parachute.Properties.Resources.parachute1;
            Cannon.Size = new Size(100, 100);
            Cannon.Location = new Point(Cannon.Location.X - 25, Cannon.Location.Y - 50);
            chute = true;
            state = 2;
            Counter.Start();
        }

        private int GetSpeed()
        {
            if (S0.Checked)
                return 1;
            if (S1.Checked)
                return 2;
            return 4;
        }
    }
}
