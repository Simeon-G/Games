using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    public enum SpielerNummer
    {
        spieler1 = 1,
        spieler2 = 2
    }
    public class Spieler
    {
        public string SpielerName { get; set; }
        public int SpielerZahl { get; set; }
        public string SpielerZeichen { get; set; }
        public bool KünstlicheIntelligenz { get; set; }

        //Man könnte hier überlegen die Programmlogik aus dem Konstruktor in eine eigene Methode zu verlegen
        public Spieler(string spielerName, SpielerNummer Zahl)
        {
            SpielerName = spielerName;
            bool rdy = Int32.TryParse(Zahl.ToString(), out int spielerZahl);
            SpielerZahl = spielerZahl;
            if(spielerZahl == 1)
            {
                SpielerZeichen = "X";
            }
            else
            {
                SpielerZeichen = "O";
            }
            if(spielerName == "KI")
            {
                KünstlicheIntelligenz = true;
            }
            else
            {
                KünstlicheIntelligenz = false;
            }
        }
    }
}
