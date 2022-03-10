using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    delegate void NemVersenyzikTobbet(IVersenyzo versenyzo);
    enum Nem
    {
        férfi,nő
    }
    abstract class VersenyzoTipusok : IVersenyzo
    {
        public  NemVersenyzikTobbet NemVersenyzikTobbetMar; 
        
        public string Nev { get ; }
        public Nem Nem { get; }
        public string VersenyzoAzonosito { get ; set ; }
        public int MaxVersenyOraSzam { get;  }
        public int TerheltVersenyOraSzam { get; set; }
        public Verseny Versenyek { get; set; }

        public VersenyzoTipusok(string nev,Nem nem,int maxversenyoraszam)
        {
            this.Nev = nev;
            this.Nem = nem;
            this.MaxVersenyOraSzam = maxversenyoraszam;
        }

       

        public void VersenyBeszurasElejere(string tartalom,int ora)
        {
            Verseny uj = new Verseny(tartalom, ora);
            uj.Kovetkezo = Versenyek;
            Versenyek = uj;
        }



        public abstract int Fogyasztas(int ora); //hány liter óránként
       

        public virtual double Teherbiras() //0 és 1 közötti kihasználtságot ad meg
        {
            return (double)TerheltVersenyOraSzam / MaxVersenyOraSzam;
        }

        public void Terheles(int ora)
        {
            TerheltVersenyOraSzam += ora;
        }

        public bool TerhelhetoMeg()
        {
            return Teherbiras() < 0.95;
        }
    }
}
