using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PruebaXamarin.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? CodBarras { get; set; }
        public byte[] Img { get; set; }
        public int Cantidad { get; set; }

        
        
    }
}
