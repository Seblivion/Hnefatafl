using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /*
     * The tile is the basis of the game field.
     * The game is built of various tiles on which interactable objects can be placed.
     */
    abstract class Tile : ITile
    {
        public Tile() { }

        // Piece on the tile.
        public IPiece Piece { get; set; } = null;

        // Determines which access type tile belongs to.
        public int AccessType { get; set; }

        // Determines if a piece is a victory condition.
        public bool VictoryCond { get; set; }

        // Coordinates if tile is in a hash.
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
