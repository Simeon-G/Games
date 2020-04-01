using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    class Hauptklasse
    {
        //Mein Eindruck auf die Schnelle war, dass du deine Spieler Objekte noch gar nicht wirklich nutzt.
        //Du könntest sie z.B. den Spielen mitgeben, dann kann im Spiel direkt auf die Spielfigur oder der Name zugegriffen werden (z.B. "Glückwunsch Simeon, du hast gewonnen")
        public static void Main()
        {
            SpielerNummer spieler1 = SpielerNummer.spieler1;
            SpielerNummer spieler2 = SpielerNummer.spieler2;
            Console.Clear();
            Console.WriteLine("Gib KI als Namen ein, um den Computer spielen zu lassen.");
            //Enums werden nicht als Zahl sondern als string ausgegeben |Ausgabe: spieler1 statt 1
            Spieler Spieler1 = new Spieler(Spielfeld.AbfrageSpielernamen(spieler1), spieler1);
            Spieler Spieler2 = new Spieler(Spielfeld.AbfrageSpielernamen(spieler2), spieler2);
            IGame aktuellesSpiel = Spielfeld.Spielwahl(Spieler1, Spieler2);
            aktuellesSpiel.Hauptprogramm(aktuellesSpiel);
        }
    }
}

