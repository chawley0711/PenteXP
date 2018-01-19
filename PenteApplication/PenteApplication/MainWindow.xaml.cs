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
        public Intersection[][] gameIntersections;
        public MainWindow()
        {
            InitializeComponent();
            fillGameGrid();
            
        }
        //Jordon and Collin
        public void fillGameGrid()
        {
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
                    intersection.Opacity = .5;
                    intersection.Height = GameWindow.Height * .04;
                    intersection.Width = GameWindow.Height * .04;
                    Binding b = new Binding();
                    GameButtons.Children.Add(intersection);
                }
            }

        }
        //Austin and Jarrett
        public void PvPButton_Click(object sender, RoutedEventArgs e)
        {
            pvp = true;
            MainMenu.Visibility = Visibility.Hidden;
            NamePlayer1.Visibility = Visibility.Visible;
        }
        //Austin and Jarrett
        public void PvCButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            NamePlayer1.Visibility = Visibility.Visible;
            lblP2Captures.Content = CpuName + "'s captures:";
            lblP2Announcement.Content = CpuName + ":";
        }
        //Austin and Jarrett
        public void Player1SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Player1Name = Player1NameTextBox.Text;
            if (Player1Name.Equals(""))
            {
                Player1Name = "Player 1";
            }
            lblP1Captures.Content = Player1Name + "'s captures:";
            lblP1Announcement.Content = Player1Name + ":";
                NamePlayer1.Visibility = Visibility.Hidden;
            if (pvp)
            {
                NamePlayer2.Visibility = Visibility.Visible;
            }
            else
            {
                PlayGame.Visibility = Visibility.Visible;
            }
        }
        //Austin and Jarrett
        public void Player2SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Player2Name = Player2NameTextBox.Text;
            if (Player2Name.Equals(""))
            {
                Player2Name = "Player 2";
            }
            lblP2Captures.Content = Player2Name + "'s captures:";
            lblP2Announcement.Content = Player2Name + ":"; 
            NamePlayer2.Visibility = Visibility.Hidden;
            PlayGame.Visibility = Visibility.Visible;
        }
    }
}
