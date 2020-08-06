﻿using PruebaXamarin.ViewModels;
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
        }

        private async void productsListBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductosListPage());
        }

        private void newEntryBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EntradaPage());
        }
    }
}