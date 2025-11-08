using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using KioskClient.DTO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KioskClient.ViewModel
{
    public partial class MenuViewModel : ViewModelBase
    {
      
        public MenuCategoryDTO SelectedCategory { get; }

        public ObservableCollection<MenuProductViewModel> Products { get; }

        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand GoHomeCommand { get; }
        public bool IsCartVisible => TotalItems > 0;

        private int totalItems;
        private int totalPrice;

        public int TotalItems
        {
            get { return totalItems; }
            set
            {
                if(Set(ref totalItems, value))
                {
                    RaisePropertyChanged(nameof(IsCartVisible));
                }
            }
        }

        public int TotalPrice
        {
            get { return totalPrice; }
            set { Set(ref totalPrice, value); }
        }

        public MenuViewModel(MenuCategoryDTO category)
        {
            SelectedCategory = category;

            // 상품 로드
            var productModels = MenuData.MenuProducts[category.Id];

            // Model -> ViewModel로 래핑
            Products = new ObservableCollection<MenuProductViewModel>(
                productModels.Select(p => new MenuProductViewModel(p))
            );

            // 각 상품의 수량 변경 이벤트 구독
            foreach (var pvm in Products)
            {
                pvm.QuantityChanged += (s, e) => UpdateCartTotals();
            }

            // Commands
            AddToCartCommand = new RelayCommand<MenuProductViewModel>(p => p?.IncrementQuantity());
            RemoveFromCartCommand = new RelayCommand<MenuProductViewModel>(p => p?.DecrementQuantity());
            GoHomeCommand = new RelayCommand(() =>
                Messenger.Default.Send(new GoToPageMessage(PageName.Welcome)));
        }

        private void UpdateCartTotals()
        {
            TotalItems = Products.Sum(p => p.Quantity);
            TotalPrice = Products.Sum(p => p.Product.Price * p.Quantity);
        }
    }
}
