using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    /*
     * Interface is used to handle the dependency injection.
     */
    public interface IPiece
    {
        int PieceClassType { get; set; }

        bool Selectable { get; set; }
        bool Selected { get; set; }

        bool Removable { get; set; }

        bool VictoryCond { get; set; }

        int AccessType { get; set; }
        HashSet<int> AccessibleTypes { get; set; }
        HashSet<int> CrossableTypes { get; set; }

        int Player { get; set; }

        int Column { get; set; }
        int Row { get; set; }
    }
}
