using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VersenyBeosztoRendszer
{
    //    brigad1   Kimi maximum 2 versenyen vehet részt, Andi viszont minimum 3-on kell résztvegyen 
    //0,Mariska,nő,32,Tour de NIK_4;Tour de Hungrie_5;Tour de Budapest_4;Budapest körbe_13; Börzsöny Downhill_6
    //1,Buldózer János, férfi,35,Tour de NIK_4;Érd körbe_7; Tour de Hungrie_5;Tour de Budapest_4;Börzsöny Downhill_6
    //2,Julcsi,nő,40,Érd Körbe_2, Tour de NIK_4; Tour de Hungrie_5;Tour de Budapest_4;Érd körbe_2; Pilis Downhill_2
    //0,Andi,nő,37,Tour de NIK_4;Tour de Balaton_8;Tour de Hungrie_10;Tour de Budapest_4;Érd körbe_2; Pilis Downhill_2
    //3,Kimi Raikonnen, férfi,14,2,Tour de NIK_4;Tour de Hungrie_10;Pilis Downhill_2

    //brigad2    
    //2,Kis Pistike, férfi,31,Börzsöny Downhill_3, Tour de NIK_4; Tour de Hungrie_5;Tour de Budapest_2;Érd körbe_7; Börzsöny Downhill_3
    //0,Nagy Pistike, férfi,14,Tour de NIK_4;Érd körbe_7; Tour de Hungrie_5;Tour de Budapest_2;Börzsöny Downhill_3
    //1,Gitta,nő,20,Tour de NIK_4;Tour de Hungrie_5;Tour de Budapest_2;Tárnok körbe_1; Pilis Downhill_1
    //3,Bud Spencer, férfi,19,1,Tour de NIK_4;Tour de Balaton_8;Tour de Hungrie_5;Tour de Budapest_2;Érd körbe_1; Pilis Downhill_1
    //0,Kis János, férfi,25,Tour de NIK_4;Tour de Hungrie_5;Pilis Downhill_1

    //brigad3   //nincs semmi különös
    //2,Elon Musk, férfi,45,Tour de NIK_4,Tour de Hungrie_5;Tour de NIK_4;Tour de Budapest_2;Börzsöny Downhill_3
    //0,Alex,férfi,30,Tour de NIK_4;Érd körbe_7; Tour de Hungrie_5;Tour de Budapest_2;Pilis Downhill_3
    //1,Csilla,nő,20,Tour de Hungrie_5;Tour de NIK_4;Tour de Budapest_2;Tárnok körbe_1; Pilis Downhill_1
    //3,Arnold Shwarzenegger, férfi,30,1,Tour de NIK_4;Tour de Balaton_8;Tour de Hungrie_5;Tour de Budapest_2;Érd körbe_1; Pilis Downhill_1
    //0,Erika,nő,40,Tour de NIK_4;Tour de Hungrie_5;Pilis Downhill_1; Tour de Balaton_8

    // brigad4   // Ádámnál nincs annyi verseny ami teljesíti a feltételt
    //0,Petra,nő,25,Tour de NIK_4;Tour de Hungrie_5;Tour de Budapest_2;Érd körbe_7; Pilis Downhill_3
    //1,Feri,férfi,30,Tour de NIK_4;Érd körbe_7; Tour de Hungrie_5;Tour de Budapest_2;Pilis Downhill_3
    //3,Ádám,férfi,35,2,Tour de NIK_4;Pilis Downhill_1
    enum VersenyzoFajtak
    {
        KozepKoruAmator,FogyatekkalRendelkezo,Kiskoru,VilagBajnok
    }

    class VersenyzoKezelo 
    {
        delegate void VersenyzoFeldolgozoFuggveny(IVersenyzo versenyzo, 
            VersenyBrigad<IVersenyzo> aktualisbrigad,int db);
        delegate void BrigadFeldolgozoFuggveny(VersenyBrigad<IVersenyzo> aktualisbrigad);
      

        private ListaElem<VersenyBrigad<IVersenyzo>> fej;

        public void Kezelo(string path)
        {

            StreamReader sr = new StreamReader(path);
          
            string ujsor;

            while (!sr.EndOfStream)
            {

                ujsor = sr.ReadLine();
                string[] b_adatok = ujsor.Split(',');
                VersenyBrigad<IVersenyzo> ujbrigad = new VersenyBrigad<IVersenyzo>(b_adatok[0]);

                ujsor = sr.ReadLine();
                while (ujsor != "" && !sr.EndOfStream)
                {
                    string ujversenyzostr = ujsor;
                    string[] v_adatok = ujversenyzostr.Split(',');

                    VersenyzoFajtak versenyzoFajtak = (VersenyzoFajtak)int.Parse(v_adatok[0]);
                    VersenyzoTipusok ujversenyzo;
                    string[] v_versenyek;

                    if (versenyzoFajtak is VersenyzoFajtak.KozepKoruAmator)
                    {
                        ujversenyzo = new KozepKoruAmator(v_adatok[1],
                      (Nem)Enum.Parse(typeof(Nem), v_adatok[2]), int.Parse(v_adatok[3]));
                        v_versenyek = v_adatok[4].Split(';');
                    }
                    else if (versenyzoFajtak is VersenyzoFajtak.FogyatekkalRendelkezo)
                    {
                        ujversenyzo = new FogyatekkalRendelkezoVersenyzo(v_adatok[1],
                      (Nem)Enum.Parse(typeof(Nem), v_adatok[2]), int.Parse(v_adatok[3]));
                        v_versenyek = v_adatok[4].Split(';');
                    }
                    else if (versenyzoFajtak is VersenyzoFajtak.Kiskoru)
                    {
                        string[] kedvencversenytomb = v_adatok[4].Split('_');
                        ujversenyzo = new KisKoruVersenyzo(v_adatok[1],
                       (Nem)Enum.Parse(typeof(Nem), v_adatok[2]), int.Parse(v_adatok[3]),
                       new Verseny(kedvencversenytomb[0], int.Parse(kedvencversenytomb[1])));
                        v_versenyek = v_adatok[5].Split(';');
                    }
                    else if (versenyzoFajtak is VersenyzoFajtak.VilagBajnok)
                    {
                        ujversenyzo = new BajnokVersenyzo(v_adatok[1],
                       (Nem)Enum.Parse(typeof(Nem), v_adatok[2]), int.Parse(v_adatok[3]), int.Parse(v_adatok[4]));
                        v_versenyek = v_adatok[5].Split(';');
                    }
                    else
                    {
                        ujversenyzo = new BajnokVersenyzo(v_adatok[1],
                      (Nem)Enum.Parse(typeof(Nem), v_adatok[2]), int.Parse(v_adatok[3]), int.Parse(v_adatok[4]));
                        v_versenyek = v_adatok[5].Split(';');
                    }



                    ujversenyzo.VersenyzoAzonosito = AzonositoGenerator(ujversenyzo.Nem);
                    ujversenyzo.NemVersenyzikTobbetMar += ujbrigad.VersenyzoTorles;

                    ElkeszultVersenyzo(ujversenyzo);

                    for (int i = 0; i < v_versenyek.Length; i++)
                    {
                        string[] verseny_adatok = v_versenyek[i].Split('_');
                        ujversenyzo.VersenyBeszurasElejere(verseny_adatok[0], int.Parse(verseny_adatok[1]));
                    }

                    ujbrigad.VersenyzoRendezettBeszurasKor(ujversenyzo);
                    ujsor = sr.ReadLine();

                }
                ElkeszultBrigad(ujbrigad.Nev);
                ujbrigad.MegszuntetettBrigad += BrigadMegSzuntetes;
                ujbrigad.MegszuntetettBrigad += VersenyzoBrigadbolTorles;
                ujbrigad.ToroltVersenyzo += ToroltVersenyzo;
                VersenyzoBrigadRendezettBeszuras(ujbrigad);
            }
            sr.Close();


            Console.ReadLine();
            try
            {
                VersenyBrigadBejaras(Optimalizalas);
                Console.ReadLine();
                VersenyBrigadBejaras(FogyasztasMeres);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               
            }
            



        }

        private void BrigadMegSzuntetes(string nev)
        {
            Console.WriteLine(nev+" nevű brigád meg lett szüntetve");
        }

        private void ElkeszultBrigad(string nev)
        {
            Console.WriteLine("Új brigád hozzáadva, neve: "+nev);
            Console.WriteLine();
        }

        private void ElkeszultVersenyzo(VersenyzoTipusok versenyzo)
        {
            Console.WriteLine("Új versenyző hozzáadva, Neve: "+versenyzo.Nev+
                ", Neme: "+versenyzo.Nem+", Azonosítója:"+versenyzo.VersenyzoAzonosito);
        }

        private void ToroltVersenyzo(string nev)
        {
            Console.WriteLine(nev+" nevű versenyző törölve");
           
        }

        private void BoldogVersenyzo(IVersenyzo aktualisversenyzo,Verseny aktualisverseny)
        {
            if (aktualisversenyzo is KisKoruVersenyzo)
            {
                if ((aktualisversenyzo as KisKoruVersenyzo).KedvencVerseny.Equals(aktualisverseny))
                {
                    Console.WriteLine("Kicsi " + aktualisversenyzo.Nev + " nagyon örül hogy indulhat a kedvenc versenyén, ami a: "+aktualisverseny.Megnevezes);
                    Console.WriteLine();
                }
            }

           
        }

        private void VersenyzoListaBejaras(VersenyBrigad<IVersenyzo> brigad,VersenyzoFeldolgozoFuggveny fuggveny) 
        {
            //fel kell készíteni rá hogy bejárás során töröljük a listát
            ListaElem<IVersenyzo> p = brigad.Fej;
            int db = 0;
            if (brigad.Fej != null)
            {
                fuggveny?.Invoke(p.Tartalom,brigad,db);
                //Console.WriteLine(p.Tartalom.Nev);
                while (int.Parse(p.Tartalom.VersenyzoAzonosito.Substring(2, 3))   //mert növekvő sorrendben van rendezve
                < int.Parse(p.Kovetkezo.Tartalom.VersenyzoAzonosito.Substring(2, 3)))
                {
                    db++;
                    p = p.Kovetkezo;
                    //bejarasIntezo?.Invoke(p.Tartalom);
                    fuggveny?.Invoke(p.Tartalom,brigad,db);
                    //Console.WriteLine(p.Tartalom.Nev);
                }
               
            }

        }

        private void EredmenyKiiro(VersenyBrigad<IVersenyzo> brigad)
        {
            if (brigad.VanMegoldas == true)
            {
                Console.WriteLine(brigad.Nev + "-ben van optimális megoldás");
                VersenyzoListaBejaras(brigad, EredmenyKiiroKieg);
                Console.WriteLine();
            }
            else if(brigad.NincsMegoldas==true)
            {
                Console.WriteLine(brigad.Nev + "-ben sajnos nem létezik optimális beosztás");
                  Console.WriteLine();
            }


           


        }

        private void EredmenyKiiroKieg(IVersenyzo versenyzo,VersenyBrigad<IVersenyzo> brigad,int db)
        {
            if (versenyzo is BajnokVersenyzo)
            {
                (versenyzo as BajnokVersenyzo).BajnokiCimetNyert();
                Console.WriteLine(versenyzo.Nev+" már megint bajnok lett, most már "+
                    (versenyzo as BajnokVersenyzo).HanyszorVoltBajnok+" bajnoki címe van");
            }
            Console.WriteLine(versenyzo.Nev + " " + brigad.OptimalisBeosztas[db].VersenySzam + "db versenyre lett beosztva, " +
                       Math.Round((versenyzo.Teherbiras() * 100),2) + "%-osan van kihasználva a teherbírása");
        }

        

        private void Optimalizalas(VersenyBrigad<IVersenyzo> aktualisbrigad)
        {
            aktualisbrigad.OptimalisBeosztas = new VersenyTarolo[aktualisbrigad.VersenyzoSzam];
            aktualisbrigad.VanMegoldas = false;
            aktualisbrigad.NincsMegoldas = false;


            BTS(aktualisbrigad, -2, 0);
           
            
                EredmenyKiiro(aktualisbrigad);
            
            ;
        }



        

        private void FogyasztasMeres(VersenyBrigad<IVersenyzo> aktualisbrigad)
        {
            Console.WriteLine();
            ;
            if (aktualisbrigad.VanMegoldas==true)
            {
                VersenyzoListaBejaras(aktualisbrigad, FogyasztasMeresKieg);

            }

            ;
        }

        private void FogyasztasMeresKieg(IVersenyzo versenyzo,VersenyBrigad<IVersenyzo> brigad,int db)
        {
            Console.WriteLine(db+1 +". versenyző: "+versenyzo.Nev + " fogyasztása: " + versenyzo.Fogyasztas(6));
        }

        private int VersenySzam(IVersenyzo versenyzo)
        {
            
            Verseny p = versenyzo.Versenyek;
            int db = 0;

            while (p != null)
            {
                db++;
                p = p.Kovetkezo;
            }

            return db;
        }

        private Verseny VersenyKereses(IVersenyzo versenyzo,int hanyadik)
        {
            Verseny p = versenyzo.Versenyek;
            int db = 1;
            while (p != null && db < hanyadik)
            {
                db++;
                p = p.Kovetkezo;
            }
            if (p != null && db == hanyadik)
            {
                return p;
            }
            else
            {
                throw new ArgumentException("Nem található a " + db + "-ik elem");
            }
        }



        public void BTS(VersenyBrigad<IVersenyzo> brigad, int minhanyversszuks, int szint)
        {
            IVersenyzo aktualisversenyzo = brigad.VersenyzoKereses(szint + 1); // a listában 1-től indexelünk
            if (brigad.OptimalisBeosztas[szint] == null)
            {
                brigad.OptimalisBeosztas[szint] = new VersenyTarolo();

            }
            int i = brigad.OptimalisBeosztas[szint].VersenySzam;
            int db = 0;

            while (!brigad.VanMegoldas && !brigad.NincsMegoldas && i < VersenySzam(aktualisversenyzo))
            {
                Verseny aktualisverseny;

                minhanyversszuks++;
                while (i < VersenySzam(aktualisversenyzo) && (aktualisversenyzo.Teherbiras() <= 0.15))
                //addig megyünk amíg el nem érjük a minimális 
                //követelményt
                {


                    i++;  
                    aktualisverseny = VersenyKereses(aktualisversenyzo, i);
                    aktualisversenyzo.Terheles(aktualisverseny.Ora);
                   

                    if (aktualisversenyzo.Teherbiras() < 0.95)  //figyeljünk hogy túl lesz-e terhelve
                    {
                        brigad.OptimalisBeosztas[szint].VersenyBeszurasVegere(aktualisverseny.Megnevezes, aktualisverseny.Ora);
                        minhanyversszuks++;
                        BoldogVersenyzo(aktualisversenyzo, aktualisverseny);


                    }
                    else
                    {
                        brigad.NincsMegoldas = true;
                        //baj van
                    }


                }

                if (i < VersenySzam(aktualisversenyzo) && (db!=0) &&    //első eset során nem léphet be mert akkor akár 2vel is eltérne a minimumtól
                         aktualisversenyzo.TerhelhetoMeg())   //megengedjük hogy a minimumtól eggyel több legyen a vállalt versenyek száma
                {
                    i++;
                    aktualisverseny = VersenyKereses(aktualisversenyzo, i);
                    aktualisversenyzo.Terheles(aktualisverseny.Ora);

                    if (aktualisversenyzo.Teherbiras() < 0.95)  //figyeljünk hogy túl lesz-e terhelve
                    {
                        brigad.OptimalisBeosztas[szint].VersenyBeszurasVegere(aktualisverseny.Megnevezes, aktualisverseny.Ora);
                        BoldogVersenyzo(aktualisversenyzo, aktualisverseny);
                    }
                    else
                    {
                        //mivel mindez nem kötelező ezért visszavonhatjuk
                        aktualisversenyzo.Terheles(aktualisverseny.Ora * (-1));
                        i--;
                    }
                }

                if (i <= VersenySzam(aktualisversenyzo) && !brigad.NincsMegoldas
                    && aktualisversenyzo.Teherbiras() > 0.15 && (i == minhanyversszuks || i == minhanyversszuks + 1))
                {

                    if (szint == brigad.VersenyzoSzam - 1)
                    {
                        brigad.VanMegoldas = true;
                    }
                    else
                    {
                        BTSkieg(brigad, minhanyversszuks, szint + 1);
                    }
                }
                else 
                {
                    brigad.NincsMegoldas = true;
                }
                db++;
            }
        }

        private void BTSkieg(VersenyBrigad<IVersenyzo> brigad, int minhanyversszuks, int szint)
        {
            IVersenyzo aktualisversenyzo = brigad.VersenyzoKereses(szint + 1);
            if (brigad.OptimalisBeosztas[szint] == null)
            {
                brigad.OptimalisBeosztas[szint] = new VersenyTarolo();

            }
            int i = brigad.OptimalisBeosztas[szint].VersenySzam;

            Verseny aktualisverseny;

            while (i < VersenySzam(aktualisversenyzo) &&
                    (aktualisversenyzo.Teherbiras() <= 0.15 || i < minhanyversszuks))  //addig megyünk amíg el nem érjük a minimális 
                                                                                       //követelményeket
            {
                i++;
                aktualisverseny = VersenyKereses(aktualisversenyzo, i);
                aktualisversenyzo.Terheles(aktualisverseny.Ora);


                if (aktualisversenyzo.Teherbiras() < 0.95)  //figyeljünk hogy túl lesz-e terhelve
                {
                    brigad.OptimalisBeosztas[szint].VersenyBeszurasVegere(aktualisverseny.Megnevezes, aktualisverseny.Ora);
                        BoldogVersenyzo(aktualisversenyzo, aktualisverseny);


                }
                else
                {
                    brigad.NincsMegoldas = true;
                    aktualisversenyzo.Terheles(aktualisverseny.Ora*-1);
                    //baj van, próbáljuk menteni a menthetőt
                }
            }

            if (i < VersenySzam(aktualisversenyzo) && !brigad.NincsMegoldas  &&
                         i == minhanyversszuks && aktualisversenyzo.TerhelhetoMeg())   //megengedjük hogy a minimumtól eggyel több legyen a vállalt versenyek száma
            {
                i++;
                aktualisverseny = VersenyKereses(aktualisversenyzo, i);
                aktualisversenyzo.Terheles(aktualisverseny.Ora);

                if (aktualisversenyzo.Teherbiras() < 0.95)  //figyeljünk hogy túl lesz-e terhelve
                {
                    brigad.OptimalisBeosztas[szint].VersenyBeszurasVegere(aktualisverseny.Megnevezes, aktualisverseny.Ora);
                    BoldogVersenyzo(aktualisversenyzo, aktualisverseny);

                }
                else
                {
                    //mivel mindez nem kötelező ezért visszavonhatjuk
                    aktualisversenyzo.Terheles(aktualisverseny.Ora * (-1));
                    i--;
                }
            }

            if (i >= VersenySzam(aktualisversenyzo) && (aktualisversenyzo.Teherbiras() <= 0.15 || i < minhanyversszuks))
            {
                brigad.NincsMegoldas = true;
                //problem van
            }

            //Console.WriteLine(minhanyversszuks);

            if (i <= VersenySzam(aktualisversenyzo) && !brigad.NincsMegoldas
                && aktualisversenyzo.Teherbiras() > 0.15 && (i == minhanyversszuks || i == minhanyversszuks + 1))
            {

                if (szint == brigad.VersenyzoSzam - 1)
                {
                    brigad.VanMegoldas = true;
                }
                else
                {
                    BTSkieg(brigad, minhanyversszuks, szint + 1);
                }
            }

                ;

        }


        private void VersenyBrigadBejaras(BrigadFeldolgozoFuggveny fuggveny)
        {
            ListaElem<VersenyBrigad<IVersenyzo>> p = fej;

            while (p != null)
            {
                fuggveny?.Invoke(p.Tartalom);
                p = p.Kovetkezo;
            }
           

        }

        
        public void VersenyzoBrigadRendezettBeszuras(VersenyBrigad<IVersenyzo> brigad)
        {
            ListaElem<VersenyBrigad<IVersenyzo>> uj = new ListaElem<VersenyBrigad<IVersenyzo>>
            {
                Tartalom = brigad
            };
            ListaElem<VersenyBrigad<IVersenyzo>> p = fej;
            ListaElem<VersenyBrigad<IVersenyzo>> e = null;
          

            //string teszt = p.VersenyzoAzonosito.Substring(3, 3);

            while ((p != null) && (int.Parse(p.Tartalom.Fej.Tartalom.VersenyzoAzonosito.Substring(2, 3))
                < int.Parse(uj.Tartalom.Fej.Tartalom.VersenyzoAzonosito.Substring(2, 3))) /*&& !p.Equals(uj)*/)
            {
                e = p;
                p = p.Kovetkezo;

            }
            
            if (e == null)
            {
                uj.Kovetkezo = fej;
                fej = uj;

            }
            else
            {
                uj.Kovetkezo = p;
                e.Kovetkezo = uj;
            }
        }

        public void VersenyzoBrigadbolTorles(string torlendobrigadnev)
        {

            ListaElem<VersenyBrigad<IVersenyzo>> p = fej;
            ListaElem<VersenyBrigad<IVersenyzo>> e = null;


            while (p != null && p.Tartalom.Nev != torlendobrigadnev)
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (p != null)
            {
                //törlés, mert megvan
                if (e == null)
                {
                    //első elemet kell törölni
                    fej = p.Kovetkezo;
                }
                else
                {
                    //valahanyadik elemet kell törölni
                    e.Kovetkezo = p.Kovetkezo;
                }
            }
            else
            {
                //kivételt dobunk, mert nincs ilyen elem a listában
                throw new Exception();
            }


        }

        private string AzonositoGenerator(Nem nem)
        {
            string id = null;

            if (nem == 0)
            {
                id += "F";
            }
            else
            {
                id += "N";

            }

            id += "-";

            for (int i = 0; i < 3; i++)
            {
                id += RandomGenerator.r.Next(0, 10);
            }

            for (int i = 0; i < 2; i++)
            {
                id += (char)RandomGenerator.r.Next('A', 'Z');

            }

            return id;
        }


    } 

}
