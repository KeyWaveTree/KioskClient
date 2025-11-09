using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using KioskClient.DTO;
using KioskClient.Service;
using CommonServiceLocator;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;


namespace KioskClient.ViewModel
{
    public partial class MenuViewModel : ViewModelBase
    {

        private readonly ApiService? apiService;
        private int totalItems;
        private int totalPrice;

        public MenuCategoryDTO SelectedCategory { get; }

        public ObservableCollection<MenuProductViewModel> Products { get; }

        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand GoHomeCommand { get; }
        public bool IsCartVisible => TotalItems > 0;

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
            apiService = ServiceLocator.Current.GetInstance<ApiService>();

            SelectedCategory = category;

            // 빈 컬렉션으로 초기화
            Products = new ObservableCollection<MenuProductViewModel>();

            //서버에서 상품 목록을 비동기로 로드
            LoadProductsAsync(category.Id);

            // Commands
            AddToCartCommand = new RelayCommand<MenuProductViewModel>(p => p?.IncrementQuantity());
            RemoveFromCartCommand = new RelayCommand<MenuProductViewModel>(p => p?.DecrementQuantity());
            GoHomeCommand = new RelayCommand(() =>
                Messenger.Default.Send(new GoToPageMessage(PageName.Welcome)));
        }

        private async void LoadProductsAsync(string categoryId)
        {
            try
            {
                //서버에서 상품 목록 api 호출 
                var productModels = await apiService.GetProductsDTOAsync(categoryId);

                //ui 스레드에서 product 컬렉션 추가
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Products.Clear();

                    foreach (var product in productModels)
                    {
                        var pvm = new MenuProductViewModel(product);
                        // 각 상품의 수량 변경 이벤트 추가
                        pvm.QuantityChanged += (s, e) => UpdateCartTotals();
                        Products.Add(pvm);
                    }
                });
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"APi Error (Products): {ex.Message}");
                //상품 로르 실패 팝업을 띄어야 함.
            }
        }

        private void UpdateCartTotals()
        {
            TotalItems = Products.Sum(p => p.Quantity);
            TotalPrice = Products.Sum(p => p.Product.Price * p.Quantity);
        }
    }
}
