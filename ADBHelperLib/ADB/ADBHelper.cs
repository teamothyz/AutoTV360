using ADBHelperLib.Image;
using Emgu.CV;
using System;
using System.Diagnostics;
using System.Drawing;

namespace ADBHelperLib.ADB
{
    public class ADBHelper
    {
        public static int GlobalTimeout { get; set; } = 10;

        private static Process GetProcess(string command, bool needOutput = true)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = needOutput,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process
            {
                StartInfo = startInfo
            };
            return process;
        }

        private static string? RunProcessWithOutput(string command)
        {
            using var process = GetProcess(command, true);
            process.Start();
            string? result = null;
            if (process.WaitForExit(GlobalTimeout * 1000))
            {
                result = process.StandardOutput.ReadToEnd();
            }
            process.Kill();
            process.Close();
            return result;
        }

        private static bool RunProcessNoOutput(string command)
        {
            using var process = GetProcess(command, false);
            process.Start();
            var result = process.WaitForExit(GlobalTimeout * 1000);

            process.Kill();
            process.Close();
            return result;
        }

        public static List<string> GetDevices()
        {
            var devices = new List<string>();
            var result = RunProcessWithOutput(ADBConstants.GetDevice);
            if (result == null) return devices;

            var devicesInfo = result.Split("\n").ToList();
            devicesInfo.RemoveAt(0);

            foreach (var deviceInfo in devicesInfo)
            {
                if (string.IsNullOrWhiteSpace(deviceInfo)) continue;

                var deviceId = deviceInfo.Split("\t")[0].Trim();
                if (string.IsNullOrWhiteSpace(deviceId)) continue;

                var deviceStatus = deviceInfo.Split("\t")[1].Trim();
                if (!deviceStatus.Contains("device")) continue;

                devices.Add(deviceId);
            }
            return devices;
        }

        public static bool Tap(string deviceId, float x, float y)
        {
            var command = string.Format(ADBConstants.Tap, deviceId, x, y);
            return RunProcessNoOutput(command);
        }

        public static bool Swipe(string deviceId, float srcx, float srcy, float desx, float desy, float duration)
        {
            var command = string.Format(ADBConstants.Swipe, deviceId, srcx, srcy, desx, desy, duration * 1000);
            return RunProcessNoOutput(command);
        }

        public static Point? FindImage(string deviceId, string src)
        {
            var fileName = ImageHelper.GetScreenShotFileName(".png");
            var folder = ImageHelper.GetScreenShotFolder();
            var filePath = Path.Combine(folder, fileName);
            try
            {
                var screenShotCmd = string.Format(ADBConstants.ScreenShot, deviceId, fileName);
                _ = RunProcessNoOutput(screenShotCmd);

                var saveImgCmd = string.Format(ADBConstants.PullScreenShot, deviceId, fileName, filePath);
                _ = RunProcessNoOutput(saveImgCmd);

                var point = ImageHelper.GetPoint(src, filePath);
                return point;
            }
            finally
            {
                var deleteCmd = string.Format(ADBConstants.RemoveScreenShot, deviceId, fileName);
                _ = RunProcessNoOutput(deleteCmd);

                if (File.Exists(filePath)) File.Delete(filePath);
            }
        }
    }
}
