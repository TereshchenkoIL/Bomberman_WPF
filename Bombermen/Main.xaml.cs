using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Bombermen
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        List<World> levels;
        World world;
        Player pl;
        Enemy en;
        int i = 0;
        bool Continue = true;
        private MediaPlayer player = new MediaPlayer();
        public MediaPlayer bomb_player = new MediaPlayer();
        
     


        public Main()
        {
            InitializeComponent();
            Prepare();
          
                world = levels[i];
                Run(world);
          

            player.Open(new Uri("music.mp3", UriKind.Relative));
            player.Play();
            bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
        }
        public Main(bool Only1, int num)
        {
            InitializeComponent();
            Prepare();
            world = levels[num];
            Run(world);
            Continue = false;
            player.Open(new Uri("music.mp3", UriKind.Relative));
            player.Play();
            bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
        }
        public void Run(World world)
        {
            try
            {
                game_canv.Width = world.Columns * 20;
                game_canv.Height = world.Rows * 20;
                this.Width = world.Columns * 20 + 200;
                this.Height = game_canv.Height = world.Rows * 20 + 400;

                pl = new Player(20, 20);
                ;
                game_canv.Children.Add(pl.Uielement);
                Canvas.SetLeft(pl.Uielement, pl.X);
                Canvas.SetTop(pl.Uielement, pl.Y);
                Draw(world);
                Spawn_Enemy();
            }catch(Exception ex)
            {
                Run(world);
            }
        }
        public void Draw_Player( Player pl)
        {
            game_canv.Children.Add(pl.Uielement);
            Canvas.SetLeft(pl.Uielement, pl.X);
            Canvas.SetTop(pl.Uielement, pl.Y);
        }
       public  void Draw(World world)
        {
            for (int i = 0; i < world.Rows; i++)
            {
                for (int j = 0; j < world.Columns; j++)
                {
                    #region Wave
                    if (world[i * 20, j * 20].sym == '>')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Right.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '<')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Left.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '-')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Down.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == '+')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Up.png", UriKind.Relative)) } };
                    if (world[i * 20, j * 20].sym == ' ')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = Brushes.Green };
                    if (world[i * 20, j * 20].sym == '.')
                        world[i * 20, j * 20].Uielement = new Rectangle() { Width = 20, Height = 20, Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Wave_Center.png", UriKind.Relative)) } };
                    #endregion

                    if (!game_canv.Children.Contains(world[i * 20, j * 20].Uielement))
                    {
                        game_canv.Children.Add(world[i * 20, j * 20].Uielement);
                        Canvas.SetLeft(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].X);
                        Canvas.SetTop(world[i * 20, j * 20].Uielement, world[i * 20, j * 20].Y);
                       
                    }
                }
            }
            if(game_canv.Children.Contains(pl.Uielement))
            {
                game_canv.Children.Remove(pl.Uielement);
                Draw_Player(pl);
                if (en!= null && en.isAlive)
                    RefreshEnemy();
            }
          
        }
        public void RefreshEnemy()
        {
            game_canv.Children.Remove(en.Uielement);
            game_canv.Children.Add(en.Uielement);
            Canvas.SetLeft(en.Uielement, en.X);
            Canvas.SetTop(en.Uielement, en.Y);
        }
       public void RemoveEnemy()
        {
            if (en != null) 
            game_canv.Children.Remove(en.Uielement);
        }

       public void Spawn_Enemy()
        {
            Random r = new Random();
            int x = r.Next(3, world.Columns);
            int y = r.Next(3, world.Rows);         
            if(world[y*20,x*20].sym != 'o' && world[y * 20, x * 20].sym != '#')
            {
                en = new Enemy(x*20, y*20, this, world, pl);
                return;
          
            }
            else
            {
                List<Element> el = world.GetEmptNeighb(x * 20, y * 20);
                int i = r.Next(el.Count);
                en = new Enemy(el[i].X, el[i].Y, this, world, pl);
               
            }

        }


        private void Key_Released(object sender, KeyEventArgs e)
        {
            Count_Bricks.Text = world.Sum_Brick().ToString();
            
           if(!pl.IsAlive)
            {
                
                MainWindow menu = new MainWindow();
                menu.Show();
                Close();
                player.Stop();
            }
           

                pl.Handlkey(e);
            if (pl.direct == Directions.SPACE)
            {
                Bomb b = new Bomb(pl.X, pl.Y,this,world,pl,en);
                game_canv.Children.Add(b.Uielement);
                Canvas.SetLeft(b.Uielement, b.X);
                Canvas.SetTop(b.Uielement, b.Y);
                player.Pause();
                MediaPlayer bomb_player = new MediaPlayer();
                bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
               
                player.Play();

            }
            else
            {
                Canvas.SetLeft(pl.Uielement, pl.X);
                Canvas.SetTop(pl.Uielement, pl.Y);

                pl.Move(world);
                Canvas.SetLeft(pl.Uielement, pl.X);
                Canvas.SetTop(pl.Uielement, pl.Y);
             
            }
            
        }
        private void Prepare()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader("Levels.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {

                    levels = (List<World>)serializer.Deserialize(reader, typeof(List<World>));

                }
            }
            foreach(var i in levels)
            {
                i.Validate();
            }

        }

        private void Key_Pressed(object sender, KeyEventArgs e)
        {

            if (!pl.IsAlive)
            {

                MainWindow menu = new MainWindow();
                menu.Show();
                Close();
                player.Stop();
            }
            if (pl.Check_Finish(world))
            {

                player.Stop();

                player = new MediaPlayer();

                player.Open(new Uri("finish.mp3", UriKind.Relative));
                player.Play();

                MessageBox.Show("Congratulations");
               en.isAlive = false;

                i++;  
                game_canv.Children.Clear();

                if(i < levels.Count() && Continue == true)
                {
                    RemoveEnemy();
                    en = null;
                    world = levels[i];
                    Run(world);
                    player.Open(new Uri("music.mp3", UriKind.Relative));
                    player.Play();
                }                         
                else if(!Continue)
                {
                    MessageBox.Show("Congratulation");
                    MainWindow menu = new MainWindow();
                    menu.Show();
                    player.Stop();
                    Close();
                }
                else
                {
                    MessageBox.Show("Вы прошли игру \n Вы можете создать свою карту в конструкторе\n" +
                        "для двльнейшего удовольствия");
                    en = null;
                    MainWindow menu = new MainWindow();        
                    player.Stop();
                    Close();
                    menu.Show();
                }
               
            }
        }
    }
}
