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
        public string _spielName { get; set; }
        public string[,] _board { get; set; }
        public bool _yKoord = false;
        public bool _xKoord = false;
        public TicTacToe(string spielName, string[,] board)
        {
            _spielName = spielName;
            _board = board;
        }
        public void Hauptprogramm(IGame ticTacToe)
        {
            bool gewonnen = false;
            bool gleichstand = false;
            Console.Clear();
            Spielfeld.Render(_board);
            Console.Write(_fehlermeldung);
            _fehlermeldung = "";
            Make_move(Get_move());
            bool gewonnen = Gewonnen(_boardlaenge);
            if (gewonnen || gleichstand)
            {
                Ende(gewonnen);
            }
            if (_player == 1)
            {
                _player = 2;
            }
            else
            {
                _player = 1;
            }
            Console.Clear();
            Hauptprogramm();
        }
        int[] IGame.Get_Move()
        {
            int[] koordinaten = new int[2];
            int yKoordinate = 0;
            int xKoordinate = 0;

            if (_playerName[_player] == "KI")
            {
                    Random zufallsZahl = new Random();
                    yKoordinate = zufallsZahl.Next(-1, _boardhoehe);
                    xKoordinate = zufallsZahl.Next(-1, _boardlaenge);
            }
            else
            {
                if (_yKoord == false)
                {
                    Console.WriteLine("Gib bitte die vertikale Achse an.");
                    _yKoord = Int32.TryParse(Console.ReadLine(), out yKoordinate);
                }

                if (_yKoord == false || yKoordinate < 0 || yKoordinate > _boardlaenge - 1)
                {
                    _fehlermeldung = "Es muss eine Zahl zwischen 0 und " + (_boardlaenge - 1) + " eingegeben werden. \n";
                    Hauptprogramm();
                }
                _yKoord = true;

                Console.WriteLine("Gib bitte die horizontale Achse an.");
                _xKoord = Int32.TryParse(Console.ReadLine(), out xKoordinate);

                if (_xKoord == false || xKoordinate < 0 || xKoordinate > _boardlaenge - 1)
                {
                    _fehlermeldung = "Es muss eine Zahl zwischen 0 und " + (_boardlaenge - 1) + " eingegeben werden. \n";
                    Hauptprogramm();
                }
            }

            _yKoord = false;
            _xKoord = false;
            koordinaten[0] = yKoordinate;
            koordinaten[1] = xKoordinate;
            return koordinaten;
        }
        void IGame.Make_Move(int[] koordinaten)
        {
            if (_board[koordinaten[0], koordinaten[1]] == " ")
            {
                _anzahlSpielzuege++;
                if (_player == 1)
                {
                    _board[koordinaten[0], koordinaten[1]] = "X";
                    return;
                }
                else
                {
                    _board[koordinaten[0], koordinaten[1]] = "O";
                    return;
                }
            }
            else
            {
                Console.Clear();
                _fehlermeldung = "In dem Feld (" + koordinaten[0] + "," + koordinaten[1] + ") steht schon etwas drin.";
                Hauptprogramm();
            }
        }
    }
}
