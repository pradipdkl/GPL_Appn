using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GPL_Appn
{
    public class Command
    {
        int mouseX;
        int mouseY;
        int xax;
        int yax;

        String[] command = { "moveto", "drawto" };
        //String[] variables = { "width", "height", "radius", "hypotenus" };
        String[] shapes = { "circle", "rectangle", "triangle" };

        public void Commandline(string textcmd, Graphics G)
        {
            try
            {
                textcmd = Regex.Replace(textcmd, @"\s+", " ");
                string[] words = textcmd.Split(' ');
                //white spaces removed in between words
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }
                String firstWord = words[0].ToLower();
                Boolean firstWordcom = command.Contains(firstWord);
                if (firstWordcom)
                {
                    if (firstWord == "moveto")
                    {
                        String args = textcmd.Substring(6, (textcmd.Length - 6));
                        String[] parms = args.Split(',');
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                        }
                        mouseX = int.Parse(parms[0]);
                        mouseY = int.Parse(parms[1]);
                        G.TranslateTransform(mouseX, mouseY);
                    }
                    else if (firstWord == "drawto")
                    {
                        String args = textcmd.Substring(6, (textcmd.Length - 6));
                        String[] parms = args.Split(',');
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                        }
                        xax = int.Parse(parms[0]);
                        yax = int.Parse(parms[0]);
                        G.TranslateTransform(xax, yax);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Shapecommand(string textcmd, Graphics G)
        {
            try
            {
                textcmd = Regex.Replace(textcmd, @"\s+", " ");
                string[] words = textcmd.Split(' ');
                //helps to remove white lines between words.
                for(int i=0;i<words.Length;i++)
                {
                    words[i] = words[i].Trim();
                }
                String firstWord = words[0].ToLower();
                Boolean firstWordShape = shapes.Contains(firstWord);
                ShapeFactory sf = new ShapeFactory();
                if (firstWordShape)
                {
                    if (firstWord == "circle")
                    {
                        float secondwordvariable = float.Parse(words[1]);
                        IShape sh = sf.GetShape("circle");
                        sh.GetValue(0, 0, 0, secondwordvariable);
                        sh.Draw(G, xax, yax);
                    }
                    else if (firstWord == "rectangle")
                    {
                        String args = textcmd.Substring(9, (textcmd.Length - 9));
                        String[] parms = args.Split(',');
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                        }

                        float secondvariable = float.Parse(parms[0]);
                        float thirdvariable = float.Parse(parms[1]);

                        IShape sha = sf.GetShape("rectangle");
                        sha.GetValue(secondvariable, thirdvariable, 0, 0);
                        sha.Draw(G, xax, yax);
                    }
                    if (firstWord == "triangle")
                    {
                        String args = textcmd.Substring(8, (textcmd.Length - 8));
                        String[] parms = args.Split(',');
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                        }
                        float secondvr = float.Parse(parms[0]);
                        float thirdvr = float.Parse(parms[1]);
                        float fourthvr = float.Parse(parms[2]);

                        IShape shp = sf.GetShape("triangle");
                        shp.GetValue(secondvr, thirdvr, fourthvr, 0);
                        shp.Draw(G, xax, yax);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}
