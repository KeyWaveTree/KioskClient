using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using KioskClient.Model;
using KioskClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace KioskClient.ViewModel
{

    public class MainViewModel: ViewModelBase
    {

        private ViewModelBase? _currentViewModel;
        private ViewModelBase? _popViewModel;
        private string? _currentTime;

        public ThemeViewModel ThemeViewModel { get; } //테마 viewModel

        
        public MainViewModel()
        {
            ThemeViewModel = new ThemeViewModel(); 
            Messenger.Default.Register<GoToPageMessage>(this, (action) => ReceiveMessage(action));
            Messenger.Default.Register<PopUpMessage>(this, (action) => ReceivePopupMessage(action));

            //시간 표시 타이머설정
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, e) => { CurrentTime = DateTime.Now.ToString("hh:mm tt"); };
            timer.Start();
            CurrentTime = DateTime.Now.ToString("hh:mm tt");
        }

        public ViewModelBase? CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(nameof(CurrentViewModel), ref _currentViewModel, value); }
        }
        
        public ViewModelBase? PopViewModel
        {
            get { return _popViewModel; }
            set { Set(nameof(PopViewModel), ref _popViewModel, value); }
        }

        /// <summary>
        /// 헤더에 표시될 현재 시간 (Figma: mainwindow-cs.cs)
        /// </summary>
        public string CurrentTime
        {
            get { return _currentTime; }
            set { Set(ref _currentTime, value); }
        }

        private object? ReceiveMessage(GoToPageMessage action)
        {
            switch (action.PageName)
            {
                //추가적인 페이지가 나올 시 case 추가하는 것 같음.
                case PageName.Welcome:
                    CurrentViewModel = new WelcomeViewModel();
                    break;

                case PageName.Menu:
                    //파라티터 카테고리를 전달하여 MenuViewModel 생성
                    if (action.Param is MenuCategory category) CurrentViewModel = new MenuViewModel(category);
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
