using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;

namespace GoogleHome
{
    public static class MonitorControll
    {
        private static int SC_MONITORPOWER = 0xF170;

        private static uint WM_SYSCOMMAND = 0x0112;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);



      public  enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
        public static void SetMonitorState(MonitorState state)
        {
            Form frm = new Form();

            SendMessage(frm.Handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)state);
            Console.WriteLine("Monitors: " + state);

        }
    }
}