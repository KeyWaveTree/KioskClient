using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KioskClient.Message;

namespace KioskClient.ViewModel
{
    public class PopupViewModel:ViewModelBase
    {
        public ICommand ClosePopupCommand { get; private set; }
        
        public PopupViewModel()
        {
            ClosePopupCommand = new RelayCommand(ExcuteClosePopup);
        }

        private void ExcuteClosePopup()
        {
            Messenger.Default.Send(new PopUpMessage(PopUpName.Close));
        }
    }
}
