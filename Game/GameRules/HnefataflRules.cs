using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameRules
{
    public class HnefataflRules : IRules
    {
        #region Public Methods

        public HnefataflRules(ref GameData.ITile[,] boardGrid) { this.BoardGrid = boardGrid; }

        // Check if piece is selectable. Can select any piece belonging to the player.
        public bool CheckIfSelectable(GameData.IPiece clickedPiece, GameData.IPiece selectedPiece, int playerTurn)
        {
            if (selectedPiece == null && clickedPiece != null && clickedPiece.Player == playerTurn)
            {
                return clickedPiece.Selectable;
            }
            return false;
        }

        // Check if piece can be moved to the selected tile.
        // Soldiers can't access castles, but can cross them.
        // Soldiers can't move over other pieces or access tiles with another piece.
        // King can move over and access castles.
        // King can't move over other pieces or access tiles with another piece.
        // Pieces can't move diagonally.
        public bool CheckMove(GameData.ITile fromTile, GameData.ITile toTile)
        {
            // Boolean set to true if piece can cross tiles and pieces.
            bool canReachDest = false;
            // Check if piece can move though all tiles to reach destination tile.
            // MOVE VERTICALLLY
            if (fromTile.Column == toTile.Column)
            {
                // If the fromTile row is smaller than the toTile row, increase tmp in loop. Move down.
                if (fromTile.Row < toTile.Row)
                {
                    // Iterate the tiles from the destination to the piece that is to be moved.
                    // Don't start with the selected piece's tile.
                    for (int tmp = fromTile.Row + 1; tmp < toTile.Row; tmp++)
                    {
                        // Check if piece can cross tile.
                        if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[fromTile.Column, tmp].AccessType))
                        {
                            return false;
                        }
                        // Check if there's piece on the tile.
                        if (BoardGrid[fromTile.Column, tmp].Piece != null)
                        {
                            // Check if piece on fromTile can cross the tile's piece.
                            if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[fromTile.Column, tmp].Piece.AccessType))
                            {
                                return false;
                            }
                        }
                    }
                }
                // Else decrease tmp in loop. Move up.
                else
                {
                    // Iterate the tiles from the destination to the piece that is to be moved.
                    for (int tmp = fromTile.Row - 1; tmp > toTile.Row; tmp--)
                    {
                        // Check if piece can cross tile.
                        if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[fromTile.Column, tmp].AccessType))
                        {
                            return false;
                        }
                        // Check if there's piece on the tile.
                        if (BoardGrid[fromTile.Column, tmp].Piece != null)
                        {
                            // Check if piece on fromTile can cross the tile's piece.
                            if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[fromTile.Column, tmp].Piece.AccessType))
                            {
                                return false;
                            }
                        }
                    }
                }
                canReachDest = true;
            }
            // MOVE HORIZONTALLY
            else if (fromTile.Row == toTile.Row)
            {
                // If the fromTile column is smaller than the toTile column, increase tmp in loop. Move right.
                if (fromTile.Column < toTile.Column)
                {
                    // Iterate the tiles from the destination to the piece that is to be moved.
                    for (int tmp = fromTile.Column + 1; tmp < toTile.Column; tmp++)
                    {
                        // Check if piece can cross tile.
                        if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[tmp, fromTile.Row].AccessType))
                        {
                            return false;
                        }
                        // Check if there's piece on the tile.
                        if (BoardGrid[tmp, fromTile.Row].Piece != null)
                        {
                            // Check if piece on fromTile can cross the tile's piece.
                            if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[tmp, fromTile.Row].Piece.AccessType))
                            {
                                return false;
                            }
                        }
                    }
                }
                // Else decrease tmp in loop. Move left.
                else
                {
                    // Iterate the tiles from the destination to the piece that is to be moved.
                    for (int tmp = fromTile.Column - 1; tmp > toTile.Column; tmp--)
                    {
                        // Check if piece can cross tile.
                        if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[tmp, fromTile.Row].AccessType))
                        {
                            return false;
                        }
                        // Check if there's piece on the tile.
                        if (BoardGrid[tmp, fromTile.Row].Piece != null)
                        {
                            // Check if piece on fromTile can cross the tile's piece.
                            if (!fromTile.Piece.CrossableTypes.Contains(BoardGrid[tmp, fromTile.Row].Piece.AccessType))
                            {
                                return false;
                            }
                        }
                    }
                }
                canReachDest = true;
            }
            // CHECK IF PIECE CAN BE PLACED ON DESTIONATION TILE
            // If the piece can cross to until destination has been reached, check if it can be placed.
            if (canReachDest == true)
            {
                bool check1 = false, check2 = false;
                // Check if tile can be accessed.
                check1 = fromTile.Piece.AccessibleTypes.Contains(BoardGrid[toTile.Column, toTile.Row].AccessType);
                check2 = true;
                // Check if there is already a piece on the tile.
                if (BoardGrid[toTile.Column, toTile.Row].Piece != null)
                {
                    // Check if piece allows access.
                    check2 = fromTile.Piece.AccessibleTypes.Contains(BoardGrid[toTile.Column, toTile.Row].Piece.AccessType);
                }
                if (check1 == true && check2 == true)
                {
                    return true;
                }
            }
            return false;
        }

        // If piece is king, check if it moves to a castle part of the victory condition.
        public bool CheckKingMoveVic(GameData.ITile tile)
        {
            if (tile.VictoryCond == true && tile.Piece.VictoryCond == true)
            {
                return true;
            }
            return false;
        }

        // Check if any of opponent's pieces can be removed.
        // A piece is removed if it's between two unfriendly pieces 
        // or between an unfriendly piece and a castle.
        // Output is the tile with the piece that should be removed.
        // This method is repeated until no more tiles are returned.
        public GameData.ITile CheckRemoval(GameData.ITile tile, int player)
        {
            // Number of tiles in a column or row. From 0 to 8, that is 9.
            int max = 8;

            // There are 4 corners, 4 sides and the grid.
            // Upper-left corner.
            if (tile.Column <= 1 && tile.Row <= 1)
            {
                // Check piece below.
                if(CheckBeneath(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row + 1]; 
                }

                // Check right of piece.
                if (CheckRight(tile, player))
                {
                    return BoardGrid[tile.Column + 1, tile.Row];
                }
            }
            // Lower-left corner.
            else if (tile.Column <= 1 && tile.Row >= max - 1)
            {
                // Check above the piece.
                if (CheckOver(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row - 1];
                }

                // Check right of piece.
                if (CheckRight(tile, player))
                {
                    return BoardGrid[tile.Column + 1, tile.Row];
                }
            }
            // Upper-right corner.
            else if (tile.Column >= max - 1 && tile.Row <= 1)
            {
                // Check piece below.
                if (CheckBeneath(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row + 1];
                }

                // Check left of piece.
                if (CheckLeft(tile, player))
                {
                    return BoardGrid[tile.Column - 1, tile.Row];
                }
            }
            // Lower-right corner.
            else if (tile.Column >= max - 1 && tile.Row >= max - 1)
            {
                // Check piece above.
                if (CheckOver(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row - 1];
                }

                // Check left of piece.
                if (CheckLeft(tile, player))
                {
                    return BoardGrid[tile.Column - 1, tile.Row];
                }
            }
            // Left side.
            else if (tile.Column <= 1)
            {
                // Check piece above.
                if (CheckOver(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row - 1];
                }

                // Check piece below.
                if (CheckBeneath(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row + 1];
                }

                // Check right of piece.
                if (CheckRight(tile, player))
                {
                    return BoardGrid[tile.Column + 1, tile.Row];
                }
            }
            // Right side.
            else if (tile.Column >= max - 1)
            {
                // Check piece above.
                if (CheckOver(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row - 1];
                }

                // Check piece below.
                if (CheckBeneath(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row + 1];
                }

                // Check left of piece.
                if (CheckLeft(tile, player))
                {
                    return BoardGrid[tile.Column - 1, tile.Row];
                }
            }
            // Upper side.
            else if (tile.Row <= 1)
            {
                // Check piece below.
                if (CheckBeneath(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row + 1];
                }

                // Check right of piece.
                if (CheckRight(tile, player))
                {
                    return BoardGrid[tile.Column + 1, tile.Row];
                }

                // Check left of piece.
                if (CheckLeft(tile, player))
                {
                    return BoardGrid[tile.Column - 1, tile.Row];
                }
            }
            // Lower side.
            else if (tile.Row >= max - 1)
            {
                // Check piece above.
                if (CheckOver(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row - 1];
                }

                // Check right of piece.
                if (CheckRight(tile, player))
                {
                    return BoardGrid[tile.Column + 1, tile.Row];
                }

                // Check left of piece.
                if (CheckLeft(tile, player))
                {
                    return BoardGrid[tile.Column - 1, tile.Row];
                }
            }
            // Check every direction around the piece.
            else
            {
                // Check piece above.
                if (CheckOver(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row - 1];
                }

                // Check piece below.
                if (CheckBeneath(tile, player))
                {
                    return BoardGrid[tile.Column, tile.Row + 1];
                }

                // Check right of piece.
                if (CheckRight(tile, player))
                {
                    return BoardGrid[tile.Column + 1, tile.Row];
                }

                // Check left of piece.
                if (CheckLeft(tile, player))
                {
                    return BoardGrid[tile.Column - 1, tile.Row];
                }
            }
            return null;
        }

        #endregion

        #region Private Methods

        // Check if the piece beneath the moved piece can be removed.
        private bool CheckBeneath(GameData.ITile tile, int player)
        {
            // Check if there is a piece.
            if (BoardGrid[tile.Column, tile.Row + 1].Piece != null)
            {
                // Check if piece belongs to enemy.
                if (BoardGrid[tile.Column, tile.Row + 1].Piece.Player != player)
                {
                    // Check if there's another piece or a castle on the other side.
                    if (BoardGrid[tile.Column, tile.Row + 2].Piece != null ||
                        BoardGrid[tile.Column, tile.Row + 2] is GameData.Castle)
                    {
                        // Check if next piece belongs to you or if there is a castle.
                        // Castle is checked first since player can't be refered to if it's a castle.
                        if (BoardGrid[tile.Column, tile.Row + 2] is GameData.Castle ||
                            BoardGrid[tile.Column, tile.Row + 2].Piece.Player == player)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Check if the piece over the moved piece can be removed.
        private bool CheckOver(GameData.ITile tile, int player)
        {
            // Check if there is a piece.
            if (BoardGrid[tile.Column, tile.Row - 1].Piece != null)
            {
                // Check if piece belongs to enemy.
                if (BoardGrid[tile.Column, tile.Row - 1].Piece.Player != player)
                {
                    // Check if there is another piece or a castle.
                    if (BoardGrid[tile.Column, tile.Row - 2].Piece != null ||
                        BoardGrid[tile.Column, tile.Row - 2] is GameData.Castle)
                    {
                        // Check if next piece belongs to you or if there is a castle.
                        // Castle is checked first since player can't be refered to if it's a castle.
                        if (BoardGrid[tile.Column, tile.Row - 2] is GameData.Castle ||
                            BoardGrid[tile.Column, tile.Row - 2].Piece.Player == player)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Check if the piece right of the moved piece can be removed.
        private bool CheckRight(GameData.ITile tile, int player)
        {
            // Check if there is a piece.
            if (BoardGrid[tile.Column + 1, tile.Row].Piece != null)
            {
                // Check if piece belongs to enemy.
                if (BoardGrid[tile.Column + 1, tile.Row].Piece.Player != player)
                {
                    // Check if there is another piece or castle on the other side.
                    if (BoardGrid[tile.Column + 2, tile.Row].Piece != null ||
                        BoardGrid[tile.Column + 2, tile.Row] is GameData.Castle)
                    {
                        // Check if next piece belongs to you or if there is a castle.
                        // Castle is checked first since player can't be refered to if it's a castle.
                        if (BoardGrid[tile.Column + 2, tile.Row] is GameData.Castle ||
                            BoardGrid[tile.Column + 2, tile.Row].Piece.Player == player)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Check if the piece left of the moved piece can be removed.
        private bool CheckLeft(GameData.ITile tile, int player)
        {
            // Check if there is a piece.
            if (BoardGrid[tile.Column - 1, tile.Row].Piece != null)
            {
                // Check if piece belongs to enemy.
                if (BoardGrid[tile.Column - 1, tile.Row].Piece.Player != player)
                {
                    // Check if there's another piece or a castle.
                    if (BoardGrid[tile.Column - 2, tile.Row].Piece != null ||
                        BoardGrid[tile.Column - 2, tile.Row] is GameData.Castle)
                    {
                        // Check if next piece belongs to you or if there is a castle.
                        // Castle is checked first since player can't be refered to if it's a castle.
                        if (BoardGrid[tile.Column - 2, tile.Row] is GameData.Castle ||
                            BoardGrid[tile.Column - 2, tile.Row].Piece.Player == player)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Private data

        // Board grid.
        private GameData.ITile[,] BoardGrid { get; set; }

        #endregion

    }
}
