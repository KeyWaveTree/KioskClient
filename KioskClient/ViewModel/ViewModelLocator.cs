using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace KioskClient.ViewModel
{
    public class ViewModelLocator
    {
        private static MainViewModel? _main;

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ThemeViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            
        }
    }
}
