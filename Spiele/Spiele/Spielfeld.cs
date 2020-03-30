using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    public enum ausgewähltesSpiel
    {
        TicTacToe = 1,
        VierGewinnt
    }
    class Spielfeld
    {
        public int _boardlaenge { get; set; }
        public int _boardhoehe { get; set; }
        string _aktuellesSpiel { get; set; }
        public string _ausgewähltesSpiel { get; set; }

        public string[,] _felderGewonnen;
        public Spielfeld(int boardlaenge, int boardhoehe, string aktuellesSpiel)
        {
            _boardlaenge = boardlaenge;
            _boardhoehe = boardhoehe;
            _aktuellesSpiel = aktuellesSpiel;
        }
        public void Render(string[,] board, IGame spiel)
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
            Console.WriteLine("*" + _aktuellesSpiel._spielName + "*");
            Console.Write("  ");
            for (int i = 0; i < _boardlaenge; i++)
            {
                Console.Write(GameKonstanten.zahlen[i] + " ");
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
                    if (board[i, x] == _felderGewonnen[i, x])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(board[i, x] + " ");
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
        public bool Gewonnen(int siegesBedingung)
        {
            string[,] boardKopie = _board;
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
                            boardKopie[i, x] = _board[i, x];
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

                for (int i = 0; i < _boardlaenge; i++)
                {
                    for (int x = 0; x < _boardhoehe; x++)
                    {
                        if (_board[x, i] == derzeitigerSpieler)
                        {
                            _felderGewonnen[x, i] = _board[x, i];
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
                                _felderGewonnen[i + x, x] = _board[i + x, x];
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

                for (int i = _boardlaenge - 1; i >= 0; i--)
                {
                    for (int x = 0; x < _boardhoehe; x++)
                    {
                        try
                        {
                            if (_board[x, i + x] == derzeitigerSpieler)
                            {
                                _felderGewonnen[x, i + x] = _board[x, i + x];
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
                                _felderGewonnen[i + diagonale, x] = _board[i + diagonale, x];
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
                    for (int x = _boardhoehe - 1; x >= 0; x--)
                    {
                        try
                        {
                            if (_board[x, i + diagonale] == derzeitigerSpieler)
                            {
                                _felderGewonnen[x, i + diagonale] = _board[x, i + diagonale];
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

        protected void Ende(bool gewonnen)
        {
            string nochmalSpielen;
            Console.Clear();
            Render(_board);
            if (gewonnen)
            {
                if (_playerName[_player] == "KI")
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
                    if (_ausgewähltesSpiel == GameKonstanten.tictactoe)
                    {
                        Console.Clear();
                        ticTacToe.Hauptprogramm();
                    }
                    else
                    {
                        if (_ausgewähltesSpiel == GameKonstanten.viergewinnt)
                        {
                            Console.Clear();
                            
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
                        if (nochmalSpielen.ToUpper() == "START")
                        {
                            Hauptklasse.Main();
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
        public static string AbfrageSpielernamen(SpielerNummer spieler)
        {
            string name;
            Console.WriteLine("Spieler " + spieler + ", wie ist dein Name?");
            name = Console.ReadLine();
            return name;
        }
        public static IGame Spielwahl()
        {
            int spielAuswahl;
            string[,] board = null;
            while (true)
            {
                Console.WriteLine("Möchstest du (1) TicTacToe oder (2) 4 Gewinnt spielen?");
                bool ok = Int32.TryParse(Console.ReadLine(), out spielAuswahl);
                if (ok && spielAuswahl == 1 || spielAuswahl == 2)
                {
                    if (spielAuswahl == 1)
                    {
                        Spielfeld feld = new Spielfeld(3, 3, GameKonstanten.tictactoe);
                        board = new string[feld._boardhoehe, feld._boardlaenge];
                        TicTacToe ticTacToe = new TicTacToe(GameKonstanten.tictactoe, board);
                        Console.Clear();
                        return ticTacToe;
                        
                    }
                    else
                    {
                        Spielfeld feld = new Spielfeld(7, 6, GameKonstanten.viergewinnt);
                        board = new string[feld._boardhoehe, feld._boardlaenge];
                        VierGewinnt vierGewinnt = new VierGewinnt(GameKonstanten.viergewinnt, board);
                        Console.Clear();
                        return vierGewinnt;
                        
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                    Spielwahl();
                }
            }
        }
        private void WerteZurücksetzen()
        {
            ClearBoard(_board);
            ClearBoard(_boardKopie);
            _anzahlSpielzuege = 0;
            _gewonnen = false;
            _gleichstand = false;
            _player = 1;
        }
        public void ClearBoard(string[,] board)
        {
            for (int i = 0; i < _boardhoehe; i++)
            {
                for (int x = 0; x < _boardlaenge; x++)
                {
                    board[i, x] = null;
                }
            }
        }
        private void Gleichstand()
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
            if (counter == _boardhoehe * _boardlaenge)
            {
                _gleichstand = true;
            }
        }
    }
}
