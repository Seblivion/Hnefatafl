using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /*
     * A square is an empty tile to which pieces can be moved.
     */
    class Square : Tile
    {
        public Square()
        {
            AccessType = 0;
            VictoryCond = false;
        }
    }
}
