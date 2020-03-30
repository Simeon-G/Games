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
        string _spielName { get; set; } 
        string[,] _board { get; set; }
        void Make_Move(int[] koordinaten);
        int[] Get_Move();
        void Hauptprogramm(IGame spiel);
    }
}
