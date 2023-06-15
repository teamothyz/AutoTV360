namespace ADBHelperLib.ADB
{
    public class ADBConstants
    {
        public static readonly string GetDevice = "adb devices";

        //device - x - y
        public static readonly string Tap = "adb -s {0} shell input tap {1} {2}";

        //device - from x - from y - to x - to y - duration
        public static readonly string Swipe = "adb -s {0} shell input swipe {1} {2} {3} {4} {5}";

        //device - file name
        public static readonly string ScreenShot = "adb -s {0} shell screencap -p /sdcard/{1}";

        //device - file name - destination
        public static readonly string PullScreenShot = "adb -s {0} pull /sdcard/{1} {2}";

        //device - file name
        public static readonly string RemoveScreenShot = "adb -s {0} shell rm /sdcard/{1}";
    }
}
