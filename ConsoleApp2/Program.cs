using ADBHelperLib.ADB;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var devices = ADBHelper.GetDevices();
            //var tapRs = ADBHelper.Tap(devices[0], 453, 168);
            //var rs = ADBHelper.Swipe(devices[0], 100, 168, 500, 168, 4);
            ADBHelper.FindImage(devices[0], "D:/vtp.png");
        }
    }
}
