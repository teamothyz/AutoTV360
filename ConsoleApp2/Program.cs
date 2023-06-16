using LDAutoHelper;
using LDAutoHelper.ADB;
using LDAutoHelper.LDConsole;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommonHelper.LDConsolePath = @"C:\LDPlayer\LDPlayer9\ldconsole.exe";
            var devicesLD = LDHelper.GetDevices2();
            ADBHelper.Tap(devicesLD[0], 50, 50);
        }
    }
}
