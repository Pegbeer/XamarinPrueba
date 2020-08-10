using PruebaXamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            

            MainPage = new MainPage() { BackgroundColor = Color.FromHex("#AFD5DD") };
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
