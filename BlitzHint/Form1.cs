using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BlitzHint
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        [DllImport("user32.dll")]
        static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hdc, int x, int y);
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private int X = 0;
        private int Y = 0;
        private IntPtr dc = IntPtr.Zero;
        //1 = White; 2 = Purple; 3 = Yellow; 4 = Blue; 5 = Green; 6 = Orange; 7 = Red;
        private int[,] table = new int[8, 8];//[X, Y]
        private int[,] moves = new int[8, 8];//[X, Y] Move | 1 = Left; 2 = Up; 3 = Right; 4 = Down
        private int[,] used = new int[8, 8];//[X, Y] 0 = Unused; 1 = Used
        private int hyx = 0;
        private int hyy = 0;
        private bool hyper = false;
        //White RGB 250-255 Original color ranges!
        //Purple +8, +33 Red 140-15 Green 0-15 Blue 145-160
        //Yellow +13, +28 Red 240-255 Green 245-255 Blue 30-70
        //Blue +10, +5 Red 32-64 Green 228-255 Blue 250-255
        //Green +9, +32 Red 0-5 Green 235-245 Blue 50-55
        //Orange +10 +31 Red 250-255 Green 80-85 Blue 0-5
        //Red +8, +32 Red 210-230 Green 15-20 Blue 30-50

        public void DoMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        public void Drag(int xx1, int yy1, int xx2, int yy2)
        {
            Cursor.Position = new Point(xx1, yy1);
            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            Cursor.Position = new Point(xx2, yy2);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        public Point Field()
        {
            int px = Cursor.Position.X;
            int py = Cursor.Position.Y;
            int pz = 0;
            int pw = 0;
            dc = GetDC(IntPtr.Zero);
            for (int z = 0; Color.FromArgb((int)GetPixel(dc, px + z, py)).Name == "ffffff"; z++)
            {
                pz = z;
                if (z > 3000)
                    break;
            }
            if (pz > 3000)
                Application.Restart();
            for (int z = 0; Color.FromArgb((int)GetPixel(dc, px + pz + 169, py - z)).Name != "a2146"; z++)
            {
                pw = z;
                if (z > 3000)
                    break;
            }
            if (pw > 3000)
                Application.Restart();
            uint a = GetPixel(dc, px + pz + 169 + 19, py - pw - 1 + 19);
            Color color = Color.FromArgb((int)a);
            return new Point(px + pz + 169, py - pw - 1);
        }

        public void Process()
        {
            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 8; b++)
                {
                    //Stars that work: (Increase Ranges later)
                    //Purple, Blue, Green, White, Yellow, Orange, 
                    Color star1 = Color.FromArgb((int)GetPixel(dc, X + 19 + a * 40, Y + 19 + b * 40));
                    Color star2 = Color.FromArgb((int)GetPixel(dc, X + 8 + a * 40, Y + 33 + b * 40));
                    Color star3 = Color.FromArgb((int)GetPixel(dc, X + 13 + a * 40, Y + 28 + b * 40));
                    Color star4 = Color.FromArgb((int)GetPixel(dc, X + 10 + a * 40, Y + 5 + b * 40));
                    Color star5 = Color.FromArgb((int)GetPixel(dc, X + 9 + a * 40, Y + 32 + b * 40));
                    Color star6 = Color.FromArgb((int)GetPixel(dc, X + 10 + a * 40, Y + 31 + b * 40));
                    Color star7 = Color.FromArgb((int)GetPixel(dc, X + 8 + a * 40, Y + 32 + b * 40));
                    Color star8 = Color.FromArgb((int)GetPixel(dc, X + 20 + a * 40, Y + 39 + b * 40));
                    string norm = Color.FromArgb((int)GetPixel(dc, X + 19 + a * 40, Y + 19 + b * 40)).Name;
                    string multi = Color.FromArgb((int)GetPixel(dc, X + 19 + a * 40, Y + 4 + b * 40)).Name;
                    //White Star is White... So yea..
                    if (norm == "fefefe")
                    {//White Normal
                        table[a, b] = 1; 
                    }
                    else if (multi == "ffffff")
                    {//White Multi FIXED
                        table[a, b] = 1;
                    }
                    else if (norm == "ffffff")
                    {//White Flame
                        table[a, b] = 1;
                    }
                    else if (norm == "f610f6")
                    {//Purple Normal
                        table[a, b] = 2;
                    }
                    else if (multi == "ff6eff")
                    {//Purple Multi FIXED
                        table[a, b] = 2;
                    }
                    else if (norm == "f613f6")
                    {//Purple Flame
                        table[a, b] = 2;
                    }
                    else if (norm == "25fbfe")
                    {//Yellow Normal
                        table[a, b] = 3;
                    }
                    else if (norm == "218fb2")
                    {//Yellow Coin
                        table[a, b] = 3;
                    }
                    else if (multi == "6afeff")
                    {//Yellow Multi FIXED
                        table[a, b] = 3;
                    }
                    else if (norm == "27fcff")
                    {//Yellow Flame
                        table[a, b] = 3;
                    }
                    else if (norm == "fc8612")
                    {//Blue Normal
                        table[a, b] = 4;
                    }
                    else if (multi == "ffb523")
                    {//Blue Multi FIXED
                        table[a, b] = 4;
                    }
                    else if (norm == "fd8715")
                    {//Blue Flame
                        table[a, b] = 4;
                    }
                    else if (norm == "88fe54")
                    {//Green Normal
                        table[a, b] = 5;
                    }
                    else if (multi == "1ff518")
                    {//Green Multi FIXED
                        table[a, b] = 5;
                    }
                    else if (norm == "86fe55")
                    {//Green Flame
                        table[a, b] = 5;
                    }
                    else if (norm == "79fbfe")
                    {//Orange Normal
                        table[a, b] = 6;
                    }
                    else if (multi == "4bbcff")
                    {//Orange Flame
                        table[a, b] = 6;
                    }
                    else if (norm == "78f8ff")
                    {//Orange Multi FIXED
                        table[a, b] = 6;
                    }
                    else if (norm == "391cfe")
                    {//Red Normal
                        table[a, b] = 7;
                    }
                    else if (multi == "3a3aff")
                    {//Red Multi FIXED
                        table[a, b] = 7;
                    }
                    else if (norm == "3b1fff")
                    {//Red Flame
                        table[a, b] = 7;
                    }
                    else if (star2.R >= 145 && star2.R <= 160 && star2.G >= 0 && star2.G <= 15 && star2.B >= 140 && star2.B <= 150)
                    {
                        table[a, b] = 2;
                    }
                    else if (star3.R >= 20 && star3.R <= 70 && star3.G >= 230 && star3.G <= 255 && star3.B >= 230 && star3.B <= 255)
                    {
                        table[a, b] = 3;
                    }
                    else if (star4.R >= 220 && star4.R <= 255 && star4.G >= 220 && star4.G <= 255 && star4.B >= 10 && star4.B <= 70)
                    {
                        table[a, b] = 4;
                    }
                    else if (star5.R >= 50 && star5.R <= 55 && star5.G >= 235 && star5.G <= 245 && star5.B >= 0 && star5.B <= 5)
                    {
                        table[a, b] = 5;
                    }
                    else if (star6.R >= 0 && star6.R <= 5 && star6.G >= 80 && star6.G <= 85 && star6.B >= 250 && star6.B <= 255)
                    {
                        table[a, b] = 6;
                    }
                    else if (star7.R >= 20 && star7.R <= 60 && star7.G >= 0 && star7.G <= 30 && star7.B >= 200 && star7.B <= 255)
                    {
                        table[a, b] = 7;
                    }
                    else if (star1.R >= 250 && star1.R <= 255 && star1.G >= 250 && star1.G <= 255 && star1.B >= 250 && star1.B <= 255)
                    {
                        table[a, b] = 1;
                    }
                    else if (star8.R >= 160 && star8.R <= 200 && star8.G >= 190 && star8.G <= 230 && star8.B >= 210 && star8.B <= 250)
                    {
                        hyper = true;
                        hyx = a;
                        hyy = b;
                        table[a, b] = 8;
                    }
                }
            }
        }

        public bool Stars()
        {
            bool k = false;
            //Flames
            for (int b = 0; b < 8; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if (table[a, b] != 0 && used[a, b] == 0)
                    {
                        //Sequence 1
                        if (a + 3 < 8 && b - 1 > -1 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b - 1] && table[a, b] == table[a + 2, b] && table[a, b] == table[a + 3, b] && table[a, b] == table[a + 1, b + 1])
                            {
                                if (used[a + 1, b - 1] == 0 && used[a + 2, b] == 0 && used[a + 3, b] == 0 && used[a + 1, b] == 0 && used[a + 1, b + 1] == 0)
                                {
                                    moves[a, b] = 3;
                                    used[a, b] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 2
                        if (a + 3 < 8 && b - 1 > -1 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 2, b - 1] && table[a, b] == table[a + 1, b] && table[a, b] == table[a + 3, b] && table[a, b] == table[a + 2, b + 1])
                            {
                                if (used[a + 2, b - 1] == 0 && used[a + 1, b] == 0 && used[a + 3, b] == 0 && used[a + 2, b] == 0 && used[a + 2, b + 1] == 0)
                                {
                                    moves[a + 3, b] = 1;
                                    used[a, b] = 1;
                                    used[a + 2, b - 1] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 2, b + 1] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 3
                        if (b + 3 < 8 && a - 1 > -1 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a - 1, b + 1] && table[a, b] == table[a, b + 2] && table[a, b] == table[a, b + 3] && table[a, b] == table[a + 1, b + 1])
                            {
                                if (used[a - 1, b + 1] == 0 && used[a, b + 2] == 0 && used[a, b + 3] == 0 && used[a, b + 1] == 0 & used[a + 1, b + 1] == 0)
                                {
                                    moves[a, b] = 4;
                                    used[a, b] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a, b + 2] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 1] = 1;
                                    used[a + 1, b + 1] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 4
                        if (b + 3 < 8 && a - 1 > -1 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a - 1, b + 2] && table[a, b] == table[a, b + 1] && table[a, b] == table[a, b + 3] && table[a, b] == table[a + 1, b + 2])
                            {
                                if (used[a - 1, b + 2] == 0 && used[a, b + 1] == 0 && used[a, b + 3] == 0 && used[a, b + 2] == 0 && used[a + 1, b + 2] == 0)
                                {
                                    moves[a, b + 3] = 2;
                                    used[a, b] = 1;
                                    used[a - 1, b + 2] = 1;
                                    used[a, b + 1] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 2] = 1;
                                    used[a + 1, b + 2] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 5
                        if (a + 1 < 8 && b + 2 < 8 && a - 2 > -1)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a - 1, b + 1] && table[a, b] == table[a - 1, b + 2] && table[a, b] == table[a - 2, b])
                            {
                                if (used[a + 1, b] == 0 && used[a - 1, b + 1] == 0 && used[a - 1, b + 2] == 0 && used[a - 2, b] == 0 && used[a - 1, b] == 0)
                                {
                                    moves[a - 2, b] = 3;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a - 1, b + 2] = 1;
                                    used[a - 2, b] = 1;
                                    used[a - 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 6
                        if (a + 1 < 8 && b + 2 < 8 && b - 1 > -1 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a - 1, b + 1] && table[a, b] == table[a - 1, b + 2] && table[a, b] == table[a - 1, b - 1])
                            {
                                if (used[a + 1, b] == 0 && used[a - 1, b + 1] == 0 && used[a - 1, b + 2] == 0 && used[a - 1, b - 1] == 0 && used[a - 1, b] == 0)
                                {
                                    moves[a - 1, b - 1] = 4;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a - 1, b + 2] = 1;
                                    used[a - 1, b - 1] = 1;
                                    used[a - 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 7
                        if (a - 1 > -1 && b + 2 < 8 && a + 2 < 8)
                        {
                            if (table[a, b] == table[a - 1, b] && table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a + 1, b + 2] && table[a, b] == table[a + 2, b])
                            {
                                if (used[a - 1, b] == 0 && used[a + 1, b + 1] == 0 && used[a + 1, b + 2] == 0 && used[a + 2, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 2, b] = 1;
                                    used[a, b] = 1;
                                    used[a - 1, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 1, b + 2] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 8
                        if (a - 1 > -1 && b + 2 < 8 && b - 1 > -1 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a - 1, b] && table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a + 1, b + 2] && table[a, b] == table[a + 1, b - 1])
                            {
                                if (used[a - 1, b] == 0 && used[a + 1, b + 1] == 0 && used[a + 1, b + 2] == 0 && used[a + 1, b - 1] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 1, b - 1] = 4;
                                    used[a, b] = 1;
                                    used[a - 1, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 1, b + 2] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 9
                        if (a - 1 > -1 && b - 2 > -1 && a + 2 < 8)
                        {
                            if (table[a, b] == table[a - 1, b] && table[a, b] == table[a + 1, b - 1] && table[a, b] == table[a + 1, b - 2] && table[a, b] == table[a + 2, b])
                            {
                                if (used[a - 1, b] == 0 && used[a + 1, b - 1] == 0 && used[a + 1, b - 2] == 0 && used[a + 2, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 2, b] = 1;
                                    used[a, b] = 1;
                                    used[a - 1, b] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 1, b - 2] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 10
                        if (a - 1 > -1 && b - 2 > -1 && b + 1 < 8 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a - 1, b] && table[a, b] == table[a + 1, b - 1] && table[a, b] == table[a + 1, b - 2] && table[a, b] == table[a + 1, b + 1])
                            {
                                if (used[a - 1, b] == 0 && used[a + 1, b - 1] == 0 && used[a + 1, b - 2] == 0 && used[a + 1, b + 1] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 1, b + 1] = 2;
                                    used[a, b] = 1;
                                    used[a - 1, b] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 1, b - 2] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 11
                        if (a + 1 < 8 && b - 2 > -1 && a - 2 > -1)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a - 1, b - 1] && table[a, b] == table[a - 1, b - 2] && table[a, b] == table[a - 2, b])
                            {
                                if (used[a + 1, b] == 0 && used[a - 1, b - 1] == 0 && used[a - 1, b - 2] == 0 && used[a - 2, b] == 0 && used[a - 1, b] == 0)
                                {
                                    moves[a - 2, b] = 3;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a - 1, b - 1] = 1;
                                    used[a - 1, b - 2] = 1;
                                    used[a - 2, b] = 1;
                                    used[a - 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 12
                        if (a + 1 < 8 && b - 2 > -1 && b + 1 < 8 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a - 1, b - 1] && table[a, b] == table[a - 1, b - 2] && table[a, b] == table[a - 1, b + 1])
                            {
                                if (used[a + 1, b] == 0 && used[a - 1, b - 1] == 0 && used[a - 1, b - 2] == 0 && used[a - 1, b + 1] == 0 && used[a - 1, b] == 0)
                                {
                                    moves[a - 1, b + 1] = 2;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a - 1, b - 1] = 1;
                                    used[a - 1, b - 2] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a - 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            return k;
        }

        public bool Flames()
        {
            bool k = false;
            //Flames
            for (int b = 0; b < 8; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if (table[a, b] != 0 && used[a, b] == 0)
                    {
                        //Sequence 1
                        if (a + 3 < 8 && b - 1 > -1)
                        {
                            if (table[a, b] == table[a + 1, b - 1] && table[a, b] == table[a + 2, b] && table[a, b] == table[a + 3, b])
                            {
                                if (used[a + 1, b - 1] == 0 && used[a + 2, b] == 0 && used[a + 3, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 1, b - 1] = 4;
                                    used[a, b] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 2
                        if (a + 3 < 8 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a + 2, b] && table[a, b] == table[a + 3, b])
                            {
                                if (used[a + 1, b + 1] == 0 && used[a + 2, b] == 0 && used[a + 3, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 1, b + 1] = 2;
                                    used[a, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 1, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 3
                        if (a + 3 < 8 && b - 1 > -1)
                        {
                            if (table[a, b] == table[a + 2, b - 1] && table[a, b] == table[a + 1, b] && table[a, b] == table[a + 3, b])
                            {
                                if (used[a + 2, b - 1] == 0 && used[a + 1, b] == 0 && used[a + 3, b] == 0 && used[a + 2, b] == 0)
                                {
                                    moves[a + 2, b - 1] = 4;
                                    used[a, b] = 1;
                                    used[a + 2, b - 1] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 2, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 4
                        if (a + 3 < 8 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 2, b + 1] && table[a, b] == table[a + 1, b] && table[a, b] == table[a + 3, b])
                            {
                                if (used[a + 2, b + 1] == 0 && used[a + 1, b] == 0 && used[a + 3, b] == 0 && used[a + 2, b] == 0)
                                {
                                    moves[a + 2, b + 1] = 2;
                                    used[a, b] = 1;
                                    used[a + 2, b + 1] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 2, b] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 5
                        if (b + 3 < 8 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a - 1, b + 1] && table[a, b] == table[a, b + 2] && table[a, b] == table[a, b + 3])
                            {
                                if (used[a - 1, b + 1] == 0 && used[a, b + 2] == 0 && used[a, b + 3] == 0 && used[a, b + 1] == 0)
                                {
                                    moves[a - 1, b + 1] = 3;
                                    used[a, b] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a, b + 2] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 1] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 6
                        if (b + 3 < 8 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a, b + 2] && table[a, b] == table[a, b + 3])
                            {
                                if (used[a + 1, b + 1] == 0 && used[a, b + 2] == 0 && used[a, b + 3] == 0 && used[a, b + 1] == 0)
                                {
                                    moves[a + 1, b + 1] = 1;
                                    used[a, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a, b + 2] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 1] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 7
                        if (b + 3 < 8 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a - 1, b + 2] && table[a, b] == table[a, b + 1] && table[a, b] == table[a, b + 3])
                            {
                                if (used[a - 1, b + 2] == 0 && used[a, b + 1] == 0 && used[a, b + 3] == 0 && used[a, b + 2] == 0)
                                {
                                    moves[a - 1, b + 2] = 3;
                                    used[a, b] = 1;
                                    used[a - 1, b + 2] = 1;
                                    used[a, b + 1] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 2] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                        //Sequence 8
                        if (b + 3 < 8 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 2] && table[a, b] == table[a, b + 1] && table[a, b] == table[a, b + 3])
                            {
                                if (used[a + 1, b + 2] == 0 && used[a, b + 1] == 0 && used[a, b + 3] == 0 && used[a, b + 2] == 0)
                                {
                                    moves[a + 1, b + 2] = 1;
                                    used[a, b] = 1;
                                    used[a + 1, b + 2] = 1;
                                    used[a, b + 1] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 2] = 1;
                                    k = true;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            return k;
        }

        public void Normals()
        {
            //Normal
            for (int b = 0; b < 8; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if (table[a, b] != 0 && used[a, b] == 0)
                    {
                        //Sequence 1
                        if (a + 2 < 8 && b - 1 > -1)
                        {
                            if (table[a, b] == table[a + 1, b - 1] && table[a, b] == table[a + 2, b])
                            {
                                if (used[a + 1, b - 1] == 0 && used[a + 2, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 1, b - 1] = 4;
                                    used[a, b] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 1, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 2
                        if (a + 2 < 8 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a + 2, b])
                            {
                                if (used[a + 1, b + 1] == 0 && used[a + 2, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a + 1, b + 1] = 2;
                                    used[a, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 1, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 3
                        if (a + 2 < 8 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a + 2, b + 1])
                            {
                                if (used[a + 1, b + 1] == 0 && used[a + 2, b + 1] == 0 && used[a, b + 1] == 0)
                                {
                                    moves[a, b] = 4;
                                    used[a, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 2, b + 1] = 1;
                                    used[a, b + 1] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 4
                        if (a + 2 < 8 && b - 1 > -1)
                        {
                            if (table[a, b] == table[a + 1, b - 1] && table[a, b] == table[a + 2, b - 1])
                            {
                                if (used[a + 1, b - 1] == 0 && used[a + 2, b - 1] == 0 && used[a, b - 1] == 0)
                                {
                                    moves[a, b] = 2;
                                    used[a, b] = 1;
                                    used[a + 1, b - 1] = 1;
                                    used[a + 2, b - 1] = 1;
                                    used[a, b - 1] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 5
                        if (a + 2 < 8 && b - 1 > -1)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a + 2, b - 1])
                            {
                                if (used[a + 1, b] == 0 && used[a + 2, b - 1] == 0 && used[a + 2, b] == 0)
                                {
                                    moves[a + 2, b - 1] = 4;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 2, b - 1] = 1;
                                    used[a + 2, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 6
                        if (a + 2 < 8 && b + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a + 2, b + 1])
                            {
                                if (used[a + 1, b] == 0 && used[a + 2, b + 1] == 0 && used[a + 2, b] == 0)
                                {
                                    moves[a + 2, b + 1] = 2;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 2, b + 1] = 1;
                                    used[a + 2, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 7
                        if (a + 3 < 8)
                        {
                            if (table[a, b] == table[a + 1, b] && table[a, b] == table[a + 3, b])
                            {
                                if (used[a + 1, b] == 0 && used[a + 3, b] == 0 && used[a + 2, b] == 0)
                                {
                                    moves[a + 3, b] = 1;
                                    used[a, b] = 1;
                                    used[a + 1, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 2, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 8
                        if (a + 3 < 8)
                        {
                            if (table[a, b] == table[a + 2, b] && table[a, b] == table[a + 3, b])
                            {
                                if (used[a + 2, b] == 0 && used[a + 3, b] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a, b] = 3;
                                    used[a, b] = 1;
                                    used[a + 2, b] = 1;
                                    used[a + 3, b] = 1;
                                    used[a + 1, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 9
                        if (b + 2 < 8 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a - 1, b + 1] && table[a, b] == table[a, b + 2])
                            {
                                if (used[a - 1, b + 1] == 0 && used[a, b + 2] == 0 && used[a, b + 1] == 0)
                                {
                                    moves[a - 1, b + 1] = 3;
                                    used[a, b] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a, b + 2] = 1;
                                    used[a, b + 1] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 10
                        if (b + 2 < 8 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a, b + 2])
                            {
                                if (used[a + 1, b + 1] == 0 && used[a, b + 2] == 0 && used[a, b + 1] == 0)
                                {
                                    moves[a + 1, b + 1] = 1;
                                    used[a, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a, b + 2] = 1;
                                    used[a, b + 1] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 11
                        if (b + 2 < 8 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a + 1, b + 1] && table[a, b] == table[a + 1, b + 2])
                            {
                                if (used[a + 1, b + 1] == 0 && used[a + 1, b + 2] == 0 && used[a + 1, b] == 0)
                                {
                                    moves[a, b] = 3;
                                    used[a, b] = 1;
                                    used[a + 1, b + 1] = 1;
                                    used[a + 1, b + 2] = 1;
                                    used[a + 1, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 12
                        if (b + 2 < 8 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a - 1, b + 1] && table[a, b] == table[a - 1, b + 2])
                            {
                                if (used[a - 1, b + 1] == 0 && used[a - 1, b + 2] == 0 && used[a - 1, b] == 0)
                                {
                                    moves[a, b] = 1;
                                    used[a, b] = 1;
                                    used[a - 1, b + 1] = 1;
                                    used[a - 1, b + 2] = 1;
                                    used[a - 1, b] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 13
                        if (b + 2 < 8 && a - 1 > -1)
                        {
                            if (table[a, b] == table[a, b + 1] && table[a, b] == table[a - 1, b + 2])
                            {
                                if (used[a, b + 1] == 0 && used[a - 1, b + 2] == 0 && used[a, b + 2] == 0)
                                {
                                    moves[a - 1, b + 2] = 3;
                                    used[a, b] = 1;
                                    used[a, b + 1] = 1;
                                    used[a - 1, b + 2] = 1;
                                    used[a, b + 2] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 14
                        if (b + 2 < 8 && a + 1 < 8)
                        {
                            if (table[a, b] == table[a, b + 1] && table[a, b] == table[a + 1, b + 2])
                            {
                                if (used[a, b + 1] == 0 && used[a + 1, b + 2] == 0 && used[a, b + 2] == 0)
                                {
                                    moves[a + 1, b + 2] = 1;
                                    used[a, b] = 1;
                                    used[a, b + 1] = 1;
                                    used[a + 1, b + 2] = 1;
                                    used[a, b + 2] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 15
                        if (b + 3 < 8)
                        {
                            if (table[a, b] == table[a, b + 1] && table[a, b] == table[a, b + 3])
                            {
                                if (used[a, b + 1] == 0 && used[a, b + 3] == 0 && used[a, b + 2] == 0)
                                {
                                    moves[a, b + 3] = 2;
                                    used[a, b] = 1;
                                    used[a, b + 1] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 2] = 1;
                                    continue;
                                }
                            }
                        }
                        //Sequence 16
                        if (b + 3 < 8)
                        {
                            if (table[a, b] == table[a, b + 2] && table[a, b] == table[a, b + 3])
                            {
                                if (used[a, b + 2] == 0 && used[a, b + 3] == 0 && used[a, b + 1] == 0)
                                {
                                    moves[a, b] = 4;
                                    used[a, b] = 1;
                                    used[a, b + 2] = 1;
                                    used[a, b + 3] = 1;
                                    used[a, b + 1] = 1;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Bejewel()
        {
            for (int b = 0; b < 8; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if (moves[a, b] != 0)
                    {
                        if (moves[a, b] == 1)
                        {
                            Drag(X + a * 40 + 19, Y + b * 40 + 19, X + a * 40 + 19 - 40, Y + b * 40 + 19);
                        }
                        else if (moves[a, b] == 2)
                        {
                            Drag(X + a * 40 + 19, Y + b * 40 + 19, X + a * 40 + 19, Y + b * 40 + 19 - 40);
                        }
                        else if (moves[a, b] == 3)
                        {
                            Drag(X + a * 40 + 19, Y + b * 40 + 19, X + a * 40 + 19 + 40, Y + b * 40 + 19);
                        }
                        else if (moves[a, b] == 4)
                        {
                            Drag(X + a * 40 + 19, Y + b * 40 + 19, X + a * 40 + 19, Y + b * 40 + 19 + 40);
                        }
                    }
                }
            }
        }

        public void Empty()
        {
            table = new int[8, 8];
            moves = new int[8, 8];
            used = new int[8, 8];
        }

        public void Unglitch()
        {
            bool c = false;
            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 8; b++)
                {
                    if (Color.FromArgb((int)GetPixel(dc, X + a * 40, Y + b * 40)).Name == "1d6ae1")
                    {
                        Cursor.Position = new Point(X + 19 + a * 40, Y + 19 + b * 40);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                        c = true;
                        break;
                    }
                    if (c)
                    {
                        break;
                    }
                }
                if (c)
                {
                    break;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process();
            if (hyper)
            {
                Drag(X + hyx * 40 + 19, Y + hyy * 40 + 19, X + hyx * 40 + 19 + 40, Y + hyy * 40 + 19);
                Drag(X + hyx * 40 + 19, Y + hyy * 40 + 19, X + hyx * 40 + 19, Y + hyy * 40 + 19 + 40);
                Drag(X + hyx * 40 + 19, Y + hyy * 40 + 19, X + hyx * 40 + 19 - 40, Y + hyy * 40 + 19);
                Drag(X + hyx * 40 + 19, Y + hyy * 40 + 19, X + hyx * 40 + 19, Y + hyy * 40 + 19 - 40);
                hyper = false;
            }
            while (true)
            {
                if (Stars())
                {
                    Bejewel();
                    Empty();
                    System.Threading.Thread.Sleep(200);
                    Process();
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                if (Flames())
                {
                    Bejewel();
                    Empty();
                    System.Threading.Thread.Sleep(200);
                    Process();
                }
                else
                {
                    break;
                }
            }
            Normals();
            Bejewel();
            Empty();
            Unglitch();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Keys.F12))
            {
                if (timer1.Enabled == false)
                {
                    Point zz = Field();
                    X = zz.X;
                    Y = zz.Y;
                    timer1.Enabled = true;
                    //timer2.Enabled = true;
                }
                else
                {
                    timer1.Enabled = false;
                    //timer2.Enabled = false;
                    ReleaseDC(IntPtr.Zero, dc);
                    Application.Restart();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*if(Color.FromArgb((int)GetPixel(dc, , )).Name == "")
            {

            }*/
        }
    }
}
