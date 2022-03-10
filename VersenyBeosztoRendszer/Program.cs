using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyBeosztoRendszer
{
    class Program
    {
        static void Main(string[] args)
        {
            VersenyzoKezelo kezelo = new VersenyzoKezelo();
            //kezelo.VersenyzoLetrehozo();
            kezelo.Kezelo("data1.txt");
            
            
            Console.ReadLine();
        }
    }
}
