using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging; // KioskClient-master의 Messenger
using KioskClient.Data;
using KioskClient.Message; // NavigationMessage
using KioskClient.Model;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

/*
 * KioskClient-master에 추가 (Figma wpf/navigationviewmodel.cs 기반)
 * * - 'WelcomeView'의 로직을 담당합니다.
 * - Figma 예제의 'NavigationViewModel'의 카테고리 선택 로직을 가져왔습니다.
 * - 'SelectCategoryCommand' 실행 시, 'KioskClient-master'의 Messenger를 사용해
 * 'MainViewModel'에 페이지 전환을 요청합니다.
 */
namespace KioskClient.ViewModel
{
    public class WelcomeViewModel : ViewModelBase
    {
        /// <summary>
        /// View에 표시할 카테고리 목록
        /// </summary>
        public List<MenuCategory> Categories { get; }

        /// <summary>
        /// 카테고리 선택 시 실행될 명령
        /// </summary>
        public RelayCommand<MenuCategory> SelectCategoryCommand { get; }

        public WelcomeViewModel()
        {
            Categories = MenuData.MenuCategories;
            SelectCategoryCommand = new RelayCommand<MenuCategory>(ExecuteSelectCategory);
        }

        /// <summary>
        /// 카테고리 선택 시 MainViewModel에 "메뉴 페이지로 이동" 메시지 전송
        /// </summary>
        private void ExecuteSelectCategory(MenuCategory category)
        {
            if (category == null) return;
            
            // MainViewModel이 이 메시지를 수신하여 CurrentViewModel을 교체
            Messenger.Default.Send(new GoToPageMessage(PageName.Menu, category));
            
        }
    }
}