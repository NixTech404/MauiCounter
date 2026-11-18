using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaApka.Models
{
    class Counter
    {
        public string Filename { get; set; } = string.Empty;
        public string Name { get; set; } = "Licznik";
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
