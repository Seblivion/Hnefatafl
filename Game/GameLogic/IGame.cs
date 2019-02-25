using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLogic
{
    public interface IGame
    {
        void SelectPlayerToStart(int playerTurn);

        void SelectPiece(int column, int row);

        void UnselectPiece(int column, int row);

        void MoveSelectedPiece(int toColumn, int toRow, GameData.IPiece newPiece);

        void ShiftPLayerTurn();

        void RemovePieces(int column, int row, GameRules.IRules rules);

        void EndGame();

        void LoadBoardGrid(Game.GameData.ITile[,] board);

        GameData.ITile[,] BoardGrid { get; set; }

        int PlayerTurn { get; set; }

        GameData.IPiece SelectedPiece { get; set; }

        bool GameEnded { get; set; }
    }
}
