using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Game;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Main Function

        /// <summary>
        /// Default main function.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            // Create engine.
            StupidDataGhost.GameEngine = new Game.HnefataflEngine();

            // Create a new game and display its content.
            NewGame();
        }

        #endregion

        #region Public Interface Methods

        public void NewGame()
        {
            StupidDataGhost.GameEngine.NewGame();

            // Iterate thorugh all the buttons and set the default content.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                var column = Grid.GetColumn(button);
                var row = Grid.GetRow(button);

                // Set background and foreground to default values.
                if (column % 2 != 0 && row % 2 != 0)
                {
                    button.Background = Brushes.SaddleBrown;
                }
                if (column % 2 == 0 && row % 2 == 0)
                {
                    button.Background = Brushes.SaddleBrown;
                }
            });

            // Display player turn.
            DisplayPlayerTurn();
            DisplayPieces();
        }

        public void DisplayPieces()
        {
            // Iterate thorugh all the buttons and set the default content.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                var column = Grid.GetColumn(button);
                var row = Grid.GetRow(button);

                // Create a DisplayChecker. It's used to check what's in the tiles.
                Game.DisplayChecker displayChecker = new DisplayChecker();

                // Display castle.
                if (displayChecker.CheckIfCastle(StupidDataGhost.GameEngine, column, row))
                {
                    Canvas canv = new Canvas();
                    Rectangle tower = new Rectangle();
                    Rectangle def1 = new Rectangle();
                    Rectangle def2 = new Rectangle();
                    Rectangle def3 = new Rectangle();

                    tower.Fill = Brushes.DarkGray;
                    tower.Height = 30;
                    tower.Width = 30;
                    Canvas.SetTop(tower, 10);
                    Canvas.SetLeft(tower, 5);

                    def1.Fill = Brushes.DarkGray;
                    def1.Height = 9;
                    def1.Width = 5;
                    Canvas.SetTop(def1, 5);
                    Canvas.SetLeft(def1, 5);

                    def2.Fill = Brushes.DarkGray;
                    def2.Height = 9;
                    def2.Width = 5;
                    Canvas.SetTop(def2, 5);
                    Canvas.SetLeft(def2, 18);

                    def3.Fill = Brushes.DarkGray;
                    def3.Height = 9;
                    def3.Width = 5;
                    Canvas.SetTop(def3, 5);
                    Canvas.SetLeft(def3, 30);

                    canv.Children.Add(tower);
                    canv.Children.Add(def1);
                    canv.Children.Add(def2);
                    canv.Children.Add(def3);

                    button.Content = canv;
                    button.HorizontalContentAlignment = HorizontalAlignment.Left;
                    button.VerticalContentAlignment = VerticalAlignment.Top;
                }
                // Display king on castle.
                if (displayChecker.CheckIfKingAndCastle(StupidDataGhost.GameEngine, column, row))
                {
                    Grid grd = new Grid();
                    Ellipse el = new Ellipse();
                    // Display selected.
                    if (displayChecker.CheckIfSelected(StupidDataGhost.GameEngine, column, row))
                    {
                        el.Fill = Brushes.Black;
                        el.Height = 30;
                        el.Width = 30;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                    else
                    {
                        el.Fill = Brushes.Lavender;
                        el.Height = 30;
                        el.Width = 30;
                        el.Stroke = Brushes.Black;
                        el.StrokeThickness = 6;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                    button.HorizontalContentAlignment = HorizontalAlignment.Center;
                    button.VerticalContentAlignment = VerticalAlignment.Center;
                }
                // Display king.
                else if (displayChecker.CheckIfKing(StupidDataGhost.GameEngine, column, row))
                {
                    Grid grd = new Grid();
                    Ellipse el = new Ellipse();
                    // Display selected.
                    if (displayChecker.CheckIfSelected(StupidDataGhost.GameEngine, column, row))
                    {
                        el.Fill = Brushes.Black;
                        el.Height = 30;
                        el.Width = 30;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                    else
                    {
                        el.Fill = Brushes.Lavender;
                        el.Height = 30;
                        el.Width = 30;
                        el.Stroke = Brushes.Black;
                        el.StrokeThickness = 6;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                }
                // Display soldiers of defender.
                if (displayChecker.CheckIfSoldierDef(StupidDataGhost.GameEngine, column, row))
                {
                    Grid grd = new Grid();
                    Ellipse el = new Ellipse();
                    // Display selected.
                    if (displayChecker.CheckIfSelected(StupidDataGhost.GameEngine, column, row))
                    {
                        el.Fill = Brushes.Black;
                        el.Height = 30;
                        el.Width = 30;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                    else
                    {
                        el.Fill = Brushes.Tomato;
                        el.Height = 30;
                        el.Width = 30;
                        el.Stroke = Brushes.Black;
                        el.StrokeThickness = 6;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                }
                // Display soldiers of attacker.
                if (displayChecker.CheckIfSoldierAtt(StupidDataGhost.GameEngine, column, row))
                {
                    Grid grd = new Grid();
                    Ellipse el = new Ellipse();
                    // Display selected.
                    if (displayChecker.CheckIfSelected(StupidDataGhost.GameEngine, column, row))
                    {
                        el.Fill = Brushes.Black;
                        el.Height = 30;
                        el.Width = 30;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                    else
                    {
                        el.Fill = Brushes.Gold;
                        el.Height = 30;
                        el.Width = 30;
                        el.Stroke = Brushes.Black;
                        el.StrokeThickness = 6;
                        grd.Children.Add(el);
                        button.Content = grd;
                    }
                }
                // Display empty tiles.
                if (displayChecker.CheckIfEmpty(StupidDataGhost.GameEngine, column, row))
                {
                    button.Content = "";
                }
            });
        }

        public void DisplayPlayerTurn()
        {
            // Display player turn.
            if (StupidDataGhost.GameEngine.PlayerTurn() == 1)
            {
                PlayerTurn.Text = "Defender";
            }
            else if (StupidDataGhost.GameEngine.PlayerTurn() == 2)
            {
                PlayerTurn.Text = "Attacker";
            }
        }

        #endregion

        #region Private Button and Event Methods

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            // Execute the click function.
            StupidDataGhost.GameEngine.OnClick(column, row);

            // Check if game has ended.
            if (StupidDataGhost.GameEngine.GameEnded() == true)
            {
                // Display changes.
                DisplayPieces();
                MessageBox.Show(PlayerTurn.Text + " is victorious!");
                return;
            }

            // Display player turn.
            DisplayPlayerTurn();

            // Display changes.
            DisplayPieces();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSave saveWindow = new WindowSave();
            saveWindow.ShowDialog();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            WindowLoad loadWindow = new WindowLoad();
            loadWindow.ShowDialog();
            // Reset the board and player turn in case a file was loaded.
            DisplayPlayerTurn();
            DisplayPieces();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.Close();
        }

        #endregion

    }
}
