using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
    /// <summary>
    /// circle class
    /// </summary>
    /// <Inheritance cref="IShape"/>
    public class Circle : IShape
    {
        private float rad;

        /// <summary>
        /// shape parameter
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="hypotenus">the hypotenus</param>
        /// <param name="radius">the radius</param>
        public void GetValue(float width, float height, float hypotenus, float radius)
        {
            rad = radius;
        }
        /// <summary>
        /// to draw circle 
        /// </summary>
        /// <param name="G"></param>

        /// <summary>
        /// parameter to draw circle
        /// </summary>

        /// <summary>
        /// circle parameterized constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>

        public void Draw(Graphics G,int x,int y)
        {
            //SolidBrush SB = new SolidBrush(Color.Black);
            G.DrawEllipse(new Pen(Color.Black, 5), x, y, rad, rad);
            //G.FillEllipse(SB, 100, 100, 100, 100);//int.Parse(txtshapesize.text),
            //int.Parse(txtshapesize.Text));

        }
    }
}
