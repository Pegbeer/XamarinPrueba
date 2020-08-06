using PruebaXamarin.Models;
using PruebaXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductosListPage : ContentPage
    {
        public ProductosListPage()
        {
            InitializeComponent();
            BindingContext = new ProductoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MostrarProductos();
        }

        protected override bool OnBackButtonPressed()
        {
            MostrarProductos();
            return base.OnBackButtonPressed();
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

        private void Add_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new NewProductPage());
        }

        private async void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Item = e.Item as Productos;
            if (Item != null)
            {
                await Navigation.PushAsync(new EditProductoPage(Item) {});
            }
        }
    }
}