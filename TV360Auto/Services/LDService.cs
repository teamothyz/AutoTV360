using Emgu.CV;
using LDAutoHelper.ADB;
using LDAutoHelper.Entity;
using LDAutoHelper.LDConsole;

namespace TV360Auto.Services
{
    public class LDService
    {
        private static string GetPath(string file)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var resourcesPath = Path.Combine(basePath, "resources");
            return Path.Combine(resourcesPath, file);
        }

        public static void StartAuto(LDDevice device, CancellationToken token)
        {
            LDHelper.Start(device);
            Task.Delay(1000, token).Wait(token);

            var checkRunSuccess = ADBHelper.Tap(device, 0, 0);
            while (checkRunSuccess == null || checkRunSuccess.Contains("device not found"))
            {
                Task.Delay(1000, token).Wait(token);
                checkRunSuccess = ADBHelper.Tap(device, 0, 0);
            }

            TapImage(device, "accounticon.png", token);
            Task.Delay(1000, token).Wait(token);
            TapImage(device, "loginbtn.png", token);
            Task.Delay(1000, token).Wait(token);
            TapImage(device, "phonetxtbox.png", token);
            Task.Delay(1000, token).Wait(token);

            ADBHelper.SendText(device, "0357090609");
            Task.Delay(1000, token).Wait(token);
            ADBHelper.SendKeyCode(device, "66");
            Task.Delay(1000, token).Wait(token);
            ADBHelper.SendKeyCode(device, "66");
            Task.Delay(1000, token).Wait(token);

            //registed
            TapImage(device, "passwordtxtbox.png", token);
            Task.Delay(1000, token).Wait(token);
            ADBHelper.SendText(device, "Tuan552001");
            Task.Delay(1000, token).Wait(token);
            ADBHelper.SendKeyCode(device, "66");
            Task.Delay(1000, token).Wait(token);
            ADBHelper.SendKeyCode(device, "66");
            Task.Delay(1000, token).Wait(token);

            //WaitImageVisible(device, "loginsuccessicon.png", token);
            //Task.Delay(1000, token).Wait(token);
            //ADBHelper.Roll(device, 0, 100);
            //Task.Delay(1000, token).Wait(token);
            //TapImage(device, "sharebtn.png", token);
            //Task.Delay(1000, token).Wait(token);
            //TapImage(device, "refertxtbox.png", token);
            //Task.Delay(1000, token).Wait(token);

            //ADBHelper.SendText(device, "referphone");
            //Task.Delay(1000, token).Wait(token);
            //ADBHelper.SendKeyCode(device, "66");
            //Task.Delay(1000, token).Wait(token);
            //ADBHelper.SendKeyCode(device, "66");

            //TapImage(device, "referconfirmacceptbtn.png", token);
            //Task.Delay(1000, token).Wait(token);
            //ADBHelper.SendKeyCode(device, "4");
            //Task.Delay(1000, token).Wait(token);

            TapImage(device, "filmbtn.png", token);
            Task.Delay(1000, token).Wait(token);
            var suggestFilmPoint = WaitImageVisible(device, "filmsuggest.png", token);
            Task.Delay(1000, token).Wait(token);
            ADBHelper.Tap(device, suggestFilmPoint.X, suggestFilmPoint.Y + 150);
        }

        private static Point WaitImageVisible(LDDevice device, string filename, CancellationToken token)
        {
            var imgPath = GetPath(filename);
            var imgPoint = ADBHelper.FindImage(device, imgPath);
            while (imgPoint == null)
            {
                Task.Delay(1000, token).Wait(token);
                imgPoint = ADBHelper.FindImage(device, imgPath);
            }
            return imgPoint.Value;
        }

        private static void TapImage(LDDevice device, string filename, CancellationToken token)
        {
            var imgPath = GetPath(filename);
            var imgPoint = ADBHelper.FindImage(device, imgPath);
            while (imgPoint == null)
            {
                Task.Delay(1000, token).Wait(token);
                imgPoint = ADBHelper.FindImage(device, imgPath);
            }
            Task.Delay(1000, token).Wait(token);
            ADBHelper.Tap(device, imgPoint.Value.X, imgPoint.Value.Y);
        }
    }
}
