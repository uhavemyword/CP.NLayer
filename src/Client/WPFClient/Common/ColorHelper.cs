using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace CP.NLayer.Client.WpfClient.Common
{
    public static class ColorHelper
    {
        //http://msdn.microsoft.com/en-us/library/windows/desktop/bb773849(v=vs.85).aspx
        [DllImport("shlwapi.dll")]
        private static extern int ColorHLSToRGB(int h, int l, int s);

        private static Color ColorFromWin32(int win32Color)
        {
            var color = System.Drawing.ColorTranslator.FromWin32(win32Color);
            return Color.FromRgb(color.R, color.G, color.B);
        }

        public static List<Color> GenerateColors(int number)
        {
            var colors = new List<Color>();
            if (number < 0)
            {
                return colors;
            }

            // hue [0, 240), saturation [0, 240), lightness [0, 240)
            for (int i = 0; i < 240; i += 240 / number)
            {
                int hue = i;
                int saturation = (int)(240 * 0.8);
                int lightness = (int)(240 * 0.8);

                var color = ColorFromWin32(ColorHLSToRGB(hue, saturation, lightness));
                color.A = (int)(255 * 0.75);  //[0,255]
                colors.Add(color);
            }

            return colors;
        }
    }
}