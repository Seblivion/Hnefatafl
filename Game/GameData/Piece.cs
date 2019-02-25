using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /*
     * The piece class is the foundation of the movable pieces on the board.
     * Piece is public since new pieces need to be created when loading a saved game.
     */
    public class Piece : IPiece
    {
        public Piece() { }

        // Type of piece. 2 is soldier and 1 is king.
        public int PieceClassType { get; set; }

        // Selected piece.
        public bool Selectable { get; set; }
        public bool Selected { get; set; }

        // Determines whether the piece can be taken by other pieces.
        public bool Removable { get; set; }

        // Determines whether piece is part of a victory condition.
        public bool VictoryCond { get; set; }

        // Determines which access type the piece belongs to.
        public int AccessType { get; set; }

        // Determines which access types the piece can access.
        public HashSet<int> AccessibleTypes { get; set; }

        // Determines which access types the piece can cross.
        public HashSet<int> CrossableTypes { get; set; }

        // Determines which player the piece belongs to.
        public int Player { get; set; }

        // Piece location if it's on a board.
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
