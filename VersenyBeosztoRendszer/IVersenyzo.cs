using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    interface IVersenyzo
    {
        string Nev { get; }
        string VersenyzoAzonosito { get; set; }
        int Fogyasztas(int ora);  //órában
        void Terheles(int ora); //órában
        double Teherbiras();  //0-1 között
        bool TerhelhetoMeg();
        Verseny Versenyek { get; set; }
        
    }
}
