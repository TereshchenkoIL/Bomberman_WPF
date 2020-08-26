using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bombermen
{
    public class Player : Element
    {

        public Directions direct = Directions.RIGHT;
        public bool IsAlive = true;
        int i = 1;
        int l = 1;
        int u = 1;
        int d = 1;
        private DispatcherTimer timer = null;
        private Rectangle rect;
        public bool Animate;
        public Player(int x, int y) : base(x, y)
        {
            sym = 'I';
            rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = Brushes.Red;

            Uielement = rect;
            X = x;
            Y = y;
            timerStart();

        }
        public void Move(World map)
        {

            if (IsAlive)
            {

                if (X + 20 < map.Columns * 20)
                    if (direct == Directions.RIGHT && map[Y, X + 20].sym != 'o' && map[Y, X + 20].sym != '#')
                    {
                        X += 20;
                    }

                if (X - 20 > 0)
                    if (direct == Directions.LEFT && map[Y, X - 20].sym != 'o' && map[Y, X - 20].sym != '#')
                    {
                        X -= 20;
                    }

                if (Y - 20 > 0)
                    if (direct == Directions.UP && map[Y - 20, X].sym != 'o' && map[Y - 20, X].sym != '#')
                    {
                        Y -= 20;

                        if (u == 4)
                            u = 1;
                        rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pl-u" + u + ".png", UriKind.Relative)) };
                        Uielement = rect;
                        u++;
                    }


                if (Y + 20 < map.Rows * 20)
                    if (direct == Directions.DOWN && map[Y + 20, X].sym != 'o' && map[Y + 20, X].sym != '#')
                    {
                        Y += 20;

                        if (d == 4)
                            d = 1;
                        rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pl-d" + d + ".png", UriKind.Relative)) };
                        Uielement = rect;
                        d++;
                    }


                
            }

        }

        public void Handlkey(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    direct = Directions.UP;
                    break;
                case Key.A:
                    direct = Directions.LEFT;
                    break;
                case Key.S:
                    direct = Directions.DOWN;
                    break;
                case Key.D:
                    direct = Directions.RIGHT;
                    break;
                case Key.Space:
                    direct = Directions.SPACE;
                    break;


            }
        }
        public bool Check_Finish(World world)
        {

            if (world[Y, X].sym == '&')
                return true;
            else
                return false;

        }

        private void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
           
                if (direct == Directions.RIGHT)
                {

                    if (i == 4)
                        i = 1;
                    rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pl-r" + i + ".png", UriKind.Relative)) };
                    Uielement = rect;
                    i++;

                }
                if (direct == Directions.LEFT)
                {

                    if (l == 4)
                        l = 1;
                    rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pl-l" + l + ".png", UriKind.Relative)) };
                    Uielement = rect;
                    l++;

                }

               
            





        }
    }
}
