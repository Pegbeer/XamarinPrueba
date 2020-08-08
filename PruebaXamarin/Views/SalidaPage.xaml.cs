using PruebaXamarin.Models;
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
    public partial class SalidaPage : ContentPage
    {
        public SalidaPage()
        {
            InitializeComponent();
            BindingContext = new ProductoViewModel();
            Lista.Refreshing += Lista_Refreshing;
            VolverBtn.Clicked += VolverBtn_Clicked;
        }

        private async void VolverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Lista_Refreshing(object sender, EventArgs e)
        {
            MostrarProductos();
            Lista.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MostrarProductos();
        }
        private async void MostrarProductos()
        {
            indicador.IsVisible = true;
            indicador.IsRunning = true;
            Lista.IsVisible = false;
            Repositorio repositorio = new Repositorio();
            Lista.ItemsSource = await repositorio.GetProductosAsync();
            Lista.IsVisible = true;
            indicador.IsRunning = false;
            indicador.IsVisible = false;
        }
    }
    
}