using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;


namespace WinScreenToolkit
{
    class Program
    {
        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc;
            User32.GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            User32.PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            while (true)
            {
                Thread.Sleep(100);

                //if (GetAsyncKeyState(VK_F12))
                //{
                //    break;
                //}

                if (User32.GetAsyncKeyState(User32.VK_LBUTTON) != 0)
                {
                    POINT pt;

                    if (!User32.GetCursorPos(out pt))
                    {
                        break;
                    }

                    var hwndPt = User32.WindowFromPoint(pt);
                    var bmp = PrintWindow(hwndPt);
                    bmp.Save("test.png", ImageFormat.Png);
                }
            }

        }
    }
}
