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
    public interface ITile
    {
        IPiece Piece { get; set; }

        int AccessType { get; set; }

        bool VictoryCond { get; set; }

        int Column { get; set; }
        int Row { get; set; }
    }
}
