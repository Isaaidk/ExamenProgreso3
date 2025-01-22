using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProgreso3.Models
{
    public class Peliculas
    {


        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public string actors { get; set; }
        public string awards { get; set; }
        public string website { get; set; }

    }
}
