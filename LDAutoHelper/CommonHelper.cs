using LDAutoHelper.Entity;
using System.Diagnostics;

namespace LDAutoHelper
{
    public class CommonHelper
    {
        public static int GlobalTimeout { get; set; } = 10;
        public static string LDConsolePath { get; set; } = string.Empty;

        public static string? GetResult(string filename, string command)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            var process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
            string? result = null;
            if (process.WaitForExit(GlobalTimeout * 1000))
            {
                result = process.StandardOutput.ReadToEnd();
            }
            process.Close();
            return result;
        }

        public static string? RunLD(string command)
        {
            return GetResult(LDConsolePath, command);
        }

        public static string? RunLDADB(LDDevice device, string command)
        {
            var option = !string.IsNullOrWhiteSpace(device.Index) ? "index" : "name";
            var nameOrIndex = !string.IsNullOrWhiteSpace(device.Index) ? device.Index : device.Name;

            command = @$"adb --{option} {nameOrIndex} --command ""{command}""";
            return GetResult(LDConsolePath, command);
        }
    }
}