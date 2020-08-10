using Java.Sql;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaXamarin.Models
{
    [Table("T_Logs")]
    public class Logs
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public DateTime Modificacion { get; set; }
        public string Texto { get; set; }
    }
}
