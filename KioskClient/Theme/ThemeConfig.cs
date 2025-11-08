using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KioskClient.Theme
{
    public enum ThemeColorMode
    {
        Morning,
        Evening,
    }

    public class ThemeConfig
    {
        public Brush BackgroundBrush { get; set; }//
        public Brush Orb1Brush { get; set; }//
        public Brush Orb2Brush {get; set;}//
        public Brush TextPrimaryBrush { get; set; }//
        public Brush TextSecondaryBrush { get; set; }//
        public Brush GlassBrush { get; set; }//
        public Brush GlassBorderBrush { get; set; }//
        public Brush GlassHoverBrush { get; set; }//

    }
}
