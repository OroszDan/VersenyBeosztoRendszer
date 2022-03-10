using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    class VersenyTarolo
    {
        public Verseny Fej { get; set; }

        public int VersenySzam
        {
            get
            {
                Verseny p = Fej;
                int db = 0;

                while (p != null)
                {
                    db++;
                    p = p.Kovetkezo;
                }

                return db;

            }
        }


        public void VersenyBeszurasVegere(string tartalom,int ora)
        {

            Verseny uj = new Verseny(tartalom, ora)
            {
                Kovetkezo = null
            };

            if (Fej==null)
            {
                Fej = uj;
            }
            else
            {
                Verseny p = Fej;
                while (p.Kovetkezo != null)
                {
                    p = p.Kovetkezo;
                }
                p.Kovetkezo = uj;
            }
           

        }
    }
}
