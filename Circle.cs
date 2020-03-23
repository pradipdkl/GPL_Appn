using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
    public class Circle : IShape
    {
        private float widt;
        private float high;
        private float hypo;
        private float rad;

        public void GetValue(float width, float height, float hypotenus, float radius)
        {
            widt = width;
            high = height;
            hypo = hypotenus;
            rad = radius;
        }

        public void Draw(Graphics G,int x,int y)
        {
            //SolidBrush SB = new SolidBrush(Color.Black);
            G.DrawEllipse(new Pen(Color.Black, 5), x, y, rad, rad);
            //G.FillEllipse(SB, 100, 100, 100, 100);//int.Parse(txtshapesize.text),int.Parse(txtshapesize.Text));

        }
    }
}
