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

namespace Bombermen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer player = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            player.Open(new Uri("music.mp3", UriKind.Relative));
            player.Play();
        }

     

        private void Play_focus(object sender, MouseEventArgs e)
        {
        
            first.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF0B"));
        }

        private void Constructor_focus(object sender, MouseEventArgs e)
        {

            second.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF0B"));
        }

        private void level_focus(object sender, MouseEventArgs e)
        {

            third.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF0B"));
        }

        private void Exit_focus(object sender, MouseEventArgs e)
        {

            fourth.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF0B"));
        }

        private void Play_leave(object sender, MouseEventArgs e)
        {
            first.Fill =Brushes.Black;
        }

        private void Cons_leave(object sender, MouseEventArgs e)
        {
           second.Fill = Brushes.Black;
        }

        private void level_leave(object sender, MouseEventArgs e)
        {
            third.Fill = Brushes.Black;
        }

        private void Exit_leave(object sender, MouseEventArgs e)
        {
            fourth.Fill = Brushes.Black;
        }

        private void Play_Click(object sender, MouseButtonEventArgs e)
        {
            Main main = new Main();
            player.Stop();
            
            Close();
            main.Show();
        }

        private void Constructor_Click(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            Construct_Window w = new Construct_Window();
            Close();
            w.Show();
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            Close();
        }

        private void Chose_Level_Click(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            Choose_Window window = new Choose_Window();
            Close();
            window.Show();
        }
    }
}
