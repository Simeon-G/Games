using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    public interface IGame
    {
        string SpielName { get; set; } 
        string[,] Board { get; set; }
        Spieler Spieler1 { get; set; }
        Spieler Spieler2 { get; set; }
        void Make_Move(int[] koordinaten);
        int[] Get_Move();
        void Hauptprogramm(IGame spiel);
    }
}
