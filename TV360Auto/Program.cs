using LDAutoHelper.LDConsole;
using LDAutoHelper;
using TV360Auto.Services;

namespace TV360Auto
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CommonHelper.LDConsolePath = @"C:\LDPlayer\LDPlayer9\ldconsole.exe";
            var devicesLD = LDHelper.GetDevices2();
            LDService.StartAuto(devicesLD[0], CancellationToken.None);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}