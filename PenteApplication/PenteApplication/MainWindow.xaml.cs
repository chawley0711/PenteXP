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
        public bool P1Turn = false;
        public MainWindow()
        {
            InitializeComponent();
            fillGameGrid();
        }

        //Jordon and Collin
        public void PlaceFirst()
        {
            int index = (GameButtons.Rows * GameButtons.Columns) / 2;
            gameIntersections[index].IntersectionFill = Fill.Black;
            Button b = (Button)GameButtons.Children[index];
            b.Opacity = 1;
            b.Style = (Style)Application.Current.Resources["MyButtonStyle"];
            b.IsEnabled = false;
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
                    intersection.Opacity = 0;
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
            Player2Entry.Visibility = Visibility.Hidden;
            NamePlayer.Visibility = Visibility.Visible;            
        }
        //Austin and Jarrett
        public void PlayerSubmitButton_Click(object sender, RoutedEventArgs e)
        {
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
            b.Opacity = 1;
            b.Style = (Style)Application.Current.Resources["MyButtonStyle"];
            b.IsEnabled = false;
            Intersection i = (Intersection)b.DataContext;
            if(!P1Turn)
            {
                i.IntersectionFill = Fill.White;
            }
            else
            {
                i.IntersectionFill = Fill.Black;
            }
            //int index = gameIntersections.IndexOf(i);
            //int col = index % Gameboard.Columns;
            //int row = (int)Math.Floor((decimal)(index/Gameboard.Columns));
            P1Turn = !P1Turn;
        }
    }
} 
