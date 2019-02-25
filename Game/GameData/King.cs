using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /*
     * The king is a piece which the white player has to reach a victory condition with to win the game.
     * If the king is slayed by the black player, white loses and black wins.
     */
    public class King : Piece
    {
        // Default constructor.
        public King()
        {
            PieceClassType = 1;
            Selectable = true;
            Selected = false;
            Removable = true;
            AccessType = 2;
            AccessibleTypes = new HashSet<int>();
            AccessibleTypes.Add(0);
            AccessibleTypes.Add(1);
            CrossableTypes = new HashSet<int>();
            CrossableTypes.Add(0);
            CrossableTypes.Add(1);
            VictoryCond = true;
            Player = 0;
        }

        // Copy constructor.
        public King(IPiece copyPiece)
        {
            PieceClassType = copyPiece.PieceClassType;
            Selectable = copyPiece.Selectable;
            Selected = copyPiece.Selected;
            Removable = copyPiece.Removable;
            AccessType = copyPiece.AccessType;
            AccessibleTypes = new HashSet<int>(copyPiece.AccessibleTypes);
            CrossableTypes = new HashSet<int>(copyPiece.CrossableTypes);
            VictoryCond = copyPiece.VictoryCond;
            Player = copyPiece.Player;
        }
    }
}
