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

        private void PvPButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PvCButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
