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
        public List<string> genre { get; set; } = new List<string>();
        public List<string> actors { get; set; } = new List<string>();
        public string awards { get; set; }
        public string website { get; set; }

        public string user = "Isaac ";

    }
}
