using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
   public interface IShape
    {
        /// <param name="x"></param>
        /// <param name="y"></param>
        void Draw(Graphics G, int x, int y);

        void GetValue(float width, float height, float hypotenus, float radius);
    }
}
