using GalaSoft.MvvmLight;
using KioskClient.Model;
using System;

/*
 * KioskClient-master에 추가 (효율성 개선의 핵심)
 * * - 이 클래스는 'Figma wpf' 예제의 비효율성을 해결하기 위해 새로 작성되었습니다.
 * - Figma 예제는 'menuview-xaml-2.txt'와 'menuview-cs-2.cs'에서
 * IMultiValueConverter를 사용해 View가 ViewModel의 상태를 복잡하게 계산합니다.
 * * - 이 효율적인 방식에서는 'MenuProductViewModel'이 'Quantity'(수량) 상태를
 * 직접 소유합니다.
 * - View('MenuView.xaml')는 이 ViewModel의 'Quantity'와 'IsInCart' 속성에
 * 단순히 바인딩만 하면 됩니다.
 * - 이로 인해 'menuview-cs-2.cs'의 모든 컨버터와 'menuview-cs.cs'의 
 * 이벤트 구독 코드 비하인드가 전부 필요 없어집니다.
 */
namespace KioskClient.ViewModel
{
    public class MenuProductViewModel : ViewModelBase
    {
        public MenuProduct Product { get; }

        private int _quantity;
        /// <summary>
        /// 이 상품의 현재 장바구니 수량
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (Set(ref _quantity, value))
                {
                    // 수량 변경 시, 'IsInCart' 속성도 변경되었음을 View에 알림
                    RaisePropertyChanged(nameof(IsInCart));
                    // 부모 ViewModel(MenuViewModel)에 총계를 업데이트하라고 알림
                    QuantityChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 수량이 0보다 큰지 여부 (View가 UI 전환에 사용)
        /// </summary>
        public bool IsInCart => Quantity > 0;

        /// <summary>
        /// 수량 변경 시 MenuViewModel에 알리기 위한 이벤트
        /// </summary>
        public event EventHandler QuantityChanged;

        public MenuProductViewModel(MenuProduct product)
        {
            Product = product;
        }
    }
}