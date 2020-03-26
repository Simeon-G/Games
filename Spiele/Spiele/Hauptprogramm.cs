using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    class Hauptklasse
    {
        protected static string[] _playerName = new string[3];
        protected static int _player = 1;
        protected static int _boardgroesse;
        protected static int _anzahlSpielzuege = 0;
        protected static int[] _zahlen = new int[20];
        protected static bool _gewonnen = false;
        static bool _ticTacToe = false;
        static bool _vierGewinnt = false;
        protected static string[,] _board;
        protected static string[,] _boardKopie;
        static void Main(string[] args)
        {
            for (int i = 0; i < _zahlen.Length; i++)
            {
                _zahlen[i] = i;
            }
            AbfrageSpielernamen();
            _boardgroesse = AbfrageFeldgroesse();
            _board = new string[_boardgroesse, _boardgroesse];
            _boardKopie = new string[_boardgroesse, _boardgroesse];
            Spielwahl();
        }
        private static void Hauptprogramm()
        {
            Console.Clear();
            Render(_board);
            Make_move(Get_move());
            _gewonnen = Gewonnen();
            if (_gewonnen)
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
            Hauptprogramm();
        }

        public static void Render(string[,] board)
        {
            for (int i = 0; i < _boardgroesse; i++)
            {
                for (int x = 0; x < _boardgroesse; x++)
                {
                    if (board[i, x] == null)
                    {
                        board[i, x] = " ";
                    }
                }
            }
            //Individuelle Ausgabe einen X mal X boards
            Console.Write("  ");
            for (int i = 0; i < _boardgroesse; i++)
            {
                Console.Write(_zahlen[i] + " ");
            }
            Console.Write("\n  ");
            for (int i = 0; i < _boardgroesse; i++)
            {
                Console.Write("--");
            }
            Console.Write("\n");
            for (int i = 0; i < _boardgroesse; i++)
            {
                Console.Write(i + "|");
                for (int x = 0; x < _boardgroesse; x++)
                {
                    for (int z = 0; z < _boardgroesse; z++)
                    {
                        if (board[i, x] == _boardKopie[i, x])
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    Console.Write(board[i, x] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("|\n");
            }
            Console.Write("  ");
            for (int i = 0; i < _boardgroesse; i++)
            {
                Console.Write("--");
            }
            Console.Write("\n");
        }

        public static int[] Get_move()
        {
            int[] koordinaten = new int[2];
            int yKoordinate = 0;
            int xKoordinate = 0;
            bool yKoord = false;
            bool xKoord = false;

            while (yKoord == false || yKoordinate < 0 || yKoordinate > _boardgroesse - 1)
            {
                Console.WriteLine("Gib bitte die vertikale Achse an.");
                yKoord = Int32.TryParse(Console.ReadLine(), out yKoordinate);

                if (yKoord == false || yKoordinate < 0 || yKoordinate > _boardgroesse - 1)
                {
                    Console.WriteLine("Es muss eine Zahl zwischen 0 und " + (_boardgroesse - 1) + " eingegeben werden. \n");
                }
            }
            while (xKoord == false || xKoordinate < 0 || xKoordinate > _boardgroesse - 1)
            {
                Console.WriteLine("Gib bitte die horizontale Achse an.");
                xKoord = Int32.TryParse(Console.ReadLine(), out xKoordinate);

                if (xKoord == false || xKoordinate < 0 || xKoordinate > _boardgroesse - 1)
                {
                    Console.WriteLine("Es muss eine Zahl zwischen 0 und " + (_boardgroesse - 1) + " eingegeben werden. \n");
                }
            }

            koordinaten[0] = yKoordinate;
            koordinaten[1] = xKoordinate;

            return koordinaten;
        }
        public static void Make_move(int[] koordinaten)
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
                //Console.Clear();
                Console.WriteLine("In dem Feld (" + koordinaten[0] + "," + koordinaten[1] + ") steht schon etwas drin.");
                Spielwahl();
            }
        }
        protected static bool Gewonnen()
        {
            int diagonale1 = 0;
            int diagonale2 = 0;
            int counter = 0;
            string derzeitigerSpieler;

            if (_anzahlSpielzuege >= _boardgroesse * 2 - 1)
            {
                if (_player == 1)
                {
                    derzeitigerSpieler = "X";
                }
                else
                {
                    derzeitigerSpieler = "O";
                }

                for (int i = 0; i < _boardgroesse; i++)
                {
                    for (int x = 0; x < _boardgroesse; x++)
                    {
                        if (_board[i, x] == derzeitigerSpieler)
                        {
                            _boardKopie[i, x] = _board[i, x];
                            counter++;
                        }
                        else
                        {
                            ClearBoardKopie();
                            x = _boardgroesse;
                        }
                    }
                    if (counter == _boardgroesse)
                    {
                        return true;
                    }
                    else
                    {
                        counter = 0;
                    }
                }

                for (int i = 0; i < _boardgroesse; i++)
                {
                    for (int x = 0; x < _boardgroesse; x++)
                    {
                        if (_board[x, i] == derzeitigerSpieler)
                        {
                            _boardKopie[x, i] = _board[x, i];
                            counter++;
                        }
                        else
                        {
                            ClearBoardKopie();
                            x = _boardgroesse;
                        }
                    }
                    if (counter == _boardgroesse)
                    {
                        return true;
                    }
                    else
                    {
                        counter = 0;
                    }
                }

                diagonale1 = 0;
                diagonale2 = 0;

                for (int i = 0; i < _boardgroesse; i++)
                {
                    if (_board[diagonale1, diagonale2] == derzeitigerSpieler)
                    {
                        _boardKopie[diagonale1, diagonale2] = _board[diagonale1, diagonale2];
                        counter++;
                    }
                    else
                    {
                        ClearBoardKopie();
                        break;
                    }
                    diagonale1++;
                    diagonale2++;
                }

                if (counter == _boardgroesse)
                {
                    return true;
                }
                else
                {
                    counter = 0;
                }

                diagonale1 = _boardgroesse - 1;
                diagonale2 = 0;

                for (int i = 0; i < _boardgroesse; i++)
                {
                    if (_board[diagonale1, diagonale2] == derzeitigerSpieler)
                    {
                        _boardKopie[diagonale1, diagonale2] = _board[diagonale1, diagonale2];
                        counter++;
                    }
                    else
                    {
                        ClearBoardKopie();
                        break;
                    }
                    diagonale1--;
                    diagonale2++;
                }

                if (counter == _boardgroesse)
                {
                    return true;
                }
                else
                {
                    counter = 0;
                }
            }
            ClearBoardKopie();
            return false;
        }

        protected static void Ende()
        {
            string nochmalSpielen;
            Console.Clear();
            Render(_board);
            Console.WriteLine("Herzlichen Glückwunsch, " + _playerName[_player] + " hat gewonnen.");
            while (true)
            {
                Console.WriteLine("Willst du nochmal spielen? Gib ja oder nein ein.");
                nochmalSpielen = Console.ReadLine();
                if (nochmalSpielen.ToUpper() == "JA")
                {
                    for (int i = 0; i < _boardgroesse; i++)
                    {
                        for (int x = 0; x < _boardgroesse; x++)
                        {
                            _board[i, x] = null;
                            _boardKopie[i, x] = null;
                        }
                    }
                    _anzahlSpielzuege = 0;
                    _gewonnen = false;
                    _player = 1;
                    Spielwahl();
                }
                else
                {
                    if (nochmalSpielen.ToUpper() == "NEIN")
                    {
                        Console.WriteLine("Drücke eine Taste um das Programm zu schließen.");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                    }
                }
            }
        }
        private static int AbfrageFeldgroesse()
        {
            int feldGroesse;
            Console.WriteLine("Das Feld kann zwischen 2 und 19 liegen. \nWie groß soll das Feld sein?");
            bool ok = Int32.TryParse(Console.ReadLine(), out feldGroesse);
            if (ok && feldGroesse > 1 && feldGroesse < 20)
            {
                Console.WriteLine("Ok, das Feld ist " + feldGroesse + " groß.");
            }
            else
            {
                Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                AbfrageFeldgroesse();
            }
            return feldGroesse;
        }
        private static void AbfrageSpielernamen()
        {
            Console.WriteLine("Spieler 1, wie ist dein Name?");
            _playerName[1] = Console.ReadLine();
            Console.WriteLine("Spieler 2, wie ist dein Name?");
            _playerName[2] = Console.ReadLine();
        }
        public static void ClearBoardKopie()
        {
            for (int i = 0; i < _boardgroesse; i++)
            {
                for (int x = 0; x < _boardgroesse; x++)
                {
                    _boardKopie[i, x] = null;
                }
            }
        }
        private static void Spielwahl()
        {
            int spielAuswahl;
            while (_ticTacToe == false && _vierGewinnt == false)
            {
                Console.WriteLine("Möchstest du (1) TicTacToe oder (2) 4 Gewinnt spielen?");
                bool ok = Int32.TryParse(Console.ReadLine(), out spielAuswahl);
                if (ok && spielAuswahl == 1 || spielAuswahl == 2)
                {
                    if (spielAuswahl == 1)
                    {
                        Console.WriteLine("Du hast TicTacToe ausgewählt.");
                        _ticTacToe = true;
                    }
                    else
                    {
                        Console.WriteLine("Du hast 4 Gewinnt ausgewählt.");
                        _vierGewinnt = true;
                    }
                }
                else
                {
                    Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                }
            }
            if (_ticTacToe)
            {
                Hauptprogramm();
            }
            else
            {
                if (_vierGewinnt)
                {
                    vierGewinnt.Hauptprogramm();
                }
            }
        }
    }
}
