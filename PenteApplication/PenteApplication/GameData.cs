using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteApplication
{
    [Serializable]
    public class GameData
    {
        public List<Intersection> buttons;
        public string P1Name;
        public string P2Name;
        public int Player1Cap;
        public int Player2Cap;
        public bool Player1Turn;
        public bool pvp;
        public GameData()
        {
            buttons = new List<Intersection>();
            P1Name = "";
            P2Name = "";
            Player1Cap = 0;
            Player2Cap = 0;
            Player1Turn = true;
            pvp = true;
        }
    }
}
