using LDAutoHelper.Entity;
using LDAutoHelper.Image;
using System.Drawing;

namespace LDAutoHelper.ADB
{
    public class ADBHelper
    {
        public static string? SendKeyCode(LDDevice device, string keyCode)
        {
            var command = string.Format(ADBConstants.SendKeyCode, keyCode);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? Roll(LDDevice device, int x, int y)
        {
            var command = string.Format(ADBConstants.Roll, x, y);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? Tap(LDDevice device, int x, int y)
        {
            var command = string.Format(ADBConstants.Tap, x, y);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? LongPress(LDDevice device, int x, int y, int duration)
        {
            var command = string.Format(ADBConstants.Swipe, x, y, x, y, duration * 1000);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? Swipe(LDDevice device, int srcx, int srcy, int desx, int desy, int duration)
        {
            var command = string.Format(ADBConstants.Swipe, srcx, srcy, desx, desy, duration * 1000);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? SendText(LDDevice device, string text)
        {
            text = text.Replace(" ", "%s")
                .Replace("&", "\\&")
                .Replace("<", "\\<")
                .Replace(">", "\\>")
                .Replace("?", "\\?")
                .Replace(":", "\\:")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("|", "\\|");
            var command = string.Format(ADBConstants.SendText, text);
            return CommonHelper.RunLDADB(device, command);
        }

        public static Point? FindImage(LDDevice device, string src)
        {
            var fileName = ImageHelper.GetScreenShotFileName(".png");
            var folder = ImageHelper.GetScreenShotFolder();
            var filePath = Path.Combine(folder, fileName);
            try
            {
                var screenShotCmd = string.Format(ADBConstants.ScreenShot, fileName);
                _ = CommonHelper.RunLDADB(device, screenShotCmd);

                var saveImgCmd = string.Format(ADBConstants.PullScreenShot, fileName, filePath);
                _ = CommonHelper.RunLDADB(device, saveImgCmd);

                var point = ImageHelper.GetPoint(src, filePath);
                return point;
            }
            finally
            {
                DeleteTempImage(device, fileName, filePath);
            }
        }

        public static string? RunApp(LDDevice device, string package)
        {
            var command = string.Format(ADBConstants.RunApp, package);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? ClearApp(LDDevice device, string package)
        {
            var command = string.Format(ADBConstants.ClearApp, package);
            return CommonHelper.RunLDADB(device, command);
        }

        public static string? KillApp(LDDevice device, string package)
        {
            var command = string.Format(ADBConstants.KillApp, package);
            return CommonHelper.RunLDADB(device, command);
        }

        private static void DeleteTempImage(LDDevice device, string fileName, string filePath)
        {
            try
            {
                var deleteCmd = string.Format(ADBConstants.RemoveScreenShot, fileName);
                _ = CommonHelper.RunLDADB(device, deleteCmd);
            }
            catch { }
            if (File.Exists(filePath)) try { File.Delete(filePath); } catch { }
        }
    }
}
