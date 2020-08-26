using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bombermen
{
     public abstract class Cell
    {
        [JsonProperty]
        protected int x;
        [JsonProperty]
        protected int y;
       public char sym;

        protected const int size = 20;
        [JsonIgnore]
        public UIElement Uielement { get; set; }
       public int X {
            get
            {
                return x;
            }
           protected set
            {
                x = value;
            }
                }
        public int Y { get
            {
                return y;
            }
            protected set {
                y = value;
            }
        }
       

        public Cell(int x, int y)

        {
            X = x;
            Y = y;
        }

        public void Set_X(int _x)
        {
            X = _x;
        }
        public void Set_Y(int _y)
        {
            Y = _y;
        }
        public char Get_Sym()
        {
            return sym;
        }
    }
}
