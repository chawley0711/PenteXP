using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Timers;
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
        //put comments on all methods with descriptions
        //unit tests
        //play again
        //save/load
        public string Player1Name;
        public string Player2Name;
        public bool pvp = false;
        public string CpuName = "GOD";
        public List<Intersection> gameIntersections;
        public List<List<Intersection>> gameIntersections2D;
        public Timer turnTimer;
        public Timer CPUTimer;
        public int timerSec = 20;
        public List<List<int>> foundBlackTria = new List<List<int>>();
        public List<List<int>> foundBlackTessera = new List<List<int>>();
        public List<List<int>> foundWhiteTria = new List<List<int>>();
        public List<List<int>> foundWhiteTessera = new List<List<int>>();
        public bool P1Turn = false;
        public int P1Cap = 0;
        public int P2Cap = 0;
        public bool firstPlacement = true;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        //Jordon and Collin
        public void PlaceFirst()
        {
            int index = (GameButtons.Rows * GameButtons.Columns) / 2;
            int Rindex = GameButtons.Rows / 2;
            int Cindex = GameButtons.Columns / 2;
            gameIntersections[index].IntersectionFill = Fill.Black;
            Button b = (Button)GameButtons.Children[index];
            b.Opacity = 1;
            b.Style = (Style)Application.Current.Resources["MyButtonStyle"];
            lblPlayer.Content = Player2Name + "'s turn";
            turnTimer = new Timer();
            //turnTimer.BeginInit();
            //turnTimer.Enabled = true;
            turnTimer.Interval = 1000;
            turnTimer.Elapsed += timer_tick;
            if (!pvp)
            {
                AI_Turn();
            }
        }
        //Jordon and Collin
        public void timer_tick(Object sender, ElapsedEventArgs e)
        {
            TimerMethod();
        }
        //Collin and Jordon
        public void TimerMethod()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (timerSec < 0)
                {
                    P1Turn = !P1Turn;
                    if (P1Turn)
                    {
                        lblPlayer.Content = Player1Name + "'s turn";
                    }
                    else
                    {
                        lblPlayer.Content = Player2Name + "'s turn";
                    }
                    turnTimer.Stop();
                    MessageBoxResult result = MessageBox.Show("Your turn has been skipped, ran out of time. :/");
                    timerSec = 20;
                    turnTimer.Start();
                    if (!pvp)
                    {
                        AI_Turn();
                    }
                }

                lblTimer.Content = timerSec.ToString();
            });
            timerSec--;
        }
        //Jordon and Collin
        public int FillGameGrid(int size)
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
                    intersection.Opacity = 0;
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
            FillGameGrid((int)BoardSizeSlider.Value - 1);
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
            turnTimer.Start();
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
            lblP1Captures.Content = Player1Name + "'s captures:";
        }
        //Jordon and Collin
        public void flip(Button b, Intersection i, int index)
        {
            AnnouncementPlayerLabel.Visibility = Visibility.Hidden;
            AnnouncementConstantLabel.Visibility = Visibility.Hidden;
            AnnouncementTypeLabel.Visibility = Visibility.Hidden;
            b.Opacity = 1;
            b.Style = (Style)Application.Current.Resources["MyButtonStyle"];

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
                bool done = CheckForWin(Fill.White);
                bool found = false;
                if (!done)
                {
                    found = CheckForTessera(Fill.White);
                }
                if (!found)
                {
                    CheckForTria(Fill.White);
                }
                lblPlayer.Content = Player1Name + "'s turn";
            }
            else
            {
                bool done = CheckForWin(Fill.Black);
                bool found = false;
                if (!done)
                {
                    found = CheckForTessera(Fill.Black);
                }
                if (!found)
                {
                    CheckForTria(Fill.Black);
                }
                lblPlayer.Content = Player2Name + "'s turn";
            }
            P1Turn = !P1Turn;
            timerSec = 20;
            if (!pvp && !P1Turn)
            {
                AI_Turn();
                P1Turn = true;
            }
        }
        //Collin and Jordon
        public void PlaceStone_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (((Intersection)b.DataContext).IntersectionFill == Fill.Empty)
            {
                Intersection i = (Intersection)b.DataContext;
                int index = gameIntersections.IndexOf(i);
                int middle = (GameButtons.Rows * GameButtons.Rows - 1) / 2;
                int upMiddle = middle - GameButtons.Rows;
                int upUpMiddle = upMiddle - GameButtons.Rows;
                int downMiddle = middle + GameButtons.Rows;
                int downDownMiddle = downMiddle + GameButtons.Rows;
                if (P1Turn)
                {
                    if (firstPlacement)
                    {

                        if (!((index <= middle + 2 && index >= middle - 2) || (index <= upMiddle + 2 && index >= upMiddle - 2)
                        || (index <= upUpMiddle + 2 && index >= upUpMiddle - 2) || (index <= downMiddle + 2 && index >= downMiddle - 2)
                        || (index <= downDownMiddle + 2 && index >= downDownMiddle - 2)))
                        {
                            flip(b, i, index);
                            firstPlacement = false;
                        }
                    }
                    else
                    {
                        flip(b, i, index);
                    }
                }
                else
                {
                    flip(b, i, index);
                }
            }
        }
        //Jordon and Collin
        public bool CheckForWin(Fill color)
        {
            bool found = false;
            List<int> intersections = gameIntersections.Where(x => x.IntersectionFill == color).Select(y => gameIntersections.IndexOf(y)).ToList();
            List<List<int>> math = new List<List<int>>()
            {
                new List<int>() {
                   GameButtons.Columns - 1,
                   (GameButtons.Columns * 2) - 2,
                   (GameButtons.Columns * 3) - 3,
                   (GameButtons.Columns * 4) - 4
                },
                new List<int>() {
                   GameButtons.Columns,
                   GameButtons.Columns * 2,
                   GameButtons.Columns * 3,
                   GameButtons.Columns * 4
                },
                new List<int>() {
                   GameButtons.Columns + 1,
                   (GameButtons.Columns * 2) + 2,
                   (GameButtons.Columns * 3) + 3,
                   (GameButtons.Columns * 4) + 4
                },
                new List<int>() {
                   1,
                   2,
                   3,
                   4
                }
            };

            if(color == Fill.Black)
            {
                if(P1Cap == 5)
                {
                    MessageBoxResult result = MessageBox.Show(Player1Name + " wins!");
                    found = true;
                    //end game
                }
            }
            else
            {
                if (P2Cap == 5)
                {
                    MessageBoxResult result = MessageBox.Show(Player2Name + " wins!");
                    found = true;
                    //end game
                }
            }

            intersections.ForEach(x =>
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if ((gameIntersections.ElementAt(x + math[i][0]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][1]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][2]).IntersectionFill == color &&
                        gameIntersections.ElementAt(x + math[i][3]).IntersectionFill == color))
                        {
                            int col1 = (x + math[i][0]) % GameButtons.Columns;
                            int col2 = (x + math[i][1]) % GameButtons.Columns;
                            int col3 = (x + math[i][2]) % GameButtons.Columns;
                            int col4 = (x + math[i][3]) % GameButtons.Columns;
                            int row1 = (int)Math.Floor((decimal)(x + math[i][0]) / GameButtons.Columns);
                            int row2 = (int)Math.Floor((decimal)(x + math[i][1]) / GameButtons.Columns);
                            int row3 = (int)Math.Floor((decimal)(x + math[i][2]) / GameButtons.Columns);
                            int row4 = (int)Math.Floor((decimal)(x + math[i][3]) / GameButtons.Columns);

                            if ((col2 == col1 &&
                            col3 == col1 &&
                            col4 == col1) ||
                            (row2 == row1 &&
                            row3 == row1 &&
                            row4 == row1) ||
                            ((row2 + 1 == row1 && col2 - 1 == col1) &&
                            (row3 + 2 == row1 && col3 - 2 == col1) &&
                            (row4 + 3 == row1 && col4 - 3 == col1)) ||
                            ((row2 + 1 == row1 && col2 + 1 == col1) &&
                            (row3 + 2 == row1 && col3 + 2 == col1) &&
                            (row4 + 3 == row1 && col4 + 3 == col1)))
                            {
                                if (color == Fill.White)
                                {
                                    MessageBoxResult result = MessageBox.Show(Player2Name + " wins!");
                                    //end game
                                }
                                else
                                {
                                    MessageBoxResult result = MessageBox.Show(Player1Name + " wins!");
                                    //end game
                                }
                                found = true;
                                AnnouncementTypeLabel.Content = "Tessera";
                                AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                                AnnouncementConstantLabel.Visibility = Visibility.Visible;
                                AnnouncementTypeLabel.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    catch (Exception e) { }
                }
            });
            return found;
        }
        //Collin and Jordon
        public bool CheckForTessera(Fill color)
        {
            List<int> intersections = gameIntersections.Where(x => x.IntersectionFill == color).Select(y => gameIntersections.IndexOf(y)).ToList();
            List<List<int>> current = new List<List<int>>();
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
                            int col1 = (x + math[i][0]) % GameButtons.Columns;
                            int col2 = (x + math[i][1]) % GameButtons.Columns;
                            int col3 = (x + math[i][2]) % GameButtons.Columns;
                            int col4 = (x + math[i][3]) % GameButtons.Columns;
                            int col5 = (x + math[i][4]) % GameButtons.Columns;
                            int row1 = (int)Math.Floor((decimal)(x + math[i][0]) / GameButtons.Columns);
                            int row2 = (int)Math.Floor((decimal)(x + math[i][1]) / GameButtons.Columns);
                            int row3 = (int)Math.Floor((decimal)(x + math[i][2]) / GameButtons.Columns);
                            int row4 = (int)Math.Floor((decimal)(x + math[i][3]) / GameButtons.Columns);
                            int row5 = (int)Math.Floor((decimal)(x + math[i][4]) / GameButtons.Columns);

                            if ((col2 == col1 &&
                            col3 == col1 &&
                            col4 == col1) ||
                            (row2 == row1 &&
                            row3 == row1 &&
                            row4 == row1) ||
                            ((row2 - 1 == row1 && col2 - 1 == col1) &&
                            (row3 - 2 == row1 && col3 - 2 == col1) &&
                            (row4 - 3 == row1 && col4 - 3 == col1)) ||
                            ((row2 - 1 == row1 && col2 + 1 == col1) &&
                            (row3 - 2 == row1 && col3 + 2 == col1) &&
                            (row4 - 3 == row1 && col4 + 3 == col1)))
                            {
                                current.Add(new List<int>()
                                {
                                    x,
                                    (x + math[i][0]),
                                    (x + math[i][1]),
                                    (x + math[i][2])
                                });
                            }
                        }
                    }
                    catch (Exception e) { }
                }
            });
            return CheckCurrentAndFoundTessera(current, color);
        }
        //Jordon and Collin
        public bool CheckCurrentAndFoundTessera(List<List<int>> current, Fill color)
        {
            bool found = false;
            if (color == Fill.White)
            {
                if (foundWhiteTessera.Count > current.Count)
                {
                    foundWhiteTessera = current;
                }
                else if (foundWhiteTessera.Count < current.Count)
                {
                    foundWhiteTessera.Add(current[current.Count - 1]);
                    found = true;
                    AnnouncementTypeLabel.Content = "Tessera";
                    AnnouncementPlayerLabel.Content = Player2Name;
                    AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                    AnnouncementConstantLabel.Visibility = Visibility.Visible;
                    AnnouncementTypeLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    int notEqual = -1;
                    int equalCount = 0;
                    for (int j = 0; j < current.Count; j++)
                    {
                        for (int i = 0; i < current.Count; i++)
                        {
                            if (EqualLists(foundWhiteTessera[j], current[i]))
                            {
                                equalCount++;
                                notEqual = i;
                            }
                        }
                    }
                    if (equalCount != foundWhiteTessera.Count && equalCount != 0)
                    {
                        foundWhiteTessera.Add(current[notEqual]);
                        found = true;
                        AnnouncementTypeLabel.Content = "Tessera";
                        AnnouncementPlayerLabel.Content = Player2Name;
                        AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                        AnnouncementConstantLabel.Visibility = Visibility.Visible;
                        AnnouncementTypeLabel.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                if(foundBlackTessera.Count > current.Count)
                {
                    foundBlackTessera = current;
                }
                else if(foundBlackTessera.Count < current.Count)
                {
                    foundBlackTessera.Add(current[current.Count - 1]);
                    found = true;
                    AnnouncementTypeLabel.Content = "Tessera";
                    AnnouncementPlayerLabel.Content = Player1Name;
                    AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                    AnnouncementConstantLabel.Visibility = Visibility.Visible;
                    AnnouncementTypeLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    int notEqual = -1;
                    int equalCount = 0;
                    for(int j = 0; j < current.Count; j++)
                    {
                        for (int i = 0; i < current.Count; i++)
                        {
                            if (EqualLists(foundBlackTessera[j], current[i]))
                            {
                                equalCount++;
                                notEqual = i;
                            }
                        }
                    }
                    if (equalCount != foundBlackTessera.Count && equalCount != 0)
                    {
                        foundBlackTessera.Add(current[notEqual]);
                        found = true;
                        AnnouncementTypeLabel.Content = "Tessera";
                        AnnouncementPlayerLabel.Content = Player1Name;
                        AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                        AnnouncementConstantLabel.Visibility = Visibility.Visible;
                        AnnouncementTypeLabel.Visibility = Visibility.Visible;
                    }
                }
            }
            return found;
        }
        //Collin and Jordon
        public bool EqualLists(List<int> main, List<int> other)
        {
            bool equal = true;
            if(main.Count == other.Count)
            {
                for(int i = 0; i < main.Count; i++)
                {
                    if(main[i] != other[i])
                    {
                        equal = false;
                    }
                }
            }
            else
            {
                equal = false;
            }
            return equal;
        }
        //Collin and Jordon
        public void CheckForTria(Fill color)
        {
            List<int> intersections = gameIntersections.Where(x => x.IntersectionFill == color).Select(y => gameIntersections.IndexOf(y)).ToList();
            List<List<int>> current = new List<List<int>>();
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
                            int col1 = (x + math[i][0]) % GameButtons.Columns;
                            int col2 = (x + math[i][1]) % GameButtons.Columns;
                            int col3 = (x + math[i][2]) % GameButtons.Columns;
                            int col4 = (x + math[i][3]) % GameButtons.Columns;
                            int row1 = (int)Math.Floor((decimal)(x + math[i][0]) / GameButtons.Columns);
                            int row2 = (int)Math.Floor((decimal)(x + math[i][1]) / GameButtons.Columns);
                            int row3 = (int)Math.Floor((decimal)(x + math[i][2]) / GameButtons.Columns);
                            int row4 = (int)Math.Floor((decimal)(x + math[i][3]) / GameButtons.Columns);

                            if ((col2 == col1 &&
                            col3 == col1 &&
                            col4 == col1) ||
                            (row2 == row1 &&
                            row3 == row1 &&
                            row4 == row1) ||
                            ((row2 + 1 == row1 && col2 - 1 == col1) &&
                            (row3 + 2 == row1 && col3 - 2 == col1) &&
                            (row4 + 3 == row1 && col4 - 3 == col1)) ||
                            ((row2 + 1 == row1 && col2 + 1 == col1) &&
                            (row3 + 2 == row1 && col3 + 2 == col1) &&
                            (row4 + 3 == row1 && col4 + 3 == col1)))
                            {
                                current.Add(new List<int>()
                                {
                                    x,
                                    (x + math[i][0]),
                                    (x + math[i][1]),
                                    (x + math[i][2])
                                });
                            }
                        }
                    }
                    catch (Exception e) { }
                }
            });
            CheckCurrentAndFoundTria(current, color);
        }
        //Collin and Jordon
        public void CheckCurrentAndFoundTria(List<List<int>> current, Fill color)
        {
            if (color == Fill.White)
            {
                if (foundWhiteTria.Count > current.Count)
                {
                    foundWhiteTria = current;
                }
                else if (foundWhiteTria.Count < current.Count)
                {
                    foundWhiteTria.Add(current[current.Count - 1]);
                    AnnouncementTypeLabel.Content = "Tria";
                    AnnouncementPlayerLabel.Content = Player2Name;
                    AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                    AnnouncementConstantLabel.Visibility = Visibility.Visible;
                    AnnouncementTypeLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    int notEqual = -1;
                    int equalCount = 0;
                    for (int j = 0; j < current.Count; j++)
                    {
                        for (int i = 0; i < current.Count; i++)
                        {
                            if (EqualLists(foundWhiteTria[j], current[i]))
                            {
                                equalCount++;
                                notEqual = i;
                            }
                        }
                    }
                    if (equalCount != foundWhiteTria.Count && equalCount != 0)
                    {
                        foundWhiteTria.Add(current[notEqual]);
                        AnnouncementTypeLabel.Content = "Tria";
                        AnnouncementPlayerLabel.Content = Player2Name;
                        AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                        AnnouncementConstantLabel.Visibility = Visibility.Visible;
                        AnnouncementTypeLabel.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                if (foundBlackTria.Count > current.Count)
                {
                    foundBlackTria = current;
                }
                else if (foundBlackTria.Count < current.Count)
                {
                    foundBlackTria.Add(current[current.Count - 1]);
                    AnnouncementTypeLabel.Content = "Tria";
                    AnnouncementPlayerLabel.Content = Player1Name;
                    AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                    AnnouncementConstantLabel.Visibility = Visibility.Visible;
                    AnnouncementTypeLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    int notEqual = -1;
                    int equalCount = 0;
                    for (int j = 0; j < current.Count; j++)
                    {
                        for (int i = 0; i < current.Count; i++)
                        {
                            if (EqualLists(foundBlackTria[j], current[i]))
                            {
                                equalCount++;
                                notEqual = i;
                            }
                        }
                    }
                    if (equalCount != foundBlackTria.Count && equalCount != 0)
                    {
                        foundBlackTria.Add(current[notEqual]);
                        AnnouncementTypeLabel.Content = "Tria";
                        AnnouncementPlayerLabel.Content = Player1Name;
                        AnnouncementPlayerLabel.Visibility = Visibility.Visible;
                        AnnouncementConstantLabel.Visibility = Visibility.Visible;
                        AnnouncementTypeLabel.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        //Collin and Jordon
        public bool CheckForCapture(int index)
        {
            bool capture = false;
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
                    int col = index % GameButtons.Columns;
                    int row = (int)Math.Floor((decimal)index / GameButtons.Columns);

                    int col1 = (index + twoAway[i]) % GameButtons.Columns;
                    int row1 = (int)Math.Floor((decimal)(index + twoAway[i]) / GameButtons.Columns);

                    if ((col == col1 - 3 &&
                        row == row1) ||
                        (col == col1 + 3 &&
                        row == row1) ||
                        (col == col1 &&
                        row == row1 + 3) ||
                        (col == col1 &&
                        row == row1 - 3) ||
                        (col == col1 + 3 &&
                        row == row1 + 3) ||
                        (col == col1 - 3 &&
                        row == row1 + 3) ||
                        (col == col1 + 3 &&
                        row == row1 - 3) ||
                        (col == col1 - 3 &&
                        row == row1 + 3))
                        {
                if (gameIntersections.ElementAt(index + twoAway[i]).IntersectionFill == color)
                        {
                            switch (i)
                            {
                                case 0:
                                    try
                                    {
                                        if (gameIntersections.ElementAt(index + (-GameButtons.Rows * 2) - 2).IntersectionFill == opposite &&
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows - 1)).IntersectionFill == opposite)
                                        {
                                            gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) - 2)).IntersectionFill = Fill.Empty;
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows - 1)).IntersectionFill = Fill.Empty;
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }
                                        }
                                    }
                                    catch (Exception e) { }
                                    break;
                                case 1:
                                    try
                                    {
                                        if (gameIntersections.ElementAt(index + (-GameButtons.Rows * 2)).IntersectionFill == opposite &&
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows)).IntersectionFill == opposite)
                                        {
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows * 2)).IntersectionFill = Fill.Empty;
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows)).IntersectionFill = Fill.Empty;
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }
                                        }
                                    }
                                    catch (Exception e) { }
                                    break;
                                case 2:
                                    try
                                    {
                                        if (gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) + 2)).IntersectionFill == opposite &&
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows + 1)).IntersectionFill == opposite)
                                        {
                                            gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) + 2)).IntersectionFill = Fill.Empty;
                                            gameIntersections.ElementAt(index + (-GameButtons.Rows + 1)).IntersectionFill = Fill.Empty;
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }

                                        }
                                    }
                                    catch (Exception e) { }
                                    break;
                                case 3:
                                    try
                                    {
                                        if (gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) - 2)).IntersectionFill == opposite &&
                                            gameIntersections.ElementAt(index + (GameButtons.Rows - 1)).IntersectionFill == opposite)
                                        {
                                            gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) - 2)).IntersectionFill = Fill.Empty;
                                            gameIntersections.ElementAt(index + (GameButtons.Rows - 1)).IntersectionFill = Fill.Empty;
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }

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
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }
                                        }
                                    }
                                    catch (Exception e) { }
                                    break;
                                case 5:
                                    try
                                    {
                                        if (gameIntersections.ElementAt(index + (GameButtons.Rows * 2) + 2).IntersectionFill == opposite &&
                                            gameIntersections.ElementAt(index + (GameButtons.Rows + 1)).IntersectionFill == opposite)
                                        {
                                            gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) + 2)).IntersectionFill = Fill.Empty;
                                            gameIntersections.ElementAt(index + (GameButtons.Rows + 1)).IntersectionFill = Fill.Empty;
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }
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
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }
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
                                            capture = true;
                                            if (color == Fill.White)
                                            {
                                                P2Cap++;
                                            }
                                            else
                                            {
                                                P1Cap++;
                                            }
                                        }
                                    }
                                    catch (Exception e) { }
                                    break;
                            }
                        }
                    }
                } catch(Exception e) { }
            }
            lblP1Captures.Content = "P1 Captures: " + P1Cap;
            lblP2Captures.Content = "P2 Captures: " + P2Cap;
            return capture;
        }
        //Austin and Jarrett
        public void CPUTimerMethod()
        {
            int cputimerSec = 0;
            CPUTimer = new Timer();
            CPUTimer.Interval = 1000;
            CPUTimer.Elapsed += CPUtimer_tick;
            this.Dispatcher.Invoke(() =>
            {
                cputimerSec = 2;
                turnTimer.Start();
            });
            cputimerSec--;
        }
        //Austin and Jarrett
        public void CPUtimer_tick(Object sender, ElapsedEventArgs e)
        {
            CPUTimerMethod();
        }
        //Austin and Jarrett
        public void AI_Turn()
        {
            int maxVal = (int)BoardSizeSlider.Value * (int)BoardSizeSlider.Value;
            bool isValid;

            do
            {
                isValid = true;
                Random rand = new Random();
                int randNum = rand.Next(maxVal);

                if (gameIntersections[randNum].IntersectionFill == Fill.Empty)
                {
                    Button b = (Button)GameButtons.Children[randNum];
                    Intersection i = (Intersection)b.DataContext;
                    flip(b, i, randNum);
                }
                else
                {
                    isValid = false;
                }
            } while (!isValid);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = ".pnt | Pente";
            s.ShowDialog();
            IFormatter format = new BinaryFormatter();
            Stream slip = new FileStream(s.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //format.Serialize(slip, );
        }
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = ".pnt | Pente";
            s.ShowDialog();
            IFormatter format = new BinaryFormatter();
            Stream slip = new FileStream(s.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //format.Serialize(slip, );
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = ".pnt | Pente";
            o.ShowDialog();
            IFormatter format = new BinaryFormatter();
            Stream slip = new FileStream(o.FileName, FileMode.Open);
            //people = ()format.Deserialize(slip);
        }
    }
} 
