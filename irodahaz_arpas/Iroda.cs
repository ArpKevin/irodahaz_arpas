using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace irodahaz_arpas
{
    internal class Iroda
    {
        public int Emelet { get; set; }
        public string Kod { get; set; }
        public int Kezdet { get; set; }
        public List<int> IrodaLetszamok { get; set; }

        public Iroda(string sor, int emeletIndex)
        {
            var d = sor.Split(" ");
            Emelet = emeletIndex;
            Kod = d[0];
            Kezdet = int.Parse(d[1]);
            IrodaLetszamok = new List<int>();
            for (int i = 2; i < 14; i++) IrodaLetszamok.Add(int.Parse(d[i]));
        }

        public override string ToString()
        {
            return $"{Emelet, -6} | {Kod, -13} | {Kezdet, -10} | {string.Join(", " ,IrodaLetszamok)}";
        }

    }


}