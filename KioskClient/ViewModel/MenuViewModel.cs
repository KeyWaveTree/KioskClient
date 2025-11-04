using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Data;
using KioskClient.Message;
using KioskClient.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

/*
 * KioskClient-master에 추가
 * * - Figma 예제의 'menuviewmodel.cs' (상품 로드)와
 * 'cartviewmodel.cs' (장바구니 로직)를 효율적으로 통합합니다.
 * - 상품 목록을 'MenuProductViewModel'의 컬렉션으로 관리합니다.
 */
namespace KioskClient.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuCategory SelectedCategory { get; }

        /// <summary>
        /// View에 바인딩될 상품 목록 (효율성 핵심: Model이 아닌 VM을 담음)
        /// </summary>
        public ObservableCollection<MenuProductViewModel> Products { get; }

        // --- 장바구니 로직 (Figma: cartviewmodel.cs) ---
        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand GoHomeCommand { get; }

        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            // 'IsCartVisible'에도 변경 알림을 보냄
            set { if (Set(ref _totalItems, value)) RaisePropertyChanged(nameof(IsCartVisible)); }
        }

        private int _totalPrice;
        public int TotalPrice
        {
            get => _totalPrice;
            set => Set(ref _totalPrice, value);
        }

        /// <summary>
        /// 장바구니 요약 UI의 Visibility를 제어 (Figma: menuview-cs-2.cs)
        /// </summary>
        public bool IsCartVisible => TotalItems > 0;

        public MenuViewModel(MenuCategory category)
        {
            SelectedCategory = category;

            // 1. 상품 로드 (Figma: menuviewmodel.cs)
            var productModels = MenuData.MenuProducts[category.Id];

            // 2. Model -> ViewModel로 래핑
            Products = new ObservableCollection<MenuProductViewModel>(
                productModels.Select(p => new MenuProductViewModel(p))
            );

            // 3. 장바구니 로직 (Figma: cartviewmodel.cs)
            // 각 자식VM(MenuProductViewModel)의 수량이 변경될 때마다 총계 업데이트
            foreach (var pvm in Products)
            {
                pvm.QuantityChanged += (s, e) => UpdateCartTotals();
            }

            // Command는 자식VM을 직접 조작
            AddToCartCommand = new RelayCommand<MenuProductViewModel>(p => p.Quantity++);
            RemoveFromCartCommand = new RelayCommand<MenuProductViewModel>(p => { if (p.Quantity > 0) p.Quantity--; });

            // 홈으로 가기 (Messenger 사용)
            GoHomeCommand = new RelayCommand(() => Messenger.Default.Send(new GoToPageMessage(PageName.Welcome)));
        }

        /// <summary>
        /// 장바구니 총 수량 및 금액 업데이트 (Figma: cartviewmodel.cs)
        /// </summary>
        private void UpdateCartTotals()
        {
            TotalItems = Products.Sum(p => p.Quantity);
            TotalPrice = Products.Sum(p => p.Product.Price * p.Quantity);
        }
    }
}