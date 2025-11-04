using System.Windows.Controls;

/*
 * KioskClient-master에 추가
 * * [오류 수정]
 * 1. 'v:MenuView' 형식을 찾을 수 없다는 XAML 오류는
 * 이 코드 비하인드 파일이 없으면 컴파일 시 형식이
 * 생성되지 않기 때문입니다.
 * * [효율성]
 * 1. 'Figma wpf'의 'menuview-cs.cs'에 있던 모든 이벤트 구독
 * 코드 비하인드 로직이 ViewModel로 이전되었으므로,
 * 이 파일은 비어있는 것이 올바른 MVVM 패턴입니다.
 */
namespace KioskClient.View
{
    /// <summary>
    /// MenuView.xaml에 대한 상호 작용 논리
    /// (로직은 ViewModel에 있으므로 비어있음)
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}