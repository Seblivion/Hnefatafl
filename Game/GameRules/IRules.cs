using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameRules
{
    /*
     * Interface is used to handle the dependency injection.
     */
    public interface IRules
    {
        bool CheckIfSelectable(GameData.IPiece clickedPiece, GameData.IPiece selectedPiece, int playerTurn);

        bool CheckMove(GameData.ITile fromTile, GameData.ITile toTile);

        bool CheckKingMoveVic(GameData.ITile tile);

        GameData.ITile CheckRemoval(GameData.ITile tile, int player);
    }
}
