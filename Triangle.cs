using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GPL_Appn
{
    /// <summary>
    /// Triangle 
    /// </summary>
    /// <inheritdoc cref="IShape"/>
    public class Triangle : IShape
    {
        private float widt;
        private float high;
        private float hypo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="hypotenus"></param>
        /// <param name="radius"></param>
        public void GetValue(float width,float height, float hypotenus, float radius)
        {
            widt = width;
            high = height;
            hypo = hypotenus;
        }
        /// <summary>
        /// Check triangle exist or not.
        /// </summary>
        /// <returns>result true or false.</returns>
        public Boolean chkTriangleValidity()
        {
            //Condition valid or not
            if (widt + high <= hypo || widt + hypo <= high || high + hypo <= widt)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="G"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(Graphics G, int x, int y)
        {
            if (chkTriangleValidity())
            {
                Pen myPen = new Pen(Color.Black, 5);
                Point[] pot = new Point[3];

                pot[0].X = x;
                pot[0].Y = y;

                pot[1].X = Convert.ToInt32(x - widt);
                pot[1].Y = y;

                pot[2].X = x;
                pot[2].Y = Convert.ToInt32(y - high);

                G.DrawPolygon(myPen, pot);

            } 
            else
            {
                MessageBox.Show("Value not provided properly");
                /*
                Pen drawingPen = new Pen(Brushes.Black, 5);
                g.DrawLine(drawingPen, new Point(0, 50), new Point(50, 0));
                g.DrawLine(drawingPen, new Point(50, 0), new Point(50, 100));
                g.DrawLine(drawingPen, new Point(50, 100), new Point(0, 50));*/

            }
        }
    }
}
