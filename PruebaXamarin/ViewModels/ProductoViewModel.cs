﻿using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PruebaXamarin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace PruebaXamarin.ViewModels
{
    public class ProductoViewModel : BaseViewModel
    {
        #region Attributes
        private int _Id;
        private string _Nombre;
        private string _Descripcion;
        private int _CodBarras;
        private byte[] _Img;
        private int _Cantidad;
        private ImageSource _Imagen;
        private Productos _selectedProducto;
        #endregion


        #region Propiedades
        public List<Productos> _Productos { get; set; } = new List<Productos>();
        public int Aumento { get; set; }
        public Productos SelectedProducto 
        {
            get { return this._selectedProducto; ; }
            set { SetValue(ref this._selectedProducto, value); }
        }

        public int Id
        {
            get { return this._Id; }
            set { SetValue(ref this._Id, value); }
        }
        public string Nombre
        {
            get { return this._Nombre; }
            set { SetValue(ref this._Nombre, value); }
        }
        public string Descripcion
        {
            get { return this._Descripcion; }
            set { SetValue(ref this._Descripcion, value); }
        }
        public int CodBarras
        {
            get { return this._CodBarras; }
            set { SetValue(ref this._CodBarras, value); }
        }
        public byte[] Img
        {
            get { return this._Img; }
            set { SetValue(ref this._Img, value); }
        }
        public int Cantidad
        {
            get { return this._Cantidad; }
            set { SetValue(ref this._Cantidad, value); }
        }
        public ImageSource Imagen
        {
            get { return this._Imagen; }
            set { SetValue(ref this._Imagen, value); }
        }
        #endregion

        

        #region Methods

        private async void OnClickPhoto()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full
            });

            if (file == null)
                return;

            Imagen = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            Img = ReadToEnd(file.GetStream());

        }

        private async void SaveProducto()
        {
            var repositorio = new Repositorio();
            var nuevoProducto = new Productos();
            nuevoProducto.Nombre = Nombre;
            nuevoProducto.Descripcion = Descripcion;
            nuevoProducto.Cantidad = Cantidad;
            nuevoProducto.CodBarras = CodBarras;
            nuevoProducto.Img = Img;

            var result = await repositorio.CreateProductoAsync(nuevoProducto);
            if (result == HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Info",$"Producto Guardado","Aceptar");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Aviso","Fallo al guardar, revise que todos los campos esten llenos y su conexion a internet","Aceptar");
            }
        }

        private async void UpdateProducto()
        {
            var repositorio = new Repositorio();
            var producto = new Productos()
            {
                Id = Id,
                Nombre = Nombre,
                Descripcion = Descripcion,
                Cantidad = Cantidad,
                CodBarras = CodBarras,
                Img = Img
            };
            var result = await repositorio.UpdateProducto(Id,producto);
            if (result == HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Info", $"Producto Actualizado", "Aceptar");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Fallo al guardar, revise que todos los campos esten llenos y su conexion a internet", "Aceptar");
            }
        }

        private async void DeleteProducto()
        {
            var repositorio = new Repositorio();
            var result = await repositorio.DeleteProductoAsync(Id);
            if (result == HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Info", $"Producto Eliminado", "Aceptar");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Fallo al eliminar, revise su conexion a internet", "Aceptar");
            }
        }

        public static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        private void AumentoCantidad()
        {
           Cantidad = Cantidad + 1;
        }

        private void DisminuyeCantidad()
        {
            Cantidad = Cantidad - 1;
        }
        #endregion
        private void AumentarCantidad()
        {
            
            Cantidad = SelectedProducto.Cantidad + Aumento;
        //    var repositorio = new Repositorio();
        //    var producto = new Productos()
        //    {
        //        Id = SelectedProducto.Id,
        //        Nombre = SelectedProducto.Nombre,
        //        Descripcion = SelectedProducto.Descripcion,
        //        CodBarras = SelectedProducto.CodBarras,
        //        Cantidad = SelectedProducto.Cantidad,
        //        Img = SelectedProducto.Img
        //    };
        }


        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(SaveProducto);
            }
        }
        public ICommand AumentoBtnCommand
        {
            get
            {
                return new RelayCommand(AumentoCantidad);
            }
        }
        public ICommand DisminuyeBtnCommand
        {
            get
            {
                return new RelayCommand(DisminuyeCantidad);
            }
        }
        public ICommand SelectImageCommand
        {
            get
            {
                return new RelayCommand(OnClickPhoto);
            }
        }
        public ICommand UpdateBtnCommand
        {
            get
            {
                return new RelayCommand(UpdateProducto);
            }
        }
        public ICommand DeleteBtnCommand
        {
            get
            {
                return new RelayCommand(DeleteProducto);
            }
        }
        public ICommand AumentarBtnCommand
        {
            get
            {
                return new RelayCommand(AumentarCantidad);
            }
        }
        #endregion

    }
}