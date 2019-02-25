using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /*
     * The soldier class is used as the primary piece in the game.
     * White player uses the soldiers to protect the king and kill enemy soldiers.
     * Black player uses the soldiers to slay the king and kill enemy soldiers.
     */
    public class Soldier : Piece
    {
        // Default constructor.
        public Soldier()
        {
            PieceClassType = 2;
            Selectable = true;
            Selected = false;
            Removable = true;
            AccessType = 2;
            AccessibleTypes = new HashSet<int>();
            AccessibleTypes.Add(0);
            CrossableTypes = new HashSet<int>();
            CrossableTypes.Add(0);
            CrossableTypes.Add(1);
            VictoryCond = false;
            Player = 0;
        }

        // Copy constructor.
        public Soldier(IPiece copyPiece)
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
