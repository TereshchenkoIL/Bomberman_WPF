using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bombermen
{
    public class World
    {
        public Element[,] world;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Name { get; set; }
        public World()
        {

        }

        public World(Element[,] world, string name)
        {
            this.world = world;
            Name = name;
            Rows = world.GetLength(0);
            Columns = world.GetLength(1);
        }

        public World(int x, int y)
        {
            world = new Element[x, y];
            Rows = world.GetLength(0);
            Columns = world.GetLength(1);
        }

   
        public void SetName(string name)
        {
            Name = name;
        }

        public void Validate()
        {
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j< Columns; j++)
                {
                    if(world[i,j].sym =='o')
                    {
                        world[i, j] = new Wall(world[i, j].X, world[i, j].Y);
                    }
                    if (world[i, j].sym == ' ')
                    {
                        world[i, j] = new Empty_Cell(world[i, j].X, world[i, j].Y);
                    }
                    if (world[i, j].sym == '#')
                    {
                        world[i, j] = new Brick_Wall(world[i, j].X, world[i, j].Y);
                    }
                    if (world[i, j].sym == '&')
                    {
                        world[i, j] = new Finish(world[i, j].X, world[i, j].Y);
                    }
                }
            }
        }
 
        public Element this[int i, int j]
        {
            get
            {
                return world[i/20, j/20];
            }
            set
            {
                world[i/20, j/20] = value;
            }
        }
        public override string ToString()
        {
            return Name;
        }
        public int Sum_Brick()
        {
            int col = 0;
            for (int i = 0; i < world.GetLength(0); i++)
                for (int j = 0; j < world.GetLength(1); j++)
                {
                    if (world[i, j].sym == '#')
                        col++;
                }
            return col;
        }

        public List<Element> GetEmptNeighb(int x, int y)
        {
            if (x > Columns * 20 || y > Rows * 20)
                return null;

            List<Element> res = new List<Element>();

            if(x + 20 < Columns*20)
            {
                if (this[y, x + 20].sym != 'o' && this[y , x+ 20].sym != '#')
                {
                    res.Add(this[y, x + 20]);
                   
                }
            }
             if (y + 20 < Rows * 20)
            {
                if (this[y+20, x].sym != 'o' && this[y + 20, x].sym != '#')
                    res.Add(this[y+20, x]);
            }
             if (x - 20 >= 0)
            {
                if (this[y, x -20].sym != 'o' && this[y , x - 20].sym != '#')
                    res.Add(this[y, x -20]);
            }
            if (y- 20 >= 0)
            {
               if (this[y- 20,x].sym != 'o' && this[y -20, x].sym != '#')
                    res.Add(this[y- 20, x ]);
            }

            return res;
        }


    }
}
