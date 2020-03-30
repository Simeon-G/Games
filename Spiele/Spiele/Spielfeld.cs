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
    public class Spielfeld
    {
        public int Boardlaenge { get; set; }
        public int Boardhoehe { get; set; }
        string AktuellesSpiel { get; set; }

        private static string[,] _felderGewonnen;
        public Spielfeld(int boardlaenge, int boardhoehe, string aktuellesSpiel)
        {
            Boardlaenge = boardlaenge;
            Boardhoehe = boardhoehe;
            AktuellesSpiel = aktuellesSpiel;
        }
        public static void Render(string[,] board, Spielfeld feld)
        {
            for (int i = 0; i < feld.Boardhoehe; i++)
            {
                for (int x = 0; x < feld.Boardlaenge; x++)
                {
                    if (board[i, x] == null)
                    {
                        board[i, x] = " ";
                    }
                }
            }
            //Individuelle Ausgabe einen X mal X boards
            Console.Write("  ");
            for (int i = 0; i < feld.Boardlaenge; i++)
            {
                Console.Write(GameKonstanten.zahlen[i] + " ");
            }
            Console.Write("\n  ");
            for (int i = 0; i < feld.Boardlaenge; i++)
            {
                Console.Write("--");
            }
            Console.Write("\n");
            for (int i = 0; i < feld.Boardhoehe; i++)
            {
                Console.Write(i + "|");
                for (int x = 0; x < feld.Boardlaenge; x++)
                {
                    //Felder, die zum Sieg beigetragen haben erst später wieder einbauen
                    //if (board[i, x] == _felderGewonnen[i, x])
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //}
                    //else
                    //{
                    //    Console.ForegroundColor = ConsoleColor.White;
                    //}
                    Console.Write(board[i, x] + " ");
                }
                Console.Write("|\n");
            }
            Console.Write("  ");
            for (int i = 0; i < feld.Boardlaenge; i++)
            {
                Console.Write("--");
            }
            Console.Write("\n");
        }
        public static bool Gewonnen(int siegesBedingung, string[,] board, Spielfeld feld, int anzahlSpielzüge, Spieler derzeitigerSpieler)
        {
            string[,] boardKopie = board;
            int diagonale = 0;
            int counter = 0;

            if (anzahlSpielzüge >= siegesBedingung * 2 - 1)
            {
                for (int i = 0; i < feld.Boardhoehe; i++)
                {
                    for (int x = 0; x < feld.Boardlaenge; x++)
                    {
                        if (board[i, x] == derzeitigerSpieler.SpielerZeichen)
                        {
                            _felderGewonnen[i, x] = board[i, x];
                            counter++;
                            if (counter == siegesBedingung)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            counter = 0;
                            ClearBoard(_felderGewonnen, feld);
                        }
                    }
                    ClearBoard(_felderGewonnen, feld);
                    counter = 0;
                }

                for (int i = 0; i < feld.Boardlaenge; i++)
                {
                    for (int x = 0; x < feld.Boardhoehe; x++)
                    {
                        if (board[x, i] == derzeitigerSpieler.SpielerZeichen)
                        {
                            _felderGewonnen[x, i] = board[x, i];
                            counter++;
                            if (counter == siegesBedingung)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            counter = 0;
                            ClearBoard(_felderGewonnen, feld);
                        }
                    }
                    ClearBoard(_felderGewonnen, feld);
                    counter = 0;
                }

                for (int i = feld.Boardlaenge - 1; i >= 0; i--)
                {
                    for (int x = 0; x < feld.Boardhoehe; x++)
                    {
                        try
                        {
                            if (board[i + x, x] == derzeitigerSpieler.SpielerZeichen)
                            {
                                _felderGewonnen[i + x, x] = board[i + x, x];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoard(_felderGewonnen, feld);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            x = feld.Boardhoehe;
                            ClearBoard(_felderGewonnen, feld);
                        }
                    }
                    counter = 0;
                    ClearBoard(_felderGewonnen, feld);
                }

                for (int i = feld.Boardlaenge - 1; i >= 0; i--)
                {
                    for (int x = 0; x < feld.Boardhoehe; x++)
                    {
                        try
                        {
                            if (board[x, i + x] == derzeitigerSpieler.SpielerZeichen)
                            {
                                _felderGewonnen[x, i + x] = board[x, i + x];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoard(_felderGewonnen, feld);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            x = feld.Boardhoehe;
                            ClearBoard(_felderGewonnen, feld);
                        }
                    }
                    counter = 0;
                    ClearBoard(_felderGewonnen, feld);
                }

                for (int i = feld.Boardhoehe - 1; i >= 0; i--)
                {
                    diagonale = 0;
                    for (int x = feld.Boardlaenge - 1; x >= 0; x--)
                    {
                        try
                        {
                            if (board[i + diagonale, x] == derzeitigerSpieler.SpielerZeichen)
                            {
                                _felderGewonnen[i + diagonale, x] = board[i + diagonale, x];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoard(_felderGewonnen, feld);
                            }
                            diagonale++;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            diagonale = 0;
                            x = 0;
                            ClearBoard(_felderGewonnen, feld);
                        }
                    }

                }

                for (int i = feld.Boardlaenge - 1; i >= 0; i--)
                {
                    diagonale = 0;
                    for (int x = feld.Boardhoehe - 1; x >= 0; x--)
                    {
                        try
                        {
                            if (board[x, i + diagonale] == derzeitigerSpieler.SpielerZeichen)
                            {
                                _felderGewonnen[x, i + diagonale] = board[x, i + diagonale];
                                counter++;
                                if (counter == siegesBedingung)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                counter = 0;
                                ClearBoard(_felderGewonnen, feld);
                            }
                            diagonale++;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            counter = 0;
                            diagonale = 0;
                            x = 0;
                            ClearBoard(_felderGewonnen, feld);
                        }
                    }

                }
            }
            ClearBoard(_felderGewonnen, feld);
            return false;
        }

        public static void Ende(bool gewonnen, string[,] board, Spielfeld feld, IGame spiel, Spieler derzeitigerSpieler)
        {
            string nochmalSpielen;
            Console.Clear();
            Render(board, feld);
            if (gewonnen)
            {
                if (derzeitigerSpieler.KünstlicheIntelligenz)
                {
                    Console.Write(" \n Herzlichen Glückwunsch, ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("KI " + derzeitigerSpieler.SpielerZahl);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" hat gewonnen. \n \n");
                }
                else
                {
                    Console.Write(" \n Herzlichen Glückwunsch, ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(derzeitigerSpieler.SpielerName);
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
                    return;
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
        public static IGame Spielwahl(Spieler spieler1, Spieler spieler2)
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
                        board = new string[feld.Boardhoehe, feld.Boardlaenge];
                        TicTacToe ticTacToe = new TicTacToe(GameKonstanten.tictactoe, board, feld, spieler1, spieler2);
                        Console.Clear();
                        return ticTacToe;
                        
                    }
                    else
                    {
                        Spielfeld feld = new Spielfeld(7, 6, GameKonstanten.viergewinnt);
                        board = new string[feld.Boardhoehe, feld.Boardlaenge];
                        VierGewinnt vierGewinnt = new VierGewinnt(GameKonstanten.viergewinnt, board, feld, spieler1, spieler2);
                        Console.Clear();
                        return vierGewinnt;
                        
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                    Spielwahl(spieler1, spieler2);
                }
            }
        }
        private static void ClearBoard(string[,] board, Spielfeld feld)
        {
            for (int i = 0; i < feld.Boardhoehe; i++)
            {
                for (int x = 0; x < feld.Boardlaenge; x++)
                {
                    board[i, x] = null;
                }
            }
        }
        public static bool Gleichstand(Spielfeld feld, string[,] board)
        {
            int counter = 0;
            bool gleichstand = false;
            for (int i = 0; i < feld.Boardhoehe; i++)
            {
                for (int x = 0; x < feld.Boardlaenge; x++)
                {
                    if (board[i, x] != " ")
                    {
                        counter++;
                    }
                }
            }
            if (counter == feld.Boardhoehe * feld.Boardlaenge)
            {
                gleichstand = true;
            }
            return gleichstand;
        }
    }
}
