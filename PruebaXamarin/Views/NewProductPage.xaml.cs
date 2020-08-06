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
    public partial class NewProductPage : ContentPage
    {
        public NewProductPage()
        {
            InitializeComponent();
            BindingContext = new ProductoViewModel();
        }

        private void CancelarBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}