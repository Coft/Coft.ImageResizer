using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Coft.ImageResizer.Models.Helpers.Enums;

namespace Coft.ImageResizer.Models.Helpers
{
    public class Configuration
    {
        public static int MaxWidth { get; private set; } = 1600;
        public static int MaxHeight { get; private set; } = 1200;

        public static string FilenameApendix { get; private set; } = "_min";

        public static BitmapOutput DefaultBitmapOutput { get; private set; } = BitmapOutput.Jpeg;

        public static string AuthorEmail { get; private set; } = "coft@o2.pl";
    }
}
