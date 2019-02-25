using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.IO;

namespace Game
{
    /*
     * The game engine for Hnefatafl.
     */
    public class HnefataflEngine : IGameEngine
    {
        // Constructor.
        public HnefataflEngine() { }

        #region Public Methods

        public void NewGame()
        {
            // Build the board.
            BoardFactory = new Game.GameLogic.BoardFactory();
            BoardFactory.CreateEmptyGrid();
            BoardFactory.SetUpBoard();

            // Note: Same instance of the board is used everywhere.
            Board = BoardFactory.ReleaseBoard();

            // Create the rules.
            Rules = new Game.GameRules.HnefataflRules(ref Board);

            // Create the game.
            Game = new Game.GameLogic.HnefataflGame(ref Board);
        }

        // Function that is to be run when a tile is clicked.
        // Returns a bool for whether something was changed or not.
        public void OnClick(int column, int row)
        {
            // Nothing is happening if game has ended.
            if (Game.GameEnded == true)
            {
                return;
            }

            // SELECTION AND UNSELECTION OF PIECE
            // Check if there's a piece.
            if (Game.BoardGrid[column, row].Piece != null)
            {
                // Check if player clicked on selected piece.
                if (Game.SelectedPiece == Game.BoardGrid[column, row].Piece)
                {
                    // Unselect piece.
                    Game.UnselectPiece(column, row);
                }
                // If it can be selected, select the piece.
                else if (Rules.CheckIfSelectable(Game.BoardGrid[column, row].Piece, Game.SelectedPiece, Game.PlayerTurn))
                {
                    Game.SelectPiece(column, row);
                }
            }

            // MOVE PIECE
            // If there's no piece clicked on and a selected piece, try to move it.
            else if (Game.SelectedPiece != null)
            {
                // Check if the piece can move from one tile to another.
                if (Rules.CheckMove(Game.BoardGrid[Game.SelectedPiece.Column, Game.SelectedPiece.Row],
                    Game.BoardGrid[column, row]))
                {
                    // Check if the piece to be moved is a soldier.
                    if (Game.SelectedPiece is Game.GameData.Soldier)
                    {
                        Game.MoveSelectedPiece(column, row, new Game.GameData.Soldier(Game.SelectedPiece));
                        // REMOVAL
                        // Remove enemy pieces until there are no more to remove.
                        Game.RemovePieces(column, row, Rules);
                    }
                    // Check if the piece to be moved is a king.
                    else if (Game.SelectedPiece is Game.GameData.King)
                    {
                        Game.MoveSelectedPiece(column, row, new Game.GameData.King(Game.SelectedPiece));
                        // Check if king has moved into a victory castle.
                        if (Rules.CheckKingMoveVic(Game.BoardGrid[column, row]))
                        {
                            Game.EndGame();
                        }
                        // REMOVAL
                        // Remove enemy pieces until there are no more to remove.
                        Game.RemovePieces(column, row, Rules);
                    }
                    // Uselect the moved piece.
                    Game.UnselectPiece(column, row);
                    // Next player's turn.
                    Game.ShiftPLayerTurn();
                }
            }
        }

        // Game status. Used to check if game has ended.
        public bool GameEnded()
        {
            return Game.GameEnded;
        }

        // PlayerTurn is used to check player turn.
        public int PlayerTurn()
        {
            return Game.PlayerTurn;
        }

        // Save game.
        public void SaveGame(string fileName)
        {
            // List is used to access the pieces.
            List<GameData.IPiece> listOfPieces = new List<GameData.IPiece>();

            // Collect the pieces and put them into a list.
            // For each column.
            GameData.ITile[,] grid = Game.BoardGrid;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                // For each row.
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    // Check if there's a piece to save.
                    if (grid[x, y].Piece != null)
                    {
                        listOfPieces.Add(grid[x, y].Piece);
                    }
                }
            }

            // Creation of document.
            XElement doc = new XElement("Root", new XElement("PieceList", listOfPieces.Select(x => new XElement("Piece",
                                                new XElement("PieceClassType", x.PieceClassType),
                                                new XElement("Player", x.Player),
                                                new XElement("Column", x.Column),
                                                new XElement("Row", x.Row)))),
                                            new XElement("PlayerTurn", Game.PlayerTurn));

            // Save the XElement into a file.
            try
            {
                GameFiles.FileManager.SaveBoardGrid(fileName, doc);
            }
            catch (UserDefinedExceptions.BadStringException)
            {
                throw;
            }
        }

        // Load game.
        public void LoadGame(string fileName)
        {
            // Load the file.
            var doc = GameFiles.FileManager.LoadBoardGrid(fileName);

            // Remove all pieces from game board.
            foreach (var i in Game.BoardGrid)
            {
                i.Piece = null;
            }

            // List is used to access the pieces later on.
            List<GameData.IPiece> listOfPieces = new List<GameData.IPiece>();

            // For saved each piece...
            foreach (XElement ex in doc.Descendants("PieceList"))
            {
                // Insert all the pieces into listOfPieces.
                doc.Descendants("Piece").Select(p => new
                {
                    PieceClassType = p.Element("PieceClassType").Value,
                    Player = p.Element("Player").Value,
                    Column = p.Element("Column").Value,
                    Row = p.Element("Row").Value
                }).ToList().ForEach(p =>
                {
                    // Create new piece.
                    GameData.IPiece newPiece = new GameData.Piece();
                    if (Int32.Parse(p.PieceClassType) == 1)
                    {
                        newPiece = new GameData.King();
                    }
                    else if (Int32.Parse(p.PieceClassType) == 2)
                    {
                        newPiece = new GameData.Soldier();
                    }

                    newPiece.PieceClassType = Int32.Parse(p.PieceClassType);
                    newPiece.Player = Int32.Parse(p.Player);
                    newPiece.Column = Int32.Parse(p.Column);
                    newPiece.Row = Int32.Parse(p.Row);

                    // Add the piece to the piece list.
                    listOfPieces.Add(newPiece);
                });
            }

            // Get the player turn.
            doc.Descendants("Root").Select(p => new
            {
                PlayerTurn = p.Element("PlayerTurn").Value
            }).ToList().ForEach(p =>
            {
                Game.PlayerTurn = Int32.Parse(p.PlayerTurn);
            });

            // Place all pieces on the empty board based of the column and row position.
            foreach (GameData.IPiece piece in listOfPieces)
            {
                Game.BoardGrid[piece.Column, piece.Row].Piece = new GameData.Piece();
                Game.BoardGrid[piece.Column, piece.Row].Piece = piece;
            }
        }

        // Save list of saved games as a list of strings.
        public List<string> SaveListOfSaves(string fileName, List<string> list)
        {
            list = GameFiles.FileManager.SaveSaveList(fileName, list);
            return list;
        }

        // Load list of saved games.
        public List<string> LoadListOfSaves(List<string> list)
        {
            list = GameFiles.FileManager.LoadSaveList(list);
            return list;
        }

        // Remove saved game.
        public List<string> DeleteSavedGame(string fileName, List<string> list)
        {
            list = GameFiles.FileManager.DeleteSave(fileName, list);
            return list;
        }

        #endregion

        #region Public Data

        // Game and rule objects.
        public GameLogic.IGame Game { get; set; }
        public GameRules.IRules Rules { get; set; }

        // The board used to represent the game.
        public GameData.ITile[,] Board;

        // Creates the board/s for the game.
        public GameLogic.BoardFactory BoardFactory { get; set; }

        #endregion
    }
}
