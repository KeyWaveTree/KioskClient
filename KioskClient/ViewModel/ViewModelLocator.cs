using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using KioskClient.Service;
using System.Net.Http;

namespace KioskClient.ViewModel
{
    public class ViewModelLocator
    {


        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //ssl 인증서 사용하므로 httpClient는 인증서를 신뢰하도록 설정
            var handler = new HttpClientHandler
            {
                //유효성 감사를 무시
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            SimpleIoc.Default.Register<HttpClient>(() => new HttpClient(handler));

            SimpleIoc.Default.Register<ApiService>();
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
