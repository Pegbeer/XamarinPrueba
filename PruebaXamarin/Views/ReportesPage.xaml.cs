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
    public partial class ReportesPage : ContentPage
    {
        public ReportesPage()
        {
            BindingContext = new ProductoViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MostrarRegistros();
        }

        private async void MostrarRegistros()
        {
            indicador.IsVisible = true;
            indicador.IsRunning = true;
            Lista.IsVisible = false;
            Repositorio repositorio = new Repositorio();
            Lista.ItemsSource = await repositorio.GetLogsAsync();
            Lista.IsVisible = true;
            indicador.IsRunning = false;
            indicador.IsVisible = false;
        }
    }
}