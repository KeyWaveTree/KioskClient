using System;
using System.Windows;
using System.Windows.Controls;       // [오류 수정] 'Canvas'
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;  // [오류 수정] 'BlurEffect'

/*
 * KioskClient-master의 'MainWindow.xaml.cs'를 수정합니다.
 * * [오류 수정]
 * 1. 'Figma wpf'의 'mainwindow-cs.cs'에서 Orb 애니메이션 로직을 가져옵니다.
 * 2. 애니메이션에 필요한 'using' 지시문들을 추가합니다.
 * 3. [중요] 생성자 'MainWindow()'에서 'InitializeAnimations()'를
 * 호출하면 'Orb1'이 null이므로 런타임 오류가 발생합니다.
 * XAML이 모두 로드된 후인 'Loaded' 이벤트 핸들러로 이동시킵니다.
 *
 * * [효율성]
 * 1. 'DispatcherTimer' (시간 표시) 로직은 'MainViewModel'로 이동했으므로
 * 여기서는 제거합니다.
 */
namespace KioskClient
{
    public partial class MainWindow : Window
    {
        private Storyboard _orb1Animation;
        private Storyboard _orb2Animation;

        public MainWindow()
        {
            InitializeComponent();

            // [오류 수정] 생성자가 아닌 Loaded 이벤트에서 애니메이션 초기화
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAnimations(); //
        }


        /// <summary>
        /// 배경 Orb 애니메이션 초기화 (Figma 예제)
        /// </summary>
        private void InitializeAnimations()
        {
            // XAML의 Orb 컨트롤 찾기
            var orb1 = (Ellipse)this.FindName("Orb1");
            var orb2 = (Ellipse)this.FindName("Orb2");

            if (orb1 == null || orb2 == null) return; // 컨트롤을 찾지 못하면 중단

            // Orb1 애니메이션
            _orb1Animation = new Storyboard { RepeatBehavior = RepeatBehavior.Forever };
            var orb1ScaleX = new DoubleAnimation(1.0, 1.15, TimeSpan.FromSeconds(6))
            {
                AutoReverse = true,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTarget(orb1ScaleX, orb1);
            Storyboard.SetTargetProperty(orb1ScaleX, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));

            var orb1ScaleY = orb1ScaleX.Clone();
            Storyboard.SetTarget(orb1ScaleY, orb1);
            Storyboard.SetTargetProperty(orb1ScaleY, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            orb1.RenderTransformOrigin = new Point(0.5, 0.5);
            orb1.RenderTransform = new ScaleTransform();
            _orb1Animation.Children.Add(orb1ScaleX);
            _orb1Animation.Children.Add(orb1ScaleY);

            // Orb2 애니메이션
            _orb2Animation = new Storyboard { RepeatBehavior = RepeatBehavior.Forever };
            var orb2ScaleX = new DoubleAnimation(1.0, 1.2, TimeSpan.FromSeconds(7.5))
            {
                AutoReverse = true,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTarget(orb2ScaleX, orb2);
            Storyboard.SetTargetProperty(orb2ScaleX, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));

            var orb2ScaleY = orb2ScaleX.Clone();
            Storyboard.SetTarget(orb2ScaleY, orb2);
            Storyboard.SetTargetProperty(orb2ScaleY, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            orb2.RenderTransformOrigin = new Point(0.5, 0.5);
            orb2.RenderTransform = new ScaleTransform();
            _orb2Animation.Children.Add(orb2ScaleX);
            _orb2Animation.Children.Add(orb2ScaleY);

            _orb1Animation.Begin(this, true);
            _orb2Animation.Begin(this, true);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _orb1Animation?.Stop(this);
            _orb2Animation?.Stop(this);
        }
    }
}