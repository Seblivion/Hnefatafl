using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLogic
{
    /*
     * HnefataflGame manages the game logic of Hnefatafl.
     */
    public class HnefataflGame : IGame
    {
        #region Public Methods

        // Default constructor. Creates a 9x9 board. Injects board.
        public HnefataflGame(ref GameData.ITile[,] board)
        {
            BoardGrid = board;
        }

        // Select player to start. Default value is 1.
        public void SelectPlayerToStart(int playerTurn) { this.PlayerTurn = playerTurn; }

        // Select piece.
        public void SelectPiece(int column, int row)
        {
            BoardGrid[column, row].Piece.Selected = true;
            SelectedPiece = BoardGrid[column, row].Piece;
        }

        // Unselect piece.
        public void UnselectPiece(int column, int row)
        {
            BoardGrid[column, row].Piece.Selected = false;
            SelectedPiece = null;
        }

        // Move selected piece to another tile if a piece is selected.
        public void MoveSelectedPiece(int toColumn, int toRow, GameData.IPiece newPiece)
        {
            // Move piece to new tile.
            newPiece.Column = toColumn;
            newPiece.Row = toRow;
            BoardGrid[toColumn, toRow].Piece = newPiece;
            // Remove the piece from the old tile.
            BoardGrid[SelectedPiece.Column, SelectedPiece.Row].Piece = null;
            // Remove piece from SelectedPiece.
            SelectedPiece = null;
        }

        // Shift player turn.
        public void ShiftPLayerTurn()
        {
            if (PlayerTurn == 1)
            {
                ++PlayerTurn;
            }
            else if (PlayerTurn == 2)
            {
                --PlayerTurn;
            }
        }

        // Remove enemy pieces.
        public void RemovePieces(int column, int row, GameRules.IRules rules)
        {
            // Check if there are pieces to be removed until there are no more to remove.
            GameData.ITile tile = rules.CheckRemoval(BoardGrid[column, row], PlayerTurn);
            while(tile != null)
            {
                // Check if the removed piece is the king.
                if (tile.Piece is GameData.King)
                {
                    EndGame();
                    break;
                }
                tile.Piece = null;
                tile = rules.CheckRemoval(BoardGrid[column, row], PlayerTurn);
            }
        }

        // Ends the game.
        public void EndGame()
        {
            GameEnded = true;
        }

        // Load a new board.
        public void LoadBoardGrid(Game.GameData.ITile[,] board)
        {
            BoardGrid = board;
        }

        #endregion

        #region Data

        // Board.
        public GameData.ITile[,] BoardGrid { get; set; }

        // Player turn. Player turn might be 0 when game has ended.
        public int PlayerTurn { get; set; } = 1;

        // Selected piece.
        public GameData.IPiece SelectedPiece { get; set; } = null;

        // Boolean for status of the game.
        public bool GameEnded { get; set; } = false;

        #endregion
    }
}
