using LDAutoHelper;
using LDAutoHelper.Entity;

namespace LDAutoHelper.LDConsole
{
    public class LDHelper
    {
        public static List<string> GetDevices()
        {
            var result = CommonHelper.RunLD(LDConstants.ListDevices);
            if (result == null) return new List<string>();
            return result.Split('\n').ToList();
        }

        public static List<LDDevice> GetDevices2()
        {
            var devices = new List<LDDevice>();
            var result = CommonHelper.RunLD(LDConstants.ListDevices2);
            if (result == null) return devices;

            foreach (var deviceInfo in result.Split('\n'))
            {
                if (string.IsNullOrWhiteSpace(deviceInfo)) continue;
                var device = deviceInfo.Split(',');

                if (device.Length < 2) continue;
                devices.Add(new LDDevice
                {
                    Index = device[0],
                    Name = device[1]
                });
            }
            return devices;
        }

        public static string? Start(string nameOrIndex)
        {
            var command = string.Format(LDConstants.Launch, nameOrIndex);
            return CommonHelper.RunLD(command);
        }

        public static string? Quit(string nameOrIndex)
        {
            var command = string.Format(LDConstants.Quit, nameOrIndex);
            return CommonHelper.RunLD(command);
        }

        public static string? QuitAll()
        {
            return CommonHelper.RunLD(LDConstants.QuitAll);
        }
    }
}
