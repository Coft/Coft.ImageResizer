using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.ImageResizer.Models.Helpers
{
    public class Enums
    {
        public enum ProcessingStatus
        {
            Initial = 1,
            Success = 2,
            Error = 3,
        }

        public enum BitmapOutput
        {
            Jpeg = 1,
            Png = 2,
        }
    }
}
