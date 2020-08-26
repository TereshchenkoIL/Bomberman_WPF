using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bombermen
{
    public class Enemy : Element
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer animation = new DispatcherTimer();
        Main game;
        World map;
        Player pl;
        Element prev;
        int frame = 1;
        public bool isAlive =true;
        Stack<Element> path;
        Rectangle rect;
        public Enemy(int x, int y, Main g, World w, Player player) : base(x, y)
        {
            game = g;
             map= w;
            pl = player;
            rect = new System.Windows.Shapes.Rectangle();
            rect.Width = size;
            rect.Height = size;
            rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Enemy1.png", UriKind.Relative)) };

            Uielement = rect;
            timerStart();
            animateStart();
        }

        private void animateStart()
        {
           animation = new DispatcherTimer();
            animation.Tick += new EventHandler(Animate);
            animation.Interval = new TimeSpan(0, 0, 0, 0, 100);
            animation.Start();
        }
        private void Animate(object sender, EventArgs e)
        {
            if (frame == 5)
                frame = 1;

           
            rect.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"Enemy"+frame+".png", UriKind.Relative)) };
            frame++;
            Uielement = rect;

        }

        private void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e)
        {

            if (isAlive)
            {
                Move();
                CheckPlayer();
            }
            else
            {
                timer.Stop();
                animation.Stop(); 
                    
              
                
                game.RemoveEnemy();
            }
        }
            public void Move()
            {
            
            
                List<Element> empt = map.GetEmptNeighb(X, Y);
                //  MessageBox.Show(empt.Count.ToString());
                if (empt.Count == 1)
                {
                    prev = map[Y, X];
                    X = empt[0].X;
                    Y = empt[0].Y;
                }
                else if (empt != null && empt.Count > 1)
                {
                    Random r = new Random();
                    empt = empt.OrderBy(item => r.Next()).ToList();
                    if (prev != null)
                        empt = empt.Where(x => !x.Equals(prev)).ToList();
                    int index = r.Next(empt.Count());

                    prev = map[Y, X];
                    X = empt[index].X;
                    Y = empt[index].Y;
                }
            
            game.RefreshEnemy();
        }

        public void CheckPlayer()
        {
            if (pl != null && X == pl.X && Y == pl.Y)
                pl.IsAlive = false;
        }




        #region A*
        public bool FindPlayer(Player pl)
        {
            MinPQ < Step > visited = new MinPQ<Step>(10000000);
            MinPQ<Step> WaitForcheck= new MinPQ<Step>(10000);
            visited.insert(new Step(0, this, null, pl));
            WaitForcheck.insert(new Step(0, this, null, pl));
            bool solved = false;

            Step step = null;
            while (!WaitForcheck.isEmpty())
            {
                step = WaitForcheck.delMin();
                if (step.Equals(pl))
                {
                    solved = true;
                    break;
                }

                foreach (var next in neighb(step.el))
                {
                    if (!Exist(step, next))
                    {
                       WaitForcheck.insert(new Step(step.moves + 1, next, step, pl));
                      visited.insert(new Step(step.moves + 1, next, step,pl));

                   }
                } }

            path = GetPath(step);
            return solved;
        }
        private Stack<Element> GetPath(Step step)
        {
            Stack<Element> res = new Stack<Element>();
            Step cur = step;
            while (cur != null)
            {
                res.Push(cur.el);
                cur = cur.Pstep();
            }
            return res;
        }
        private bool Exist(Step step, Element el)
        {
            Step cur = step;
            while (cur != null)
            {
                if (cur.el.Equals(el))
                    return true;
                cur = cur.Pstep();
            }
            return false;
        }

        private List<Element> neighb(Element el)
        {
            int x = el.GetX();
            int y = el.GetY();
            List<Element> res = new List<Element>();

            if (x -20 >= 0)
                res.Add(map[ y,x - 20]);
            if (x + 20 < map.Columns*20)
                res.Add(map[ y, x + 20]);

            if (y - 20 >= 0)
                res.Add(map[ y - 20,x]);
            if (y + 20 < map.Rows *20)
                res.Add(map[ y + 20,x]);

            return res;

        }
        #endregion
    }
}
