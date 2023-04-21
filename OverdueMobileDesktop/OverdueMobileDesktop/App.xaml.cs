using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("MaterialIcons.ttf", Alias = "MaterialRegular")]
[assembly: ExportFont("MaterialIconsTwoTone.ttf", Alias = "MaterialTwoTone")]
namespace OverdueMobileDesktop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
