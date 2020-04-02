using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
   public class Rectangle:IShape
    {
        private float widt;
        private float high;
        /// <summary>
        /// parameter for rectangle
        /// </summary>
        public void GetValue(float width, float height, float hypotenus, float radius)
        {
            widt = width;
            high = height;
        }
        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="G"></param>
        public void Draw(Graphics G, int x,int y)
        {
            //SolidBrush sb = new SolidBrush(Color.Red);
            //g.FillRectangle(sb, x, y, 2 *fl,fl);
            G.DrawRectangle(new Pen(Color.Black, 5), x, y, widt, high);
        }
    }
}
