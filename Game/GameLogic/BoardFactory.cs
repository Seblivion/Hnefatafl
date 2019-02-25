using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLogic
{
    /*
     * Board is basically both the board and the factory of the board.
     * The board is inserted into the game.
     */
    public class BoardFactory
    {
        public BoardFactory() { }

        // Generates an array of empty squares to be used as the board foundation.
        public void CreateEmptyGrid()
        {
            // Generates squares for columns and rows.
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    BoardGrid[x, y] = new GameData.Square();
                    BoardGrid[x, y].Column = x;
                    BoardGrid[x, y].Row = y;
                }
            }
        }

        // Create the table for Hnefatafl.
        public void SetUpBoard()
        {
            // Generate pieces and castles, and place them on the board.
            int size = 8;
            for (int tempX = 0; tempX < 9; tempX++)
            {
                for (int tempY = 0; tempY < 9; tempY++)
                {
                    // Place castles and the king.
                    // If it's a corner - replace the square with a castle.
                    if ((tempX == 0 || tempX == size) && (tempY == 0 || tempY == size))
                    {
                        BoardGrid[tempX, tempY] = new GameData.Castle();
                        BoardGrid[tempX, tempY].Row = tempY;
                        BoardGrid[tempX, tempY].Column = tempX;
                    }
                    // If it's the board's centre - replace square with castle and place the king. 
                    else if (tempX == size / 2 && tempY == size / 2)
                    {
                        BoardGrid[tempX, tempY] = new GameData.Castle(false);
                        BoardGrid[tempX, tempY].Row = tempY;
                        BoardGrid[tempX, tempY].Column = tempX;
                        BoardGrid[tempX, tempY].Piece = new GameData.King();
                        BoardGrid[tempX, tempY].Piece.Player = 1;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                    // Place defender pieces.
                    // Horizontally.
                    else if (tempY == size / 2 && tempX > 1 && tempX < size - 1 && tempX != size / 2)
                    {
                        BoardGrid[tempX, tempY].Piece = new GameData.Soldier();
                        BoardGrid[tempX, tempY].Piece.Player = 1;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                    // Vertically.
                    else if (tempX == size / 2 && tempY > 1 && tempY < size - 1 && tempY != size / 2)
                    {
                        BoardGrid[tempX, tempY].Piece = new GameData.Soldier();
                        BoardGrid[tempX, tempY].Piece.Player = 1;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                    // Place attacker pieces.
                    // West and east side.
                    else if ((tempX == 0 || tempX == size) && tempY > 2 && tempY < size - 2)
                    {
                        BoardGrid[tempX, tempY].Piece = new GameData.Soldier();
                        BoardGrid[tempX, tempY].Piece.Player = 2;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                    // North and south.
                    else if ((tempY == 0 || tempY == size) && tempX > 2 && tempX < size - 2)
                    {
                        BoardGrid[tempX, tempY].Piece = new GameData.Soldier();
                        BoardGrid[tempX ,tempY].Piece.Player = 2;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                    // Western and northern vanguard.
                    else if ((tempY == size / 2 && tempX == 1) || (tempX == size / 2 && tempY == 1))
                    {
                        BoardGrid[tempX, tempY].Piece = new GameData.Soldier();
                        BoardGrid[tempX, tempY].Piece.Player = 2;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                    // Eastern and southern vanguard.
                    else if ((tempX == size / 2 && tempY == size - 1) || (tempY == size / 2 && tempX == size - 1))
                    {
                        BoardGrid[tempX, tempY].Piece = new GameData.Soldier();
                        BoardGrid[tempX, tempY].Piece.Player = 2;
                        BoardGrid[tempX, tempY].Piece.Column = tempX;
                        BoardGrid[tempX, tempY].Piece.Row = tempY;
                    }
                }
            }
        }

        // Release the board to game.
        public GameData.ITile[,] ReleaseBoard()
        {
            return BoardGrid;
        }

        // Multi-dimensional array used to represent the board.
        private GameData.ITile[,] BoardGrid { get; set; } = new Game.GameData.ITile[9, 9];
    }
}
