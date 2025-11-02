using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using KioskClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.ViewModel
{

    public class MainViewModel: ViewModelBase
    {

        private ViewModelBase? _currentViewModel;
        private ViewModelBase? _popViewModel;

        public MainViewModel()
        {
            Messenger.Default.Register<GoToPageMessage>(this, (action) => ReceiveMessage(action));
            Messenger.Default.Register<PopUpMessage>(this, (action) => ReceivePopupMessage(action));
            
            Messenger.Default.Send(new GoToPageMessage(PageName.Test));
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

        private object? ReceiveMessage(GoToPageMessage action)
        {
            switch (action.pageName)
            {
                //추가적인 페이지가 나올 시 case 추가하는 것 같음.
                case PageName.Test:
                    //지정할 페이지 삽입?
                    CurrentViewModel = new TestViewModel();
                    break;

                
                default:
                    break;
            }

            return null;
        }

        private object? ReceivePopupMessage(PopUpMessage action)
        {
            switch (action.popUpName)
            {
                //추가적인 엑션 관련 이벤트가 나올때 마다 뷰모델을 생성하는 Case 추가하는 것 같음.
                case PopUpName.PopupTest:
                    PopViewModel = new PopupViewModel();
                    break;

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
