using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    sealed class KisKoruVersenyzo : VersenyzoTipusok
    {
        public Verseny KedvencVerseny { get; set; }
        public KisKoruVersenyzo(string nev, Nem nem ,int maxversenyoraszam,Verseny kedvencverseny)
            :base(nev, nem, maxversenyoraszam/3)
        {
            KedvencVerseny = kedvencverseny;
            //az előre megadott versenyóraszám harmadát tudja teljesíteni
        }

        

        public override int Fogyasztas(int ora)  //kevesebb vizet kérnek
        {
            int result = TerheltVersenyOraSzam *3 / ora;
            
            if (result > 4)
            {
                NemVersenyzikTobbetMar?.Invoke(this);
            }
            return result;
          
        }
    }
}
