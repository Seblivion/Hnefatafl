using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.IO;

namespace GameFiles
{
    /*
     * The purpose of the file manager is to save and load the game data.
     * LINQ and XML approach is used.
     */
    static public class FileManager
    {
        // This method is used to create an xml file to store the grid data on.
        public static void SaveBoardGrid(string fileName, XElement doc)
        {
            // See if the name is allowed.
            try
            {
                if (fileName == "" || fileName == "SaveFile")
                {
                    throw (new UserDefinedExceptions.BadStringException(
                        "The file name has no characters or is named SaveFile, which is not allowed!"));
                }
            }
            catch (UserDefinedExceptions.BadStringException)
            {
                throw;
            }

            fileName = fileName + ".xml";
            // Create the file.
            doc.Save(fileName);
        }

        // Load a game.
        public static XDocument LoadBoardGrid(string fileName)
        {
            fileName = fileName + ".xml";
            XDocument doc = XDocument.Load(fileName);

            return doc;
        }

        // Save file name in list of saved games.
        public static List<string> SaveSaveList(string fileName, List<string> list)
        {
            // Check if file does not exist.
            if (File.Exists(@".\SaveList.xml") != true)
            {
                CreateSaveList();
            }

            XDocument doc = XDocument.Load("SaveList.xml");
            XElement el = new XElement("Save", fileName);

            // Add new save name to file and list.
            doc.Root.Add(el);
            list.Add(fileName);

            doc.Save("SaveList.xml");

            return list;
        }

        // Load list of saved games.
        public static List<string> LoadSaveList(List<string> list)
        {
            // Check if file does not exist.
            if (File.Exists(@".\SaveList.xml") == false)
            {
                return list;
            }

            XDocument doc = XDocument.Load("SaveList.xml");

            // Check if there are any names in the file.
            if (doc.Descendants("SaveList").Any())
            {
                // Insert all the saved file names into list.
                var items = doc.Descendants("Save").Select(n => (string)n.Value);

                list = items.ToList();
            }
            
            return list;
        }

        // Delete saved game and remove element from save list.
        public static List<string> DeleteSave(string fileName, List<string> list)
        {
            XDocument doc = XDocument.Load("SaveList.xml");

            // Remove name from xml and list.
            doc.Root.Elements().Where(el => el.Value == fileName).Remove();

            list.Remove(fileName);

            doc.Save("SaveList.xml");

            // Delete file.
            string fileStr = "./" + fileName + ".xml";
            File.Delete(@fileStr);

            return list;
        }

        // Create list of saved games.
        private static void CreateSaveList()
        {
            var doc = new XElement("SaveList");
            doc.Save("SaveList.xml");
        }
    }
}
