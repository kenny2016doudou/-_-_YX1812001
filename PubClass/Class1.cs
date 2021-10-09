using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PubClass
{
    public class LoadKeyBoardClass
    {
        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        public static void toLoad()
        {
            //try
            //{
            //    //if (ifLoaded())
            //    //{
            //    //    return;
            //    //}
            //    String file = "C:/Program Files/Common Files/microsoft shared/ink/TabTip.exe";
            //    if (!System.IO.File.Exists(file))
            //    {
            //        file = "C:/WINDOWS/system32/osk.exe";
            //        if (!System.IO.File.Exists(file))
            //        {
            //            file = Application.StartupPath + "\\Keyboard\\TabTip.exe";
            //            if (!System.IO.File.Exists(file))
            //            {
            //                file = Application.StartupPath + "\\Keyboard\\osk.exe";
            //                if (!System.IO.File.Exists(file))
            //                {
            //                    return;
            //                }
            //                Process.Start(file);
            //                return;
            //            }
            //            Process.Start(file);
            //            return;
            //        }
            //        Process.Start(file);
            //        return;
            //    }
            //    Process.Start(file);
            //}
            //catch (Exception)
            //{
            //}
        }

        public static bool ifLoaded()
        {
            bool ifloaded = false;
            IntPtr TouchhWnd = new IntPtr(0);
            TouchhWnd = FindWindow("IPTip_Main_Window", null);
            if (TouchhWnd != IntPtr.Zero)
            {
                ifloaded = true;
                // 获取屏幕尺寸
                //int iActulaWidth = 1024;
                //int iActulaHeight = 768;

                //// 设置软键盘的显示位置，底部居中
                //int posX = (iActulaWidth - 1000) / 2;
                //int posY = (iActulaHeight - 300);

                ////设定键盘显示位置
                //MoveWindow(TouchhWnd, posX, posY, 1000, 300, true);

                //设置软键盘到前端显示
                SetForegroundWindow(TouchhWnd);
            }
            TouchhWnd = new IntPtr(0);
            TouchhWnd = FindWindow("屏幕键盘", null);
            if (TouchhWnd == IntPtr.Zero)
            {
                ifloaded = true;
                // 获取屏幕尺寸

                //int iActulaWidth = 1024;
                //int iActulaHeight = 768;

                //// 设置软键盘的显示位置，底部居中
                //int posX = (iActulaWidth - 1000) / 2;
                //int posY = (iActulaHeight - 300);

                ////设定键盘显示位置
                //MoveWindow(TouchhWnd, posX, posY, 1000, 300, true);

                //设置软键盘到前端显示
                SetForegroundWindow(TouchhWnd);
            }
            return ifloaded;
        }

        public static void unLoad()
        {
            //IntPtr TouchhWnd = new IntPtr(0);
            //TouchhWnd = FindWindow("IPTip_Main_Window", null);
            //if (TouchhWnd != IntPtr.Zero)
            //{
            //    PostMessage(TouchhWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
            //}
            //TouchhWnd = new IntPtr(0);
            //TouchhWnd = FindWindow("屏幕键盘", null);
            //if (TouchhWnd == IntPtr.Zero)
            //{
            //    PostMessage(TouchhWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
            //}
        }
    }
}
