using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for WindowLoad.xaml
    /// </summary>
    public partial class WindowLoad : Window
    {
        public WindowLoad()
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

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if no item is selected.
            if (SaveListBox.SelectedItem == null)
            {
                MessageBox.Show("There is nothing selected!");
                return;
            }

            // Load the game.
            StupidDataGhost.GameEngine.LoadGame(SaveListBox.SelectedItem.ToString());

            // Load the list of saved games.
            StupidDataGhost.SaveList = StupidDataGhost.GameEngine.LoadListOfSaves(StupidDataGhost.SaveList);

            // Edit list box.
            SaveListBox.ItemsSource = null;
            SaveListBox.ItemsSource = StupidDataGhost.SaveList;

            LoadWindow.Close();
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
            LoadWindow.Close();
        }

        #endregion

    }
}
