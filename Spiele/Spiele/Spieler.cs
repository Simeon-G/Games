﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiele
{
    public enum SpielerNummer
    {
        spieler1 = 1,
        spieler2
    }
    class Spieler
    {
        //Properties beginnen von der Konvention her mit einem Großbuchstaben
        //_feldName nimmt man üblicherweise für das dahinterliegende Feld/Variable, sofern man sie selber implementiert
        // bsp: 
        // private string _spielerName //hier das Feld
        // public string SpielerName { get {return _spielerName;} set {_spielerName = value;} } //hier die Property
        public string _spielerName { get; set; }
        public int _spielerZahl { get; set; }
        public string _spielerZeichen { get; set; }
        public bool _künstlicheIntelligenz { get; set; }

        //Man könnte hier überlegen die Programmlogik aus dem Konstruktor in eine eigene Methode zu verlegen
        public Spieler(string spielerName, SpielerNummer Zahl)
        {
            _spielerName = spielerName;
            bool rdy = Int32.TryParse(Zahl.ToString(), out int spielerZahl);
            _spielerZahl = spielerZahl;
            if(spielerZahl == 1)
            {
                _spielerZeichen = "X";
            }
            else
            {
                _spielerZeichen = "O";
            }
            if(spielerName == "KI")
            {
                _künstlicheIntelligenz = true;
            }
            else
            {
                _künstlicheIntelligenz = false;
            }
        }
    }
}
