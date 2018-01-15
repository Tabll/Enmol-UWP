using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Enmol.Tools
{
    class ColorHelpTools
    {
        public Color ColorHx16toRGB(string strHxColor)
        {
            try
            {
                if (strHxColor.Length == 0)
                {
                    return Color.FromArgb(255, 0, 0, 0);
                }
                else
                {
                    return Color.FromArgb(255, (byte)System.Int32.Parse(strHxColor.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier), (byte)System.Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier), (byte)System.Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                }
            }
            catch
            {
                return Color.FromArgb(255, 0, 0, 0);
            }
        }

        public string ToHexColor(Color color)
        {
            string R = Convert.ToString(color.R, 16);
            if (R == "0")
                R = "00";
            string G = Convert.ToString(color.G, 16);
            if (G == "0")
                G = "00";
            string B = Convert.ToString(color.B, 16);
            if (B == "0")
                B = "00";
            string HexColor = "#FF" + R + G + B;
            return HexColor;
        }
    }
}
