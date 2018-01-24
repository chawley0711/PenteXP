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
        //win conditions
        //make random AI
        //Tournament rule for player 1
        //unit tests
        //play again
        //save/load
        public string Player1Name;
        public string Player2Name;
        public bool pvp;
        public string CpuName = "GOD";
        public List<Intersection> gameIntersections;
        public List<List<Intersection>> gameIntersections2D;
        public Timer turnTimer;
        public int timerSec = 20;
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
            int Rindex = GameButtons.Rows / 2;
            int Cindex = GameButtons.Columns / 2;
            //gameIntersections2D[Cindex][Rindex].IntersectionFill = Fill.Black;
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
                    MessageBoxResult result = MessageBox.Show("Your turn has been skipped :/");
                    timerSec = 20;
                    turnTimer.Start();
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
                    bool found = CheckForTessera(Fill.White);
                    if(!found)
                    {
                        CheckForTria(Fill.White);
                    }
                    lblPlayer.Content = Player1Name + "'s turn";
                }
                else
                {
                    bool found = CheckForTessera(Fill.Black);
                    if(!found)
                    {
                        CheckForTria(Fill.Black);
                    }
                    lblPlayer.Content = Player2Name + "'s turn";
                }
                P1Turn = !P1Turn;
                timerSec = 20;
            }
        }
        //Collin and Jordon
        public bool CheckForTessera(Fill color)
        {
            bool found = false;
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
                            col3 == col1 && //checking to see if in same column
                            col4 == col1 &&
                            col5 == col1) ||
                            (row2 == row1 &&
                            row3 == row1 && //checking to see if in same column
                            row4 == row1 &&
                            row5 == row1) ||
                            ((row2 + 1 == row1 && col2 - 1 == col1) &&
                            (row3 + 2 == row1 && col3 - 2 == col1) &&
                            (row4 + 3 == row1 && col4 - 3 == col1) &&
                            (row5 + 4 == row1 && col5 - 5 == col1)) ||
                            ((row2 + 1 == row1 && col2 + 1 == col1) &&
                            (row3 + 2 == row1 && col3 + 2 == col1) &&
                            (row4 + 3 == row1 && col4 + 3 == col1) &&
                            (row5 + 4 == row1 && col5 + 4 == col1))) //or same row, or diagonal (both ways)
                            {
                                if (color == Fill.White)
                                {
                                    AnnouncementPlayerLabel.Content = Player2Name;
                                }
                                else
                                {
                                    AnnouncementPlayerLabel.Content = Player1Name;
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
        public void CheckForTria(Fill color)
        {
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
                            int col1 = (x + math[i][0]) % GameButtons.Columns;
                            int col2 = (x + math[i][1]) % GameButtons.Columns;
                            int col3 = (x + math[i][2]) % GameButtons.Columns;
                            int col4 = (x + math[i][3]) % GameButtons.Columns;
                            int row1 = (int)Math.Floor((decimal)(x + math[i][0]) / GameButtons.Columns);
                            int row2 = (int)Math.Floor((decimal)(x + math[i][1]) / GameButtons.Columns);
                            int row3 = (int)Math.Floor((decimal)(x + math[i][2]) / GameButtons.Columns);
                            int row4 = (int)Math.Floor((decimal)(x + math[i][3]) / GameButtons.Columns);

                            if ((col2 == col1 &&
                            col3 == col1 && //checking to see if in same column
                            col4 == col1) ||
                            (row2 == row1 &&
                            row3 == row1 && //checking to see if in same column
                            row4 == row1) ||
                            ((row2 + 1 == row1 && col2 - 1 == col1) &&
                            (row3 + 2 == row1 && col3 - 2 == col1) &&
                            (row4 + 3 == row1 && col4 - 3 == col1)) ||
                            ((row2 + 1 == row1 && col2 + 1 == col1) &&
                            (row3 + 2 == row1 && col3 + 2 == col1) &&
                            (row4 + 3 == row1 && col4 + 3 == col1))) //or same row, or diagonal (both ways)
                            {
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
                    }
                    catch (Exception e) { }
                }
            });
            return tria;
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

                    if((index + twoAway[i]) % GameButtons.Columns == col ||
                       (int)Math.Floor((decimal)(index + twoAway[i]) / GameButtons.Columns) == row)
                    {
                        if (gameIntersections.ElementAt(index + twoAway[i]).IntersectionFill == color)
                        {
                            switch (i)
                            {
                                case 0:
                                    try
                                    {
                                        gameIntersections.ElementAt(index + ((-GameButtons.Rows * 2) - 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (-GameButtons.Rows - 1)).IntersectionFill = Fill.Empty;
                                        capture = true;
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
                                        capture = true;

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
                                        capture = true;

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
                                        capture = true;

                                    }
                                    catch (Exception e) { }
                                    break;
                                case 4:
                                    try
                                    {
                                        gameIntersections.ElementAt(index + (GameButtons.Rows * 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (GameButtons.Rows)).IntersectionFill = Fill.Empty;
                                        capture = true;

                                    }
                                    catch (Exception e) { }
                                    break;
                                case 5:
                                    try
                                    {
                                        gameIntersections.ElementAt(index + ((GameButtons.Rows * 2) + 2)).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + (GameButtons.Rows + 1)).IntersectionFill = Fill.Empty;
                                        capture = true;

                                    }
                                    catch (Exception e) { }
                                    break;
                                case 6:
                                    try
                                    {
                                        gameIntersections.ElementAt(index + 2).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index + 1).IntersectionFill = Fill.Empty;
                                        capture = true;

                                    }
                                    catch (Exception e) { }
                                    break;
                                case 7:
                                    try
                                    {
                                        gameIntersections.ElementAt(index - 2).IntersectionFill = Fill.Empty;
                                        gameIntersections.ElementAt(index - 1).IntersectionFill = Fill.Empty;
                                        capture = true;

                                    }
                                    catch (Exception e) { }
                                    break;
                            }
                        }
                    }
                } catch(Exception e) { }
            }
                    return capture;
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
