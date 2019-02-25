using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /*
     * The purpose of this mysterious static class is to make sure that even
     * the sub windows have access to the state of the Hnefatafl game.
     * The sub windows need the grid to save and load the game.
     */
    static class StupidDataGhost
    {
        // Game is stored here since the sub windows also need access to some data stored in game.
        // For loading and saving.
        public static Game.IGameEngine GameEngine { get; set; }

        // SaveList is used to effectively get access to the names of the saved game files.
        public static List<string> SaveList { get; set; } = new List<string>();
    }
}
