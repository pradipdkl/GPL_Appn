using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
    public class Triangle: Command, IShape
    {
        private float widt;
        private float high;
        private float hypo;

        public void GetValue(float width,float height, float hypotenus, float radius)
        {
            widt = width;
            high = height;
            hypo = hypotenus;
        }

        public Boolean chkTriangleValidity()
        {
            //Condition valid or not
            if (widt + high <= hypo || widt + hypo <= high || high + hypo <= widt)
                return false;
            else
                return true;
        }
        public void Draw(Graphics G, int x,int y)
        {
            if(chkTriangleValidity())
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

                /*
                Pen drawingPen = new Pen(Brushes.Black, 5);
                g.DrawLine(drawingPen, new Point(0, 50), new Point(50, 0));
                g.DrawLine(drawingPen, new Point(50, 0), new Point(50, 100));
                g.DrawLine(drawingPen, new Point(50, 100), new Point(0, 50));*/

            }
        }
    }
}
