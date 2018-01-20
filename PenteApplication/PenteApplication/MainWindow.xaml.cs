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
        public string Player1Name;
        public string Player2Name;
        public bool pvp;
        public string CpuName = "GOD";
        public List<Intersection> gameIntersections;
        public MainWindow()
        {
            InitializeComponent();
            fillGameGrid();
            
        }
        //Jordon and Collin
        public void fillGameGrid()
        {
            gameIntersections = new List<Intersection>();
            for(int i = 0; i < Gameboard.Rows; i++)
            {
                for(int j = 0; j < Gameboard.Columns; j++)
                {
                    Label filler = new Label();
                    filler.Background = Brushes.DarkGoldenrod;
                    filler.BorderThickness = new Thickness(.75);
                    Gameboard.Children.Add(filler);
                }
            }

            for (int i = 0; i < Gameboard.Rows + 1; i++)
            {
                for (int j = 0; j < Gameboard.Columns + 1; j++)
                {
                    Button intersection = new Button();
                    Intersection inter = new Intersection();
                    intersection.Opacity = .5;
                    intersection.Height = GameWindow.Height * .04;
                    intersection.Width = GameWindow.Height * .04;
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
            NamePlayer.Visibility = Visibility.Visible;
            lblP2Captures.Content = CpuName + "'s captures:";
            
        }
        //Austin and Jarrett
        public void PlayerSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string tempName = Player1NameTextBox.Text;
            if (tempName.Equals(""))
            {
                Player1Name = "Player 1";
            }
            else
            {
            Player1Name = tempName;
            }
            Player1Naming(Player1Name);
            //lblP1Captures.Content = Player1Name + "'s captures:";
            //    NamePlayer1.Visibility = Visibility.Hidden;
            //if (pvp)
            //{
            //    NamePlayer2.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    PlayGame.Visibility = Visibility.Visible;
            //}
        }
        //Austin and Jarrett
        //public void Player2SubmitButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Player2Name = Player2NameTextBox.Text;
        //    if (Player2Name.Equals(""))
        //    {
        //        Player2Name = "Player 2";
        //    }
        //    lblP2Captures.Content = Player2Name + "'s captures:";
        //    PlayGame.Visibility = Visibility.Visible;
        //}
        //Austin and Jarrett
        public void Player1Naming(string playerName)
        {
            lblP1Captures.Content = playerName + "'s captures:";
            NamePlayer.Visibility = Visibility.Hidden;
            PlayGame.Visibility = Visibility.Visible;
        }
        //Collin and Jordon
        public void PlaceStone_Click(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            Intersection i = (Intersection)obj.DataContext;
            i.IntersectionFill = Fill.White;
            int index = gameIntersections.IndexOf(i);
            //int col = index % Gameboard.Columns;
            //int row = (int)Math.Floor((decimal)(index/Gameboard.Columns));
        }
    }
} 
