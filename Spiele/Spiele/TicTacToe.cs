using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spiele
{
    class TicTacToe : IGame
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
        public TicTacToe(string spielName, string[,] board, Spielfeld feld, Spieler spieler1, Spieler spieler2)
        {
            SpielName = spielName;
            Board = board;
            Feld = feld;
            Spieler1 = spieler1;
            Spieler2 = spieler2;
        }
        public void Hauptprogramm(IGame ticTacToe)
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
            Console.WriteLine("*" + ticTacToe.SpielName + "*");
            Spielfeld.Render(Board, Feld);
            Console.Write(_fehlermeldung);
            _fehlermeldung = "";
            Make_Move(Get_Move());

            if (_fehlermeldung == "")
            {
                gewonnen = Spielfeld.Gewonnen(3, Board, Feld, _anzahlSpielzüge, _derzeitigerSpieler);
                gleichstand = Spielfeld.Gleichstand(Feld, Board);
                if (gewonnen || gleichstand)
                {
                    Spielfeld.Ende(gewonnen, Board, Feld, ticTacToe, _derzeitigerSpieler);
                    Board = new string[Feld.Boardhoehe,Feld.Boardlaenge];
                    gewonnen = false;
                    _derzeitigerSpielerZahl = 1;
                    _derzeitigerSpieler = null;
                    _anzahlSpielzüge = 0;
                    _fehlermeldung = "";
                }
            }
            Console.Clear();
            Hauptprogramm(ticTacToe);
        }
        public void Make_Move(int[] koordinaten)
        {
            if(_fehlermeldung == "")
            {
                if (Board[koordinaten[0], koordinaten[1]] == " ")
                {
                    _anzahlSpielzüge++;
                    Board[koordinaten[0], koordinaten[1]] = _derzeitigerSpieler.SpielerZeichen;
                }
                else
                {
                    Console.Clear();
                    _fehlermeldung = "In dem Feld (" + koordinaten[0] + "," + koordinaten[1] + ") steht schon etwas drin.";
                }
            }
        }
        public int[] Get_Move()
        {
            int[] koordinaten = new int[2];
            int yKoordinate = 0;
            int xKoordinate = 0;
            bool yKoord = false;
            bool xKoord = false;

            if (_derzeitigerSpieler.KünstlicheIntelligenz)
            {
                    Random zufallsZahl = new Random();
                    yKoordinate = zufallsZahl.Next(-1, Feld.Boardhoehe);
                    xKoordinate = zufallsZahl.Next(-1, Feld.Boardlaenge);
            }
            else
            {
                if (yKoord == false)
                {
                    Console.WriteLine("Gib bitte die vertikale Achse an.");
                    yKoord = Int32.TryParse(Console.ReadLine(), out yKoordinate);
                }

                if (yKoord == false || yKoordinate < 0 || yKoordinate > Feld.Boardlaenge - 1)
                {
                    _fehlermeldung = "Es wurde fehlerhafte Werte eingegeben. Erlaubt sind Zahlen zwischen 0 und " + ( Feld.Boardlaenge - 1) + ". \n";
                }
                yKoord = true;

                Console.WriteLine("Gib bitte die horizontale Achse an.");
                xKoord = Int32.TryParse(Console.ReadLine(), out xKoordinate);

                if (xKoord == false || xKoordinate < 0 || xKoordinate > Feld.Boardlaenge - 1)
                {
                    _fehlermeldung = "Es muss eine Zahl zwischen 0 und " + (Feld.Boardlaenge - 1) + " eingegeben werden. \n";
                }
            }
            koordinaten[0] = yKoordinate;
            koordinaten[1] = xKoordinate;
            return koordinaten;
        }
    }
}
