using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    


    delegate void MegszuntetettObjektum(string torlendonev);
    delegate void BejarasIntezo<T>(T tartalom);
    public class ListaElem<T>
    {
        public T Tartalom { get; set; }

        public ListaElem<T> Kovetkezo { get; set; }
    }
    class VersenyBrigad<T>  where T : IVersenyzo 
    {
        public event MegszuntetettObjektum MegszuntetettBrigad;
        public event MegszuntetettObjektum ToroltVersenyzo;

        public VersenyTarolo[] OptimalisBeosztas { get; set; }
        public bool VanMegoldas { get; set; }
        public bool NincsMegoldas { get; set; }
        public ListaElem<T> Fej { get; private set; }  //
        //public VersenyBrigad<T> Kovetkezo { get; set; }
        public string Nev { get; set; }

        public int VersenyzoSzam
        {
            get
            {
                ListaElem<T> p = Fej;
                int db = 0;
                if (Fej == null)
                {
                    return db;
                }
                else
                {
                    do
                    {
                        db++;
                        p = p.Kovetkezo;

                    } while (p != Fej);

                    return db;
                }
               
            }
        }

        public VersenyBrigad(string nev)
        {
            this.Nev = nev;
            
           
        }

         

        public T VersenyzoKereses(int hanyadik)
        {
            ListaElem<T> p = Fej;  //1-től indexelünk
            int db = 1;

            if (hanyadik==1)
            {
                return Fej.Tartalom;
            }
            else
            {
                db++;
                p = p.Kovetkezo;
                while (p != Fej && db < hanyadik)
                {
                    db++;
                    p = p.Kovetkezo;
                }
                if (db == hanyadik)
                {
                    return p.Tartalom;
                }
                else
                {
                    throw new ArgumentException("Nem található a " + db + "-ik elem");
                }
            }
            
        }

        public void VersenyzoRendezettBeszurasKor(T versenyzo)
        {
            ListaElem<T> uj = new ListaElem<T>
            {
                Tartalom = versenyzo
            };
            ListaElem<T> p = Fej;
            ListaElem<T> e = null;

            if (p == null)  //kezdetben a Fej null, ezért a kivételes esetet le kell kezelni, és itt 
            {               //megtörténik a kör létrehozása
                Fej = uj;
                uj.Kovetkezo = Fej;

            }
            else if (int.Parse(Fej.Tartalom.VersenyzoAzonosito.Substring(2, 3))
                > int.Parse(uj.Tartalom.VersenyzoAzonosito.Substring(2, 3)))     //ha a fejnél, azaz a legkisebb elemnél is kisebb az elem
                                                                        //akkor szükség van a lista utolsó elemének referenciájára
            {
                do
                {
                    e = p;
                    p = p.Kovetkezo;

                } while (p != Fej);

                uj.Kovetkezo = p;
                e.Kovetkezo = uj;
                Fej = uj;

            }
            else    //ellenkező esetben addig megyünk amíg nála nagyobb elemet nem találunk
            {
                do
                {
                    e = p;
                    p = p.Kovetkezo;

                } while ( (int.Parse(p.Tartalom.VersenyzoAzonosito.Substring(2, 3))
                < int.Parse(uj.Tartalom.VersenyzoAzonosito.Substring(2, 3)))&& p != Fej /*&& !p.Equals(uj)*/);

                uj.Kovetkezo = p;
                e.Kovetkezo = uj;


            }
            
            
        }

        

        public void VersenyzoTorles(T torlendo)
        {
            ListaElem<T> e = null;
            ListaElem<T> p = Fej;
            if (Fej!=null)
            {
                do
                {
                    e = p;
                    p = p.Kovetkezo;

                } while (p != Fej && !p.Tartalom.Equals(torlendo));

                if (p.Tartalom.Equals(torlendo))
                {
                    if (p != p.Kovetkezo)
                    {
                        e.Kovetkezo = p.Kovetkezo;
                        ToroltVersenyzo?.Invoke(p.Tartalom.Nev);
                        if (p==Fej)  //ha pont az első elemet akarjuk törölni
                        {
                            Fej = p.Kovetkezo;
                        }
                    }
                    else  //ilyenkor már csak egy elem található a listában ezért azt meg kell szüntetni
                    {
                        Fej = null;
                       // Console.WriteLine(p.Tartalom.Nev);
                        ToroltVersenyzo?.Invoke(p.Tartalom.Nev);

                        MegszuntetettBrigad?.Invoke(this.Nev);
                    }

                }
                else
                {
                    throw new Exception("Nincs ilyen versenyző");

                }
            }
            else
            {
                throw new Exception("Ez a lista már nem létezik ");
            }
            

        }

        
    }
}
