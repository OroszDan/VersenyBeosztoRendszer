using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    sealed class BajnokVersenyzo : EgeszsegesFelnottVersenyzo
    {
        public int HanyszorVoltBajnok { get; private set; }
        public BajnokVersenyzo(string nev, Nem nem, int maxversenyoraszam,int hanyszorvoltbajnok)
          : base(nev, nem, maxversenyoraszam)
        {
            this.HanyszorVoltBajnok = hanyszorvoltbajnok;
        }

        public override int Fogyasztas(int ora) //hány liter óránként
        {
            int result = TerheltVersenyOraSzam * 6 / ora;  //több vizet kér
           
            if (result > 4)
            {
                NemVersenyzikTobbetMar?.Invoke(this);
            }
            return result;
        }

        public void BajnokiCimetNyert()
        {
            HanyszorVoltBajnok++;
        }

        
    }
}
