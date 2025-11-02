using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KioskClient.Message;
using System.Windows.Input;

namespace KioskClient.ViewModel
{
    public class TestViewModel:ViewModelBase
    {
        //xaml view와 바인딩할 Command 속성 추가 
        public ICommand? OpenPopupCommand { get; private set; }

        //view Model 생성시 Command 초기화
        public TestViewModel()
        {
            //OpenPopupCommand가 실행되면 ExecuteOpenPopup 메서드 호출 지정
            OpenPopupCommand = new RelayCommand(ExecuteOpenPopup);
        }
       
        //Command가 실행할 실제 메서드 
        private void ExecuteOpenPopup()
        {
            //MainVeiwModel이 받을 수 있도록 PopupMessage 전송
            //PopupName.PopupTest를 지정하면 MainViewModel이 PopupViewModel을 생성
            Messenger.Default.Send(new PopUpMessage(PopUpName.PopupTest));
        }

    }
}
