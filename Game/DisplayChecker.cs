using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /*
     * The display checker determines if a certain piece is located on a tile.
     */
    public class DisplayChecker
    {
        public DisplayChecker() { }

        public bool CheckIfCastle(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row] is GameData.Castle;
        }

        public bool CheckIfKingAndCastle(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row] is GameData.Castle
                    && engine.Game.BoardGrid[column, row].Piece is GameData.King;
        }

        public bool CheckIfKing(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row].Piece is GameData.King;
        }

        public bool CheckIfSoldierDef(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row].Piece is GameData.Soldier 
                && engine.Game.BoardGrid[column, row].Piece.Player == 1;
        }

        public bool CheckIfSoldierAtt(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row].Piece is GameData.Soldier 
                && engine.Game.BoardGrid[column, row].Piece.Player == 2;
        }

        public bool CheckIfSelected(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row].Piece != null 
                && engine.Game.BoardGrid[column, row].Piece.Selected == true;
        }

        public bool CheckIfEmpty(IGameEngine engine, int column, int row)
        {
            return engine.Game.BoardGrid[column, row] is GameData.Square
                    && engine.Game.BoardGrid[column, row].Piece == null;
        }
    }
}
