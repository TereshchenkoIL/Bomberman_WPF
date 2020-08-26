using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bombermen
{
    class Bomb : Element
    {
        Main Main;
        World map;
        Player Pl;
        Enemy en;
        int im = 1;
        private DispatcherTimer timer = null;
        
        public Bomb(int x, int y, Main main, World w,Player pl, Enemy enemy) : base(x,y)
        {
            Pl = pl;
            en = enemy;
            Rectangle rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Bomb1.png", UriKind.Relative)) };

            Uielement = rect;
            X = x;
            Y = y;
            sym = '@';
            Main = main;
            map = w;
            Explosion(map,Pl);
            timerStart();
        }
        async public void Explosion(World map,Player pl)
        {
            await Task.Run(() => Clear_Map(map,pl));
            Main.bomb_player.Open(new Uri("TickingBomb.mp3", UriKind.Relative));
            Main.bomb_player.Play();
            Main.Draw(map);
          await Task.Run(()=>  Clear_Dots(map));
            Main.Draw(map);
            if(en != null)
            //Main.RefreshEnemy();
            Main.game_canv.Children.Remove(Pl.Uielement);
            Main.Draw_Player(Pl);
           
        }
        public void Clear_Map(World world, Player pl)
        {
            Thread.Sleep(800);
          
            if (X + 20 < world.Columns*20 && world[y, X + 20].sym != 'o' && world[y, X + 20].sym != '&')
            {
                world  [y,X + 20].sym = '>';
            }
            if (x - 20 >= 0 && world[y,x - 20].sym != 'o' && world[y, x - 20].sym != '&')
            {
                world[y,x - 20].sym = '<';
                
            }
            if (y + 20 < world.Rows*20 && world[ y + 20,x].sym != 'o' && world[y + 20, x].sym != '&')
            {
                world[y + 20, x].sym = '-';
              
            }
            if (y - 20 >= 0 && world[y -20, x].sym != 'o' && world[y - 20, x].sym != '&')
            {
                world[y - 20, x].sym = '+';
               
            }
            world[y, x].sym = '.';

            if (y + 20 == pl.Y && x == pl.X)
            { pl.IsAlive = false; }
            if (y == pl.Y && x == pl.X)
            { pl.IsAlive = false; }
            if (y - 20 == pl.Y && x == pl.X)
            { pl.IsAlive = false; }

            if (y == pl.Y && x + 20 == pl.X)
            { pl.IsAlive = false; }
            if (y == pl.Y && x - 20 == pl.X)
            { pl.IsAlive = false; }

            //Enemy
            if (en != null)
            {
                if (y + 20 == en.Y && x == en.X)
                { en.isAlive = false; }
                if (y == pl.Y && x == pl.X)
                { en.isAlive = false; }
                if (y - 20 == en.Y && x == en.X)
                { en.isAlive = false; }

                if (y == en.Y && x + 20 == en.X)
                { en.isAlive = false; }
                if (y == en.Y && x - 20 == en.X)
                { en.isAlive = false; }
            }



        }

        public void Clear_Dots(World world)
        {
            Thread.Sleep(200);
            for (int i = 0; i < world.Rows; i++)
            {
                for (int j = 0; j < world.Columns; j++)
                {
                    if (world[i*20, j*20].sym == '.' || world[i * 20, j * 20].sym == '>' || world[i * 20, j * 20].sym == '<' || world[i * 20, j * 20].sym == '+' || world[i * 20, j * 20].sym == '-')
                        world[i*20, j*20].sym = ' ';
                }
            }
           
        }
        private void timerStart()
        {
            timer = new DispatcherTimer();  
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        { if (im == 4)
            {
                timer.Stop();
                Main.game_canv.Children.Remove(Uielement);
            }
            else
            {

                Main.game_canv.Children.Remove(Uielement);
                Rectangle rect = new Rectangle();
                rect.Width = size;
                rect.Height = size;
                rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Bomb" + im + ".png", UriKind.Relative)) };
                Uielement = rect;
                Main.game_canv.Children.Add(Uielement);
                Canvas.SetLeft(Uielement, X);
                Canvas.SetTop(Uielement, Y);
                im++;
            }
        }
    }
}
