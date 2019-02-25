using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /*
     * The game engine interface.
     * In case there would be more than one game.
     */
    public interface IGameEngine
    {
        void NewGame();
        void OnClick(int column, int row);
        bool GameEnded();
        int PlayerTurn();
        void SaveGame(string fileName);
        void LoadGame(string fileName);
        List<string> SaveListOfSaves(string fileName, List<string> list);
        List<string> LoadListOfSaves(List<string> list);
        List<string> DeleteSavedGame(string fileName, List<string> list);

        Game.GameLogic.IGame Game { get; set; }
        Game.GameRules.IRules Rules { get; set; }
        Game.GameLogic.BoardFactory BoardFactory { get; set; }
    }
}
