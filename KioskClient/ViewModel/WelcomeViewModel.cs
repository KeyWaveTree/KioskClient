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
using System.Windows.Input;



namespace KioskClient.ViewModel
{
    public partial class WelcomeViewModel : ViewModelBase
    {
        private readonly ApiService apiService;
        private bool showCategories;
        private string statusMessage = "서버에 연결중";

        //비동기 로드 후 ui 갱신을 위해 ObservableCollection 사용
        public ObservableCollection<MenuCategoryDTO> Categories { get; }
        public RelayCommand<MenuCategoryDTO> SelectCategoryCommand { get; }
        public ICommand StartCommand { get; }


        public WelcomeViewModel(bool isShowCategories = false)
        {
            apiService = ServiceLocator.Current.GetInstance<ApiService>();
            ShowCategories = isShowCategories;
            StatusMessage = "서버에 연결중";
            Categories = new ObservableCollection<MenuCategoryDTO>();
            SelectCategoryCommand = new RelayCommand<MenuCategoryDTO>(ExecuteSelectCategory);
            StartCommand = new RelayCommand(ExecuteStart);
 
            LoadCategoriesWithRetryAsync();

        }
        public bool ShowCategories
        {
            get { return showCategories; }
            set { Set(ref showCategories, value); }
        }

        public string StatusMessage
        {
            get { return statusMessage; }
            set { Set(ref statusMessage, value); }
        }
        private void ExecuteSelectCategory(MenuCategoryDTO? category)
        {
            if (category == null) return;

            // MainViewModel에 페이지 전환 메시지 전송
            Messenger.Default.Send(new GoToPageMessage(PageName.Menu, category));
        }

        private void ExecuteStart()
        {
            //카테고리 항목들을 화면에 표시하도록 플래그 변경
            ShowCategories = true;
        }


        private async void LoadCategoriesWithRetryAsync()
        {
            bool success = false;
            while(!success)
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

                    StatusMessage = "화면을 터치하여 시작하세요";
                    success = true;
                }
                catch (HttpRequestException ex)
                {
                    //서버가 꺼져잇거나, 주소가 틀렸거나, api가 오류를 뱉을때 발생
                    Console.WriteLine($"API error{ex.Message}");
                    StatusMessage = "서버 연결 재시도 중..";

                    //실패시 3초 대기 후 루프 재시작
                    await Task.Delay(3000);

                    //팝업 로직 추가해야함.
                }
            }
            
        }
    }
}
