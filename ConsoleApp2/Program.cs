using LDAutoHelper;
using LDAutoHelper.ADB;
using LDAutoHelper.LDConsole;
using System.Threading;
using TV360Auto.Services;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommonHelper.LDConsolePath = @"C:\LDPlayer\LDPlayer9\ldconsole.exe";
            var devicesLD = LDHelper.GetDevices2();
            LDService.StartAuto(devicesLD[0], CancellationToken.None);
        }
    }
}
