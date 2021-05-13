using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class Temp
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public long id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public string name { get; set; }
        public float tempMax { get; set; }
        public float tempMin { get; set; }
        public float tempAtual { get; set; }

        public DateTime date { get; set; }


        public Temp()
        {
            date = DateTime.Now;
        }

    }

}
