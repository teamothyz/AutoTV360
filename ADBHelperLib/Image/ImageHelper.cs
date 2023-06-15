using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.CvEnum;

namespace ADBHelperLib.Image
{
    public class ImageHelper
    {
        public static string GetScreenShotFileName(string extension)
        {
            return $"{Guid.NewGuid().ToString().Replace("-", "")}_{DateTime.Now:ddMMyyyy}{extension}";
        }

        public static string GetScreenShotFolder()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var folderPath = Path.Combine(basePath, "screenshot");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        public static Point? GetPoint(string needToFind, string findInFile)
        {
            var temp = new Image<Bgr, byte>(needToFind);
            var target = new Image<Bgr, byte>(findInFile);
            var result = target.MatchTemplate(temp, TemplateMatchingType.CcoeffNormed);
            result.MinMax(out _, out _, out _, out Point[] maxLocations);
            return maxLocations?.FirstOrDefault();
        }
    }
}
