using PruebaXamarin.Models;
using PruebaXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductoPage : ContentPage
    {
        private Productos _producto;
        public EditProductoPage(Productos producto)
        {
            this._producto = producto;
            InitializeComponent();
            BindingContext = new ProductoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            var Imagen = ImageSource.FromStream(() =>
            {
                var stream = new MemoryStream(this._producto.Img);
                return stream;
            });
            idtxt.Text = String.Empty + this._producto.Id;
            nombretxt.Text = this._producto.Nombre;
            descripciontxt.Text = this._producto.Descripcion;
            cantidadtxt.Text = this._producto.Cantidad.ToString();
            codbarrastxt.Text = this._producto.CodBarras.ToString();
            circleImg.Source = Imagen;
        }

        private void CancelarBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}