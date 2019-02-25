using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /* 
     * Castle is a tile with special conditions.
     * Castle can be used by the black player to take white soldiers and can be accessed by the white king.
     */
    class Castle : Tile
    {
        // The constructor defaults to make the castle a victory condition.
        public Castle(bool victory = true)
        {
            AccessType = 1;
            this.VictoryCond = victory;
        }
    }
}
