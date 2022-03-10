using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    sealed class FogyatekkalRendelkezoVersenyzo : VersenyzoTipusok
    {
        public FogyatekkalRendelkezoVersenyzo(string nev,Nem nem ,int maxversenyoraszam)
            :base(nev,nem,maxversenyoraszam/2)
        {
            //az előre magadott versenyóraszám felét tudja teljesíteni
        }

        public override int Fogyasztas(int ora) //hány liter óránként
        {
            int result = TerheltVersenyOraSzam * 4 / ora;
            
            if (result > 4)
            {
                NemVersenyzikTobbetMar?.Invoke(this);
            }
            return result;
        }
    }
}
