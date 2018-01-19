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
        public MainWindow()
        {
            InitializeComponent();
            fillGameGrid();
            
        }
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

        }
        //Austin and Jarrett
        private void PvPButton_Click(object sender, RoutedEventArgs e)
        {
            pvp = true;
            MainMenu.Visibility = Visibility.Hidden;
            NamePlayer1.Visibility = Visibility.Visible;
        }
        //Austin and Jarrett
        private void PvCButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            NamePlayer1.Visibility = Visibility.Visible;
            lblP2Captures.Content = CpuName + "'s captures:";
            lblP2Announcement.Content = CpuName + ":";
        }
        //Austin and Jarrett
        private void Player1SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Player1Name = Player1NameTextBox.Text;
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
        private void Player2SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Player2Name = Player2NameTextBox.Text;
            lblP2Captures.Content = Player2Name + "'s captures:";
            lblP2Announcement.Content = Player2Name + ":"; 
            NamePlayer2.Visibility = Visibility.Hidden;
            PlayGame.Visibility = Visibility.Visible;
        }
    }
}
