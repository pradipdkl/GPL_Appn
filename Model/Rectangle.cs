using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
    /// <summary>
    /// Recatngle 
    /// </summary>
   public class Rectangle:IShape
    {
        /// <summary>The width</summary>
        private float widt;
        /// <summary>The height</summary>
        private float high;
        /// <summary>
        /// parameter for rectangle
        /// </summary>

        /// <summary>Sets the shape parameter.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="hypotenus"></param>
        /// <param name="radius">The Radius.</param>
        public void GetValue(float width, float height, float hypotenus, float radius)
        {
            widt = width;
            high = height;
        }
        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="G"></param>
        /// <param name="x">An integer.</param>
        /// <param name="y">An integer.</param>
        public void Draw(Graphics G, int x,int y)
        {
            //SolidBrush sb = new SolidBrush(Color.Red);
            //g.FillRectangle(sb, x, y, 2 *fl,fl);
            G.DrawRectangle(new Pen(Color.Black, 5), x, y, widt, high);
        }
    }
}
