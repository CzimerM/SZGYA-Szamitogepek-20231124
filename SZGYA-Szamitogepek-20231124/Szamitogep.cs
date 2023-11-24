using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZGYA_Szamitogepek_20231124
{
    class Szamitogep
    {
        public int Id { get; set; }
        public string CPU { get; set; }
        public float CPUSebesseg { get; set; }
        public int RAM { get; set; }
        public string RAMTipus { get; set; }
        public string OS { get; set; }
        public string Gyarto { get; set; }
        public string Modell { get; set; }
        public float KijelzoMeret { get; set; }

        public float KijelzoMeretCm => this.KijelzoMeret * 2.54f;

        public Szamitogep(string sor)
        {
            string[] adatok = sor.Split('|');
            this.Id = int.Parse(adatok[0]);
            this.CPU = adatok[1];
            this.CPUSebesseg = float.Parse(adatok[2]);
            string[] ram = adatok[3].Split(' ');
            this.RAM = int.Parse(ram.First());
            this.RAMTipus = ram.Last();
            this.OS = adatok[4];
            this.Gyarto = adatok[5].Split(' ').First();
            this.Modell = adatok[5].Substring(this.Gyarto.Length + 1);
            this.KijelzoMeret = float.Parse(adatok[6]);
        }

        public override string ToString()
        {
            return $"{this.Id,2}|{this.CPU,-27}|{this.CPUSebesseg,3}|{this.RAM,-2} GB {this.RAMTipus,4}|{this.OS,-15}|{this.Gyarto,-9} {this.Modell,-25}|{this.KijelzoMeret,4}";
        }
    }
}
