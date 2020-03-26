using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    class vierGewinnt : Hauptklasse
    {
        private static string _fehlermeldung = " ";
        public static void Hauptprogramm()
        {
            Console.Clear();
            Render(_board);
            Console.Write(_fehlermeldung + "\n");
            _fehlermeldung = "";
            Make_move(Get_move());
            _gewonnen = Gewonnen(4);
            if (_gewonnen || _gleichstand)
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

        private static void Make_move(int koordinate)
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

        private static int Get_move()
        {
            int koordinate = 0;
            bool Koord = false;

            Console.WriteLine("Gib bitte die Reihe an.");
            Koord = Int32.TryParse(Console.ReadLine(), out koordinate);

            if (Koord == false || koordinate < 0 || koordinate > _boardlaenge - 1)
            {
                _fehlermeldung = "Es muss eine Zahl zwischen 0 und " + (_boardlaenge - 1) + " eingegeben werden. \n";

            }

            return koordinate;
        }
    }
}
