using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamenProgreso3.Models
{
    [Table("IPpeliculas")]
    public class BDPeliculas
    {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string title { get; set; }
        public string[] genre { get; set; }
        public string[] actors { get; set; }
        public string awards { get; set; }
        public string website { get; set; }

    }
}
