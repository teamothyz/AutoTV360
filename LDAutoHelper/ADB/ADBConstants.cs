namespace LDAutoHelper.ADB
{
    public class ADBConstants
    {
        public static readonly string GetDevice = "devices";

        //x - y
        public static readonly string Tap = "shell input tap {0} {1}";

        //from x - from y - to x - to y - duration
        public static readonly string Swipe = "shell input swipe {0} {1} {2} {3} {4}";

        //file name
        public static readonly string ScreenShot = "shell screencap -p /sdcard/{0}";

        //file name - destination
        public static readonly string PullScreenShot = "pull /sdcard/{0} {1}";

        //file name
        public static readonly string RemoveScreenShot = "shell rm /sdcard/{0}";

        //package
        public static readonly string RunApp = "shell monkey -p {0} 1";

        //package
        public static readonly string ClearApp = "shell pm clear {0}";

        //package
        public static readonly string KillApp = "am force-stop {0}";

        //text
        public static readonly string SendText = @"shell input text ""{0}""";
    }
}