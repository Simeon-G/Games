using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    class Hauptklasse
    {
        protected static int _player = 1;
        protected static int _boardlaenge;
        protected static int _boardhoehe;
        protected static int _anzahlSpielzuege = 0;
        protected static int[] _zahlen = new int[10];
        protected static bool _gewonnen = false;
        protected static bool _gleichstand = false;
        protected static string _ausgewähltesSpiel;
        protected static string[] _playerName = new string[3];
        protected static string[,] _board;
        protected static string[,] _boardKopie;
        static void Main()
        {
            Console.Clear();
            for (int i = 0; i < _zahlen.Length; i++)
            {
                _zahlen[i] = i;
            }
            AbfrageSpielernamen();
            Spielwahl();
        }

        public static void Render(string[,] board)
        {
            for (int i = 0; i < _boardhoehe; i++)
            {
                for (int x = 0; x < _boardlaenge; x++)
                {
                    if (board[i, x] == null)
                    {
                        board[i, x] = " ";
                    }
                }
            }
            //Individuelle Ausgabe einen X mal X boards
            Console.WriteLine("*" + _ausgewähltesSpiel + "*");
            Console.Write("  ");
            for (int i = 0; i < _boardlaenge; i++)
            {
                Console.Write(_zahlen[i] + " ");
            }
            Console.Write("\n  ");
            for (int i = 0; i < _boardlaenge; i++)
            {
                Console.Write("--");
            }
            Console.Write("\n");
            for (int i = 0; i < _boardhoehe; i++)
            {
                Console.Write(i + "|");
                for (int x = 0; x < _boardlaenge; x++)
                {
                    for (int z = 0; z < _boardhoehe; z++)
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
            for (int i = 0; i < _boardlaenge; i++)
            {
                Console.Write("--");
            }
            Console.Write("\n");
        }
        protected static bool Gewonnen(int siegesBedingung)
        {
            int diagonale = 0;
            int counter = 0;
            string derzeitigerSpieler;

            if (_anzahlSpielzuege >= siegesBedingung * 2 - 1)
            {
                if (_player == 1)
                {
                    derzeitigerSpieler = "X";
                }
                else
                {
                    derzeitigerSpieler = "O";
                }

                for (int i = 0; i < _boardhoehe; i++)
                {
                    for (int x = 0; x < _boardlaenge; x++)
                    {
                        if (_board[i, x] == derzeitigerSpieler)
                        {
                            _boardKopie[i, x] = _board[i, x];
                            counter++;
                            if(counter == siegesBedingung)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            counter = 0;
                            ClearBoardKopie();
                        }
                    }
                    ClearBoardKopie();
                    counter = 0;
                }

                for (int i = 0; i < _boardlaenge; i++)
                {
                    for (int x = 0; x < _boardhoehe; x++)
                    {
                        if (_board[x, i] == derzeitigerSpieler)
                        {
                            _boardKopie[x, i] = _board[x, i];
                            counter++;
                            if (counter == siegesBedingung)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            counter = 0;
                            ClearBoardKopie();
                        }
                    }
                    ClearBoardKopie();
                    counter = 0;
                }

                for (int i = _boardlaenge - 1; i >= 0; i--)
                {
                    for (int x = 0; x < _boardhoehe; x++)
                    {
                        try
                        {
                            if (_board[i + x, x] == derzeitigerSpieler)
                            {
                                _boardKopie[i + x, x] = _board[i + x, x];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoardKopie();
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            x = _boardhoehe;
                            ClearBoardKopie();
                        }
                    }
                    counter = 0;
                    ClearBoardKopie();
                }

                for (int i = _boardlaenge-1; i >= 0; i--)
                {
                    for (int x = 0; x < _boardhoehe; x++)
                    {
                        try
                        {
                            if (_board[x, i+x] == derzeitigerSpieler)
                            {
                                _boardKopie[x, i+x] = _board[x, i+x];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoardKopie();
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            x = _boardhoehe;
                            ClearBoardKopie();
                        }
                    }
                counter = 0;
                ClearBoardKopie();  
                }

                for (int i = _boardhoehe - 1; i >= 0; i--)
                {
                    diagonale = 0;
                    for (int x = _boardlaenge - 1; x >= 0; x--)
                    {
                        try
                        {
                            if (_board[i + diagonale, x] == derzeitigerSpieler)
                            {
                                _boardKopie[i + diagonale, x] = _board[i + diagonale, x];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoardKopie();
                            }
                            diagonale++;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            diagonale = 0;
                            x = 0;
                            ClearBoardKopie();
                        }
                    }

                }

                for (int i = _boardlaenge - 1; i >= 0; i--)
                {
                    diagonale = 0;
                    for (int x = _boardhoehe-1; x >= 0; x--)
                    {
                        try
                        {
                            if (_board[x, i + diagonale] == derzeitigerSpieler)
                            {
                                _boardKopie[x, i + diagonale] = _board[x, i + diagonale];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoardKopie();
                            }
                            diagonale++;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            diagonale = 0;
                            x = 0;
                            ClearBoardKopie();
                        }
                    }

                }

                Gleichstand();
            }
            ClearBoardKopie();
            return false;
        }

        protected static void Ende()
        {
            string nochmalSpielen;
            Console.Clear();
            if(_ausgewähltesSpiel == "TicTacToe")
            {
                Render(_board);
            }
            else
            {
                if(_ausgewähltesSpiel == "4 Gewinnt")
                {
                    Render(_board);
                }
            }
            if (_gewonnen)
            {
                if(_playerName[_player] == "KI")
                {
                    Console.Write(" \n Herzlichen Glückwunsch, ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_playerName[_player] + " " + _player);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" hat gewonnen. \n \n");
                }
                else
                {
                    Console.Write(" \n Herzlichen Glückwunsch, ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_playerName[_player]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" hat gewonnen. \n \n");
                }
            }
            else
            {
                Console.WriteLine("Es ist unenschieden.");
            }
            while (true)
            {
                Console.WriteLine("Willst du nochmal spielen? Gib ja oder nein ein.");
                Console.WriteLine("Du kannst aber auch mit start zum Startmenü kommen und wieder ein Spiel auswählen.");
                nochmalSpielen = Console.ReadLine();
                if (nochmalSpielen.ToUpper() == "JA")
                {
                    WerteZurücksetzen();
                    if(_ausgewähltesSpiel == "TicTacToe")
                    {
                        Console.Clear();
                        TicTacToe.Hauptprogramm();
                    }
                    else
                    {
                        if(_ausgewähltesSpiel == "4 Gewinnt")
                        {
                            Console.Clear();
                            vierGewinnt.Hauptprogramm();
                        }
                    }
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
                        if(nochmalSpielen.ToUpper() == "START")
                        {
                            Main();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                        }
                    }
                }
            }
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
            for (int i = 0; i < _boardhoehe; i++)
            {
                for (int x = 0; x < _boardlaenge; x++)
                {
                    _boardKopie[i, x] = null;
                }
            }
        }
        private static void Spielwahl()
        {
            int spielAuswahl;
            while (true)
            {
                Console.WriteLine("Möchstest du (1) TicTacToe oder (2) 4 Gewinnt spielen?");
                bool ok = Int32.TryParse(Console.ReadLine(), out spielAuswahl);
                if (ok && spielAuswahl == 1 || spielAuswahl == 2)
                {
                    if (spielAuswahl == 1)
                    {
                        _boardhoehe = 3;
                        _boardlaenge = 3;
                        _board = new string[_boardhoehe, _boardlaenge];
                        _boardKopie = new string[_boardhoehe, _boardlaenge];
                        _ausgewähltesSpiel = "TicTacToe";
                        Console.Clear();
                        TicTacToe.Hauptprogramm();
                    }
                    else
                    { 
                        _boardhoehe = 6;
                        _boardlaenge = 7;
                        _board = new string[_boardhoehe, _boardlaenge];
                        _boardKopie = new string[_boardhoehe, _boardlaenge];
                        _ausgewähltesSpiel = "4 Gewinnt";
                        Console.Clear();
                        vierGewinnt.Hauptprogramm();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                }
            }
        }
        private static void WerteZurücksetzen()
        {
            for (int i = 0; i < _boardhoehe; i++)
            {
                for (int x = 0; x < _boardlaenge; x++)
                {
                    _board[i, x] = null;
                    _boardKopie[i, x] = null;
                }
            }
            _anzahlSpielzuege = 0;
            _gewonnen = false;
            _gleichstand = false;
            _player = 1;
        }
        private static void Gleichstand()
        {
            int counter = 0;
            for (int i = 0; i < _boardhoehe; i++)
            {
                for (int x = 0; x < _boardlaenge; x++)
                {
                    if (_board[i, x] != " ")
                    {
                        counter++;
                    }
                }
            }
            if(counter == _boardhoehe * _boardlaenge)
            {
                _gleichstand = true;
            }
        }
    }
}

