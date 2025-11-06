using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Message;
using System.Windows.Input;

namespace KioskClient.ViewModel
{
    public partial class PopupViewModel : ViewModelBase
    {
        public ICommand ClosePopupCommand { get; }

        public PopupViewModel()
        {
            ClosePopupCommand = new RelayCommand(ExecuteClosePopup);
        }

        private void ExecuteClosePopup()
        { 
            Messenger.Default.Send(new PopUpMessage(PopUpName.Close));
        }
    }
}
