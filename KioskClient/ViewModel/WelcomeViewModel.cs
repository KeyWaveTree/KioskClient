using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using KioskClient.Data;
using KioskClient.Message;
using KioskClient.Model;
using System.Collections.Generic;

namespace KioskClient.ViewModel
{
    public partial class WelcomeViewModel : ViewModelBase
    {
        public List<MenuCategory> Categories { get; }
        public RelayCommand<MenuCategory> SelectCategoryCommand { get; }

        public WelcomeViewModel()
        {
            Categories = MenuData.MenuCategories;
            SelectCategoryCommand = new RelayCommand<MenuCategory>(ExecuteSelectCategory);
        }

        private void ExecuteSelectCategory(MenuCategory? category)
        {
            if (category == null) return;

            // MainViewModel에 페이지 전환 메시지 전송
            Messenger.Default.Send(new GoToPageMessage(PageName.Menu, category));
        }
    }
}
