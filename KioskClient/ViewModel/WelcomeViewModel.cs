using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using KioskClient.DTO;
using KioskClient.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CommonServiceLocator;
using System.Net.Http;



namespace KioskClient.ViewModel
{
    public partial class WelcomeViewModel : ViewModelBase
    {
        private readonly ApiService apiService;

        //비동기 로드 후 ui 갱신을 위해 ObservableCollection 사용
        public ObservableCollection<MenuCategoryDTO> Categories { get; }
        public RelayCommand<MenuCategoryDTO> SelectCategoryCommand { get; }

        public WelcomeViewModel()
        {
            apiService = ServiceLocator.Current.GetInstance<ApiService>();

            Categories = new ObservableCollection<MenuCategoryDTO>();
            SelectCategoryCommand = new RelayCommand<MenuCategoryDTO>(ExecuteSelectCategory);

            LoadCategoriesAsync();
        }

        private void ExecuteSelectCategory(MenuCategoryDTO? category)
        {
            if (category == null) return;

            // MainViewModel에 페이지 전환 메시지 전송
            Messenger.Default.Send(new GoToPageMessage(PageName.Menu, category));
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                //서버에서 api 호출을 하여 데이터 가져온다.
                var categoriesList = await apiService.GetCategoriesDTOAsync();

                //UI 스레드에서 컬렉션 업데이트
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Categories.Clear();
                    foreach (var category in categoriesList) Categories.Add(category);
                });
            }
            catch(HttpRequestException ex)
            {
                //서버가 꺼져잇거나, 주소가 틀렸거나, api가 오류를 뱉을때 발생
                Console.WriteLine($"API error{ex.Message}");
            }
        }
    }
}
