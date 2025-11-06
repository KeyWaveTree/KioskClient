using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KioskClient.Model;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KioskClient.ViewModel
{
    public partial class ThemeViewModel : ViewModelBase
    {
        public ICommand ToggleThemeCommand { get; }

        private ThemeColorMode themeMode = ThemeColorMode.Morning;
        private ThemeConfig? themeConfig;


        public ThemeViewModel()
        {
            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            UpdateThemeConfig();
        }

        public ThemeColorMode ThemeMode
        {
            get { return themeMode; }
            set
            {
                if(Set(ref themeMode, value))
                {
                    UpdateThemeConfig();
                }
            }
        }

        public ThemeConfig? ThemeConfig
        {
            get { return themeConfig; }
            private set { Set(ref themeConfig, value); }
        }

        private void ToggleTheme()
        {
            ThemeMode = (ThemeMode == ThemeColorMode.Morning) ? 
                ThemeColorMode.Evening : ThemeColorMode.Morning;
        }

        private void UpdateThemeConfig()
        {
            if (ThemeMode == ThemeColorMode.Morning)
            {
                ThemeConfig = new ThemeConfig
                {
                    BackgroundBrush = new LinearGradientBrush(
                        Color.FromRgb(224, 242, 254), // sky-100
                        Color.FromRgb(239, 246, 255), // blue-50
                        new Point(0.5, 0), new Point(0.5, 1)),
                    Orb1Brush = new SolidColorBrush(Color.FromArgb(128, 34, 211, 238)), // cyan-400/50
                    Orb2Brush = new SolidColorBrush(Color.FromArgb(102, 96, 165, 250)), // blue-400/40
                    TextPrimaryBrush = new SolidColorBrush(Color.FromRgb(30, 41, 59)), // slate-800
                    TextSecondaryBrush = new SolidColorBrush(Color.FromRgb(71, 85, 105)), // slate-600
                    GlassBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255)), // white/40
                    GlassBorderBrush = new SolidColorBrush(Color.FromArgb(153, 255, 255, 255)), // white/60
                    GlassHoverBrush = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255)) // white/50
                };
            }
            else // Evening
            {
                ThemeConfig = new ThemeConfig
                {
                    BackgroundBrush = new SolidColorBrush(Color.FromRgb(45, 50, 80)), // #2d3250
                    Orb1Brush = new SolidColorBrush(Color.FromArgb(51, 253, 186, 116)), // orange-300/20
                    Orb2Brush = new SolidColorBrush(Color.FromArgb(38, 249, 168, 212)), // pink-300/15
                    TextPrimaryBrush = new SolidColorBrush(Color.FromRgb(254, 252, 232)), // amber-50
                    TextSecondaryBrush = new SolidColorBrush(Color.FromArgb(179, 254, 240, 138)), // amber-200/70
                    GlassBrush = new SolidColorBrush(Color.FromArgb(153, 66, 71, 105)), // #424769/60
                    GlassBorderBrush = new SolidColorBrush(Color.FromArgb(128, 112, 119, 161)), // #7077a1/50
                    GlassHoverBrush = new SolidColorBrush(Color.FromArgb(179, 66, 71, 105)) // #424769/70
                };
            }
        }
    }
}
