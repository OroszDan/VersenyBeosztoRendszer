using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    class Verseny
    {
        public string Megnevezes { get; set; }
        public int Ora { get; set; }
        public Verseny Kovetkezo { get; set; }

        public Verseny(string megnevezes,int ora)
        {
            this.Megnevezes = megnevezes;
            this.Ora = ora;
        }

        public override bool Equals(object obj)
        {
            Verseny masik = obj as Verseny;
            return this.Megnevezes == masik.Megnevezes && this.Ora == masik.Ora;
        }


    }
}
