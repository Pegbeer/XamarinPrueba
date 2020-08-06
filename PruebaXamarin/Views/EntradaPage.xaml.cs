﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaXamarin.Models;
using PruebaXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntradaPage : ContentPage
    {
        private Productos _productos;
        public EntradaPage()
        {
            InitializeComponent();
            BindingContext = new ProductoViewModel();
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