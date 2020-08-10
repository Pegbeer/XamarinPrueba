using PruebaXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            newOutBtn.Clicked += NewOutBtn_Clicked;
        }

        private async void NewOutBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SalidaPage());
        }

        private async void productsListBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductosListPage());
        }

        private async void newEntryBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntradaPage());
        }

        private async void LogsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportesPage());
        }
    }
}