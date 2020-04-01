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

        public Spieler(string spielerName, int spielerZahl)
        {
            SpielerName = spielerName;
            SpielerZahl = spielerZahl;
            ZeichenFestlegen(SpielerZahl);
            KI();
        }
        private void ZeichenFestlegen(int spielerZahl)
        {
            if (spielerZahl == 1)
            {
                SpielerZeichen = "X";
            }
            else
            {
                SpielerZeichen = "O";
            }
        }
        private void KI()
        {
            if (SpielerName == "KI")
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
