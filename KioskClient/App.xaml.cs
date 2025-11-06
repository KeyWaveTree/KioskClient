using System.Configuration;
using System.Data;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using KioskClient.ViewModel;

namespace KioskClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ViewModelLocator 초기화 확인
            InitializeServiceLocator();
        }

        private void InitializeServiceLocator()
        {
            // ServiceLocator가 이미 설정되었는지 확인
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            }

            // ViewModel들이 등록되었는지 확인
            if (!SimpleIoc.Default.IsRegistered<MainViewModel>())
            {
                SimpleIoc.Default.Register<MainViewModel>();
            }

            if (!SimpleIoc.Default.IsRegistered<ThemeViewModel>())
            {
                SimpleIoc.Default.Register<ThemeViewModel>();
            }
        }
    }
}
