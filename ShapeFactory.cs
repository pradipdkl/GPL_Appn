using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Appn
{
    public class ShapeFactory
    {
        public IShape GetShape(string ShapeTypes)
        {
            if(ShapeTypes==null)
            {
                return null;
            }
            if(ShapeTypes==("Circle").ToLower())
            {
                return new Circle();
            }
            if(ShapeTypes==("Rectangle").ToLower())
            {
                return new Rectangle();
            }
            if(ShapeTypes==("Triangle").ToLower())
            {
                return new Triangle();
            }
            return null;
        }
    }
}
