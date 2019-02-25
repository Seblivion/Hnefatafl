using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for WindowSave.xaml
    /// </summary>
    public partial class WindowSave : Window
    {
        public WindowSave()
        {
            InitializeComponent();

            StupidDataGhost.SaveList = StupidDataGhost.GameEngine.LoadListOfSaves(StupidDataGhost.SaveList);
            // Display SaveList in WPF's SaveListBox.
            if (StupidDataGhost.SaveList != null && StupidDataGhost.SaveList.Any() == true)
            {
                SaveListBox.ItemsSource = null;
                SaveListBox.ItemsSource = StupidDataGhost.SaveList;
            }
        }

        #region Private Button Methods

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if Board is null.
            if (StupidDataGhost.GameEngine.Game.BoardGrid == null)
            {
                MessageBox.Show("There is nothing to save yet!");
                return;
            }
            // Check if name already exists.
            if (StupidDataGhost.SaveList.Contains(NameOfSave.Text) == true)
            {
                MessageBox.Show("The file name is already used!");
                return;
            }

            // Save game.
            try
            {
                StupidDataGhost.GameEngine.SaveGame(NameOfSave.Text);
            }
            catch (UserDefinedExceptions.BadStringException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Add file name to the save list storage.
            StupidDataGhost.SaveList = StupidDataGhost.GameEngine.SaveListOfSaves(
                NameOfSave.Text, 
                StupidDataGhost.SaveList);

            // Edit list box.
            SaveListBox.ItemsSource = null;
            SaveListBox.ItemsSource = StupidDataGhost.SaveList;

            // Remove the text in the text box.
            NameOfSave.Text = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if no item is selected.
            if (SaveListBox.SelectedItem == null)
            {
                MessageBox.Show("There is nothing selected!");
                return;
            }

            // Remove the file and save list.
            StupidDataGhost.SaveList = StupidDataGhost.GameEngine.DeleteSavedGame(
                SaveListBox.SelectedItem.ToString(),
                StupidDataGhost.SaveList);

            // Edit list box.
            SaveListBox.ItemsSource = null;
            SaveListBox.ItemsSource = StupidDataGhost.SaveList;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow.Close();
        }

        #endregion
    }
}
