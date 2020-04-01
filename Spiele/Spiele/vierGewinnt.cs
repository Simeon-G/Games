using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    class VierGewinnt : IGame
    {
        public string SpielName { get; set; }
        public string[,] Board { get; set; }
        public Spielfeld Feld { get; set; }
        public Spieler Spieler1 { get; set; }
        public Spieler Spieler2 { get; set; }

        private Spieler _derzeitigerSpieler;
        private int _derzeitigerSpielerZahl = 1;
        private string _fehlermeldung;
        private int _anzahlSpielzüge = 0;
        public VierGewinnt(string spielName, string[,] board, Spielfeld feld, Spieler spieler1, Spieler spieler2)
        {
            SpielName = spielName;
            Board = board;
            Feld = feld;
            Spieler1 = spieler1;
            Spieler2 = spieler2;
        }
        public void Hauptprogramm(IGame vierGewinnt)
        {
            bool gewonnen = false;
            bool gleichstand = false;

            if (_derzeitigerSpielerZahl == Spieler1.SpielerZahl)
            {
                _derzeitigerSpieler = Spieler1;
                _derzeitigerSpielerZahl = 2;
            }
            else
            {
                _derzeitigerSpieler = Spieler2;
                _derzeitigerSpielerZahl = 1;
            }
            Console.Clear();
            Console.WriteLine("*" + vierGewinnt.SpielName + "*");
            Spielfeld.Render(Board, Feld);
            Console.Write(_fehlermeldung + "\n");
            _fehlermeldung = "";
            Make_Move(Get_Move());
            if(_fehlermeldung == "")
            {
                gewonnen = Spielfeld.Gewonnen(4, Board, Feld, _anzahlSpielzüge, _derzeitigerSpieler);
                gleichstand = Spielfeld.Gleichstand(Feld, Board);
                if (gewonnen || gleichstand)
                {
                    Spielfeld.Ende(gewonnen, Board, Feld, vierGewinnt, _derzeitigerSpieler);
                    Board = new string[Feld.Boardhoehe, Feld.Boardlaenge];
                    gewonnen = false;
                    _derzeitigerSpielerZahl = 1;
                    _derzeitigerSpieler = null;
                    _anzahlSpielzüge = 0;
                    _fehlermeldung = "";
                }
            }
            Console.Clear();
            Hauptprogramm(vierGewinnt);
        }

        public void Make_Move(int[] koordinate)
        {
            bool ok = false;
            if (_fehlermeldung == "")
            {
                for (int i = Feld.Boardhoehe - 1; i >= 0; i--)
                {
                    if (Board[i, koordinate[0]] == " ")
                    {
                        _anzahlSpielzüge++;

                        Board[i, koordinate[0]] = _derzeitigerSpieler.SpielerZeichen;
                        ok = true;
                        i = 0;
                    }
                }
                if (ok)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    _fehlermeldung = "In dieser Reihe ist kein Platz mehr.";
                }
            }
        }

        public int[] Get_Move()
        {
            int[] koordinate = new int[1];
            bool Koord = false;

            if(_derzeitigerSpieler.KünstlicheIntelligenz)
            {
                    Random zufallsZahl = new Random();
                    koordinate[0] = zufallsZahl.Next(-1, Feld.Boardlaenge);
            }
            else
            {
                Console.WriteLine("Gib bitte die Reihe an.");
                Koord = Int32.TryParse(Console.ReadLine(), out koordinate[0]);

                if (Koord == false || koordinate[0] < 0 || koordinate[0] > Feld.Boardlaenge - 1)
                {
                    _fehlermeldung = "Es muss eine Zahl zwischen 0 und " + (Feld.Boardlaenge - 1) + " eingegeben werden. \n";
                }
            }
            return koordinate;
        }
    }
}
