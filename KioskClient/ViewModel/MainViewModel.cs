using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using KioskClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace KioskClient.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? currentViewModel;
        private ViewModelBase? popViewModel;
        private string? currentTime;
        public ThemeViewModel ThemeViewModel { get; } //테마 viewModel
        public ICommand GoHomeCommand { get; }
        public ICommand CloseMenuCommand { get; }
        public MainViewModel()
        {
            ThemeViewModel = new ThemeViewModel();

            GoHomeCommand = new RelayCommand(ExecuteGoHome);
            CloseMenuCommand = new RelayCommand(ExecuteCloseMenu);

            // 메시지 수신 등록
            // Messenger는 Unregister를 통해 메모리를 비우지 않으면 메모리 누수가 나는 문제점이 있다고 함.
            // 따라서 후에 CommunityToolkit.Mvvm을 사용하여 WeakReferenceMessenger를 통해 메모리 누수 방지를 하면 좋을 것같음.
            Messenger.Default.Register<GoToPageMessage>(this,(action) => ReceiveMessage(action));
            Messenger.Default.Register<PopUpMessage>(this,(action) => ReceivePopupMessage(action));

            // 시간 표시 타이머 설정
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, e) => { CurrentTime = DateTime.Now.ToString("hh:mm tt"); };
            timer.Start();
            CurrentTime = DateTime.Now.ToString("hh:mm tt");

            // 초기 화면을 WelcomeViewModel로 설정
            CurrentViewModel = new WelcomeViewModel();
        }

        public ViewModelBase? CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(ref currentViewModel, value); }
        }

        public ViewModelBase? PopViewModel
        {
            get { return popViewModel; }
            set { Set(ref popViewModel, value); }
        }

        public string? CurrentTime
        {
            get { return currentTime; }
            set { Set(ref currentTime, value); }
        }

        private void ExecuteGoHome()
        {
            CurrentViewModel = new WelcomeViewModel();
        }

        private void ExecuteCloseMenu()
        {
            CurrentViewModel = new WelcomeViewModel(true);
        }

        private object? ReceiveMessage(GoToPageMessage action)
        {
            switch (action.PageName)
            {
                //추가적인 페이지가 나올 시 case 추가하는 것 같음.
                case PageName.Welcome:
                    CurrentViewModel = new WelcomeViewModel();
                    break;
                case PageName.Choice:
                    CurrentViewModel = new WelcomeViewModel(true);
                    break;
                case PageName.Menu:
                    //파라미터 카테고리를 전달하여 MenuViewModel 생성
                    if (action.Param is MenuCategoryDTO category)
                        CurrentViewModel = new MenuViewModel(category);
                    break;
                
                // 다른 페이지가 있다면 여기에 case 추가
                default:
                    break;
            }

            return null;
        }

        private object? ReceivePopupMessage(PopUpMessage action)
        {
            switch (action.PopUpName)
            {
                // 팝업 닫기
                case PopUpName.Close:
                    PopViewModel = null;
                    break;

                default:
                    break;
            }

            return null;
        }
    }
}
