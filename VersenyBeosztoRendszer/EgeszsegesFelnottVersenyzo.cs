using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    abstract class EgeszsegesFelnottVersenyzo : VersenyzoTipusok
    {
        //public event NemVersenyzikTobbet NemVersenyzikTobbetMar;
        public EgeszsegesFelnottVersenyzo(string nev, Nem nem, int maxversenyoraszam)
           : base(nev, nem, maxversenyoraszam)
        {

        }

        public override int Fogyasztas(int ora) //hány liter óránként
        {
            int result = TerheltVersenyOraSzam * 5 / ora;
           
            if (result > 4)
            {
               NemVersenyzikTobbetMar?.Invoke(this);
                
            }
            return result;
        }
    }
}
