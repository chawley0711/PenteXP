using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PenteApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //finish straight like checks
        //make random AI
        //20 second timer with pause
        //show who's turn it is
        //make sure diagonal captures and tria/tessera work
        //Tournament rule for player 1
        //unit tests
        //play again
        //save/load
        public string Player1Name;
        public string Player2Name;
        public bool pvp;
        public string CpuName = "GOD";
        public List<Intersection> gameIntersections;
        public List<List<int>> foundTria;
        public List<List<int>> foundTessera;
        public bool P1Turn = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        //Jordon and Collin
        public void PlaceFirst()
        {
            int index = (GameButtons.Rows * GameButtons.Columns) / 2;
            gameIntersections[index].IntersectionFill = Fill.Black;
            Button b = (Button)GameButtons.Children[index];
            b.Opacity = 1;
            b.Style = (Style)Application.Current.Resources["MyButtonStyle"];
        }

        //Jordon and Collin
        public int fillGameGrid(int size)
        {
            gameIntersections = new List<Intersection>();
            Gameboard.Rows = size;
            Gameboard.Columns = size;
            GameButtons.Rows = size + 1;
            GameButtons.Columns = size + 1;
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < BoardSizeSlider.Value - 1; j++)
                {
                    Label filler = new Label();
                    filler.Background = Brushes.DarkGoldenrod;
                    filler.BorderThickness = new Thickness(.75);
                    Gameboard.Children.Add(filler);
                }
            }
            GameButtons.Height = 700 - (BoardSizeSlider.Value * 1.8);
            GameButtons.Width = 700 - (BoardSizeSlider.Value * 1.8);

            for (int i = 0; i < size + 1; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    Button intersection = new Button();
                    Intersection inter = new Intersection();
                    intersection.Opacity = 1;
                    intersection.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    intersection.VerticalContentAlignment = VerticalAlignment.Stretch;
                    Binding b = new Binding("IntersectionFill");
                    b.Mode = BindingMode.OneWay;
                    intersection.DataContext = inter;
                    b.Converter = new ColorConverter();
                    intersection.SetBinding(Button.BackgroundProperty, b);
                    intersection.Click += PlaceStone_Click;
                    gameIntersections.Add(inter);
                    GameButtons.Children.Add(intersection);
                }
            }
            return Gameboard.Columns;
        }
        //Austin and Jarrett
        public void PvPButton_Click(object sender, RoutedEventArgs e)
        {
            pvp = true;
            MainMenu.Visibility = Visibility.Hidden;
            NamePlayer.Visibility = Visibility.Visible;
        }
        //Austin and Jarrett
        public void PvCButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            Player2Entry.Visibility = Visibility.Hidden;
            NamePlayer.Visibility = Visibility.Visible;            
        }
        //Austin and Jarrett
        public void PlayerSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            fillGameGrid((int)BoardSizeSlider.Value - 1);
            string tempName1 = Player1NameTextBox.Text;
            Player1Naming(tempName1);
            if(pvp)
            {
                string tempName2 = Player2NameTextBox.Text;
                Player2Naming(tempName2);
            }
            else
            {
                Player2Name = CpuName;
                Player2Naming(Player2Name);
            }
        }
        //Austin and Jarrett
        public void Player2Naming(string playerName)
        {
            if (playerName.Equals(""))
            {
                Player2Name = "Player 2";
            }
            else
            {
                Player2Name = playerName;
            }
            lblP2Captures.Content = playerName + "'s captures:";
            NamePlayer.Visibility = Visibility.Hidden;
            PlayGame.Visibility = Visibility.Visible;
            PlaceFirst();
        }
        //Austin and Jarrett
        public void Player1Naming(string playerName)
        {
            if (playerName.Equals(""))
            {
                Player1Name = "Player 1";
            }
            else
            {
                Player1Name = playerName;
            }
            lblP1Captures.Content = playerName + "'s captures:";
        }
        //Collin and Jordon
        public void PlaceStone_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (((Intersection)b.DataContext).IntersectionFill == Fill.Empty)
            {
                AnnouncementPlayerLabel.Visibility = Visibility.Hidden;
                AnnouncementConstantLabel.Visibility = Visibility.Hidden;
                AnnouncementTypeLabel.Visibility = Visibility.Hidden;
                b.Opacity = 1;
                b.Style = (Style)Application.Current.Resources["MyButtonStyle"];
                Intersection i = (Intersection)b.DataContext;
                int index = gameIntersections.IndexOf(i);
                if (!P1Turn)
                {
                    i.IntersectionFill = Fill.White;
                }
                else
                {
                    i.IntersectionFill = Fill.Black;
                }
                CheckForCapture(index);
                if (!P1Turn)
                {
                    CheckForTessera(Fill.White);
                    CheckForTria(Fill.White);
                }
                else
                {
                    CheckForTessera(Fill.Black);
                    CheckForTria(Fill.Black);
                }
                P1Turn = !P1Turn;
            }
        }

        //Collin and Jordon
        public void CheckForTessera(Fill color)
        {
            List<int> intersections = gameIntersections.Where(x => x.IntersectionFill == color).Select(y => gameIntersections.IndexOf(y)).ToList();
            List<List<int>> math = new List<List<int>>()
            {
                new List<int>() {
                   GameButtons.Columns - 1,
                   (GameButtons.Columns * 2) - 2,
                   (GameButtons.Columns * 3) - 3,
                   (GameButtons.Columns * 4) - 4,
                   -GameButtons.Columns + 1
                },
                new List<int>() {
                   GameButtons.Columns,
                   GameButtons.Columns * 2,
                   GameButtons.Columns * 3,
                   GameButtons.Columns * 4,
                   -GameButtons.Columns
                },
                new List<int>() {
                   GameButtons.Columns + 1,
                   (GameButtons.Columns * 2) + 2,
                   (GameButtons.Columns * 3) + 3,
                   (GameButtons.Columns * 4) + 4,
                   -GameButtons.Columns - 1
                },
                new List<int>() {
                   1,
                   2,
                   3,
                   4,
                   -1
                }
            };

            intersections.ForEach(x =>
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if ((gameIntersections.ElementAt(x + math[i][0]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][1]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][2]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][3]).IntersectionFill == Fill.Empty) ||
                        (gameIntersections.ElementAt(x + math[i][0]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][1]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][2]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][4]).IntersectionFill == Fill.Empty))
                        {
                            if(color == Fill.White)
                            {
                                AnnouncementPlayerLabel.Content = Player2Name;
                            }
                            else
                            {
                                AnnouncementPlayerLabel.Content = Player1Name;
                            }
                            AnnouncementTypeLabel.Content = "Tessera";
                            AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                            AnnouncementConstantLabel.Visibility = Visibility.Visible;
                            AnnouncementTypeLabel.Visibility = Visibility.Visible;
                        }
                    }
                    catch (Exception e) { }
                }
            });
        }

        //Collin and Jordon
        public void CheckForTria(Fill color)
        {
            //if color has tessera, don't do
            List<int> intersections = gameIntersections.Where(x => x.IntersectionFill == color).Select(y => gameIntersections.IndexOf(y)).ToList();
            List<List<int>> math = new List<List<int>>()
            {
                new List<int>() {
                   GameButtons.Columns - 1,
                   (GameButtons.Columns * 2) - 2,
                   (GameButtons.Columns * 3) - 3,
                   -GameButtons.Columns + 1
                },
                new List<int>() {
                   GameButtons.Columns,
                   GameButtons.Columns * 2,
                   GameButtons.Columns * 3,
                   -GameButtons.Columns
                },
                new List<int>() {
                   GameButtons.Columns + 1,
                   (GameButtons.Columns * 2) + 2,
                   (GameButtons.Columns * 3) + 3,
                   -GameButtons.Columns - 1
                },
                new List<int>() {
                   1,
                   2,
                   3,
                   -1,
                }
            };

            intersections.ForEach(x =>
            {
                for(int i = 0; i < 4; i++)
                {
                    try
                    {
                        if (gameIntersections.ElementAt(x + math[i][0]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][1]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][2]).IntersectionFill == Fill.Empty &&
                        gameIntersections.ElementAt(x + math[i][3]).IntersectionFill == Fill.Empty)
                        {
                            int colHolder = (x + math[i][0]) % GameButtons.Columns;
                            int rowHolder = (x + math[i][0]) / GameButtons.Columns;
                            if (((x + math[i][0]) / GameButtons.Columns == colHolder &&
                            (x + math[i][1]) / GameButtons.Columns == colHolder &&
                            (x + math[i][2]) / GameButtons.Columns == colHolder && //checking to see if in same column
                            (x + math[i][3]) / GameButtons.Columns == colHolder)) //or same row, or diagonal (both ways)
                            {
                                //then do things
                            }
                            if (color == Fill.White)
                            {
                                AnnouncementPlayerLabel.Content = Player2Name;
                            }
                            else
                            {
                                AnnouncementPlayerLabel.Content = Player1Name;
                            }
                            AnnouncementTypeLabel.Content = "Tria";
                            AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                            AnnouncementConstantLabel.Visibility = Visibility.Visible;
                            AnnouncementTypeLabel.Visibility = Visibility.Visible;
                        }
                    }
                    catch (Exception e) { }
                }
            });
        }

        //Collin and Jordon
        public void CheckForCapture(int index)
        {
            Fill color = gameIntersections.ElementAt(index).IntersectionFill;
            Fill opposite = Fill.Empty;
            if(color == Fill.White)
            {
                opposite = Fill.Black;
            }
            else
            {
                opposite = Fill.White;
            }
            List<int> twoAway = new List<int>() {
                (-GameButtons.Rows * 3) - 3,
                (-GameButtons.Rows * 3),
                (-GameButtons.Rows * 3) + 3,
                (GameButtons.Rows * 3) - 3,
                (GameButtons.Rows * 3),
                (GameButtons.Rows * 3) + 3,
                3,
                -3
            };
            for(int i = 0; i < 8; i++)
            {
                try
                {
                    if(gameIntersections.ElementAt(index + twoAway[i]).IntersectionFill == color)
                    {
                        switch (i)
                        {
                            case 0:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) - 2)).IntersectionFill == opposite && 
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows - 1)).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) - 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows - 1)).IntersectionFill = Fill.Empty;
                                    }
                                } catch (Exception e) { }
                                break;
                            case 1:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + (-GameButtons.Rows * 2)).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows)).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows * 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows)).IntersectionFill = Fill.Empty;
                                    }
                                } catch(Exception e) { }
                                break;
                            case 2:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) + 2)).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows + 1)).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) + 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows + 1)).IntersectionFill = Fill.Empty;
                                    }
                                } catch (Exception e) { }
                                break;
                            case 3:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) - 2)).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index + (GameButtons.Rows - 1)).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) - 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (GameButtons.Rows - 1)).IntersectionFill = Fill.Empty;
                                    }
                                }
                                catch (Exception e) { }
                                break;
                            case 4:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + (GameButtons.Rows * 2)).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index + (GameButtons.Rows)).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + (GameButtons.Rows * 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (GameButtons.Rows)).IntersectionFill = Fill.Empty;
                                    }
                                }
                                catch (Exception e) { }
                                break;
                            case 5:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) + 2)).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index + (GameButtons.Rows + 1)).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) + 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (GameButtons.Rows + 1)).IntersectionFill = Fill.Empty;
                                    }
                                }
                                catch (Exception e) { }
                                break;
                            case 6:
                                try
                                {
                                    if (gameIntersections.ElementAt(index + 2).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index + 1).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index + 2).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + 1).IntersectionFill = Fill.Empty;
                                    }
                                }
                                catch (Exception e) { }
                                break;
                            case 7:
                                try
                                {
                                    if (gameIntersections.ElementAt(index - 2).IntersectionFill == opposite &&
                                        gameIntersections.ElementAt(index - 1).IntersectionFill == opposite)
                                    {
                                        gameIntersections.ElementAt(index - 2).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index - 1).IntersectionFill = Fill.Empty;
                                    }
                                }
                                catch (Exception e) { }
                                break;
                        }
                    }
                } catch(Exception e) { }
            }
        }
    }
} 
