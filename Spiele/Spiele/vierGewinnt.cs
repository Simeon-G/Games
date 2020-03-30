using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    class VierGewinnt : IGame
    {
        public string _spielName { get; set; }
        public string[,] _board { get; set; }
        public VierGewinnt(string spielName, string[,] board)
        {
            _spielName = spielName;
            _board = board;
        }
        public void Hauptprogramm(IGame vierGewinnt)
        {
            bool gewonnen = false;
            bool gleichstand = false;

            Console.Clear();
            Spielfeld.Render(_board);
            Console.Write(_fehlermeldung + "\n");
            _fehlermeldung = "";
            Make_move(Get_move());
            gewonnen = VierGewinnt.Gewonnen(4);
            if (gewonnen || gleichstand)
            {
                Ende();
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

        void IGame.Make_Move(int[] koordinate)
        {
            bool ok = false;
            for (int i = _boardhoehe-1; i >= 0; i--)
            {
                if(_board[i, koordinate] == " ")
                {
                    _anzahlSpielzuege++;
                    if(_player == 1)
                    {
                        _board[i, koordinate] = "X";
                        ok = true;
                        i = 0;
                    }
                    else
                    {
                        _board[i, koordinate] = "O";
                        ok = true;
                        i = 0;
                    }
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
                Hauptprogramm();
            }
        }

       int[] IGame.Get_Move()
        {
            int[] koordinaten = new int[1];
            int koordinate = 0;
            bool Koord = false;

            if(_player == "KI")
            {
                    Random zufallsZahl = new Random();
                    koordinate = zufallsZahl.Next(-1, _boardlaenge);
            }
            else
            {
                Console.WriteLine("Gib bitte die Reihe an.");
                Koord = Int32.TryParse(Console.ReadLine(), out koordinate);

                if (Koord == false || koordinate < 0 || koordinate > _boardlaenge - 1)
                {
                    _fehlermeldung = "Es muss eine Zahl zwischen 0 und " + (_boardlaenge - 1) + " eingegeben werden. \n";
                }
            }
            koordinaten[0] = koordinate; 
            return koordinaten;
        }
    }
}
