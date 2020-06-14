using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GPL_Appn
{
    /// <summary>
    /// default constructor
    /// </summary>
    public interface IShape
    {
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <summary>
        /// draw method
        /// </summary>
        /// <param name="G">Graphics</param>
        void Draw(Graphics G, int x, int y);
        /// <summary>Sets the shape parameter.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="hypotenus">The hypotenus</param>
        /// <param name="radius">The Radius.</param>
        void GetValue(float width, float height, float hypotenus, float radius);
    }
}
