using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                    if (board[i, x] == _felderGewonnen[i, x])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
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
            int diagonale = 0;
            int counter = 0;
            bool gewonnen = false;
            bool outOfRange = false;
             
            if (anzahlSpielzüge >= siegesBedingung * 2 - 1)
            {
                for (int i = 0; i < feld.Boardhoehe; i++)
                {
                    for (int x = 0; x < feld.Boardlaenge; x++)
                    {
                        gewonnen = überprüfung1(i, x);
                    }
                        ClearBoard(feld);
                        counter = 0;
                }
                if (gewonnen) { return true; }
                for (int i = 0; i < feld.Boardlaenge; i++)
                {
                    for (int x = 0; x < feld.Boardhoehe; x++)
                    {
                        überprüfung1(x, i);
                    }
                    ClearBoard(feld);
                    counter = 0;
                }
                if (gewonnen) { return true; }
                for (int i = feld.Boardlaenge - 1; i >= 0; i--)
                {
                    for (int x = 0; x < feld.Boardhoehe; x++)
                    {
                        überprüfung2(i + x, x);
                        if (outOfRange) { x = feld.Boardhoehe; }
                    }
                    ClearBoard(feld);
                    counter = 0;
                }
                if (gewonnen) { return true; }
                for (int i = feld.Boardlaenge - 1; i >= 0; i--)
                {
                    for (int x = 0; x < feld.Boardhoehe; x++)
                    {
                        überprüfung2(x, i + x);
                        if (outOfRange) { x = feld.Boardhoehe; }
                    }
                }
                if (gewonnen) { return true; }
                for (int i = feld.Boardhoehe - 1; i >= 0; i--)
                {
                    diagonale = 0;
                    for (int x = feld.Boardlaenge - 1; x >= 0; x--)
                    {
                        überprüfung3(i + diagonale, x);
                        if (outOfRange) { x = 0; }
                    }

                }
                if (gewonnen) { return true; }
                for (int i = feld.Boardlaenge - 1; i >= 0; i--)
                {
                    diagonale = 0;
                    for (int x = feld.Boardhoehe - 1; x >= 0; x--)
                    {
                        überprüfung3(x, i + diagonale);
                        if (outOfRange) { x = 0; }
                    }

                }
                if (gewonnen) { return true; }
                bool überprüfung1(int x, int i)
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
                        ClearBoard(feld);
                    }
                    return false;
                }
                bool überprüfung2(int x, int i)
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
                            ClearBoard(feld);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        counter = 0;
                        outOfRange = true;
                        ClearBoard(feld);
                    }
                    return false;
                }
                bool überprüfung3(int x, int i)
                {
                    try
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
                            ClearBoard(feld);
                        }
                        diagonale++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        counter = 0;
                        diagonale = 0;
                        outOfRange = true;
                        ClearBoard(feld);
                    }
                    return false;
                }
            }
            ClearBoard(feld);
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
        //return Befehl springt nicht aus der ganzen Methode und wiederholt noch einmal die while-schleife
        public static IGame Spielwahl(Spieler spieler1, Spieler spieler2)
        {
            int spielAuswahl;
            string[,] board = null;
            bool überprüfung = true;
            IGame ausgabe = null;

            while (überprüfung)
            {
                Console.WriteLine("Möchstest du (1) TicTacToe oder (2) 4 Gewinnt spielen?");
                bool ok = Int32.TryParse(Console.ReadLine(), out spielAuswahl);
                if (ok && spielAuswahl == 1 || spielAuswahl == 2)
                {
                    if (spielAuswahl == 1)
                    {
                        Spielfeld feld = new Spielfeld(3, 3, GameKonstanten.tictactoe);
                        board = new string[feld.Boardhoehe, feld.Boardlaenge];
                        _felderGewonnen = board;
                        TicTacToe ticTacToe = new TicTacToe(GameKonstanten.tictactoe, board, feld, spieler1, spieler2);
                        Console.Clear();
                        ausgabe = ticTacToe;
                        überprüfung = false;
                    }
                    else
                    {
                        Spielfeld feld = new Spielfeld(7, 6, GameKonstanten.viergewinnt);
                        board = new string[feld.Boardhoehe, feld.Boardlaenge];
                        _felderGewonnen = board;
                        VierGewinnt vierGewinnt = new VierGewinnt(GameKonstanten.viergewinnt, board, feld, spieler1, spieler2);
                        Console.Clear();
                        ausgabe = vierGewinnt;
                        überprüfung = false;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Es wurde ein fehlerhafter Wert eingegeben.");
                    überprüfung = false;
                }
            }
            if(ausgabe == null)
            {
                return Spielwahl(spieler1, spieler2);
            }
            return ausgabe;
        }
        private static void ClearBoard( Spielfeld feld)
        {
            for (int i = 0; i < feld.Boardhoehe; i++)
            {
                for (int x = 0; x < feld.Boardlaenge; x++)
                {
                    _felderGewonnen[i, x] = null;
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
