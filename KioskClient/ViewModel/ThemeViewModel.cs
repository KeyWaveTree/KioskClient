using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command; // KioskClient-master가 사용하는 MvvmLight
using KioskClient.Model;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

/*
 * KioskClient-master에 추가 (Figma wpf/themeviewmodel.cs 기반)
 * * - Figma 예제의 테마 관리 로직입니다.
 * - INotifyPropertyChanged 대신 'ViewModelBase'를 상속받습니다.
 * - Figma 예제의 'RelayCommand' 대신 MvvmLight의 'RelayCommand'를 사용합니다.
 */
namespace KioskClient.ViewModel
{
    public class ThemeViewModel : ViewModelBase // ViewModelBase 상속
    {
        private ThemeColorMode _themeMode = ThemeColorMode.Morning; //
        public ThemeColorMode ThemeMode
        {
            get => _themeMode;
            set
            {
                // MvvmLight의 Set()을 사용하여 값 변경 및 자동 알림
                if (Set(ref _themeMode, value)) //
                {
                    UpdateThemeConfig(); //
                }
            }
        }

        private ThemeConfig _themeConfig;
        public ThemeConfig ThemeConfig
        {
            get => _themeConfig;
            private set => Set(ref _themeConfig, value); //
        }

        public ICommand ToggleThemeCommand { get; }

        public ThemeViewModel()
        {
            // MvvmLight의 RelayCommand 사용
            ToggleThemeCommand = new RelayCommand(ToggleTheme); //
            UpdateThemeConfig();
        }

        private void ToggleTheme()
        {
            ThemeMode = ThemeMode == ThemeColorMode.Morning ? ThemeColorMode.Evening : ThemeColorMode.Morning; //
        }

        /// <summary>
        /// 테마 변경 시 Brush 객체들을 업데이트 (Figma 예제 로직)
        /// </summary>
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