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
    /// <summary>
    /// Command class
    /// </summary>
    public class Command: GPL_Appn
    {
        int mouseX;
        int mouseY;
        int xax;
        int yax;
        /// <summary>
        /// 
        /// </summary>
        public int width = 0;
        /// <summary>
        /// 
        /// </summary>
        public int radius = 0;
        /// <summary>
        /// 
        /// </summary>
        public int height = 0;
        /// <summary>
        /// 
        /// </summary>
        public int hypotenus = 0;
        /// <summary>
        /// 
        /// </summary>
        public int counter = 0;
        /// <summary>
        /// 
        /// </summary>
        public int loopnumber = 0;

        /// <summary>
        /// 
        /// </summary>
        public int dsize = 0;

        TextBox textBox;
        PictureBox pnldraw;
        Graphics graph;

        String[] command = { "moveto", "drawto", "clear", "reset", "loop", "endloop", "if", "endif" };
        String[] variables = { "width", "height", "radius", "hypotenus" };
        String[] shapes = { "circle", "rectangle", "triangle","counter", "hypotenus", "size" };
/// <summary>
/// 
/// </summary>
/// <param name="textBoxCmd"></param>
/// <param name="graph"></param>
/// <param name="panelDraw"></param>
        public void loadCommand(TextBox textBoxCmd, Graphics graph, PictureBox panelDraw)
        {
            this.textBox = textBoxCmd;
            this.pnldraw = panelDraw;
            this.graph = graph;

            int numberOfLines = textBoxCmd.Lines.Length;
            for (loopnumber = 0; loopnumber < numberOfLines; loopnumber++)
            {
                String oneLineCommand = textBoxCmd.Lines[loopnumber];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    RunCommand(oneLineCommand);
                }
            }
        }
        /// <summary>
        /// function for running loop statement commands.
        /// </summary>
        /// <param name="singleLineCommand"></param>
        private void RunCommand(String singleLineCommand)
        {
            ShapeFactory sf = new ShapeFactory();
            Boolean hasEquals = singleLineCommand.Contains("=");
            Boolean hasplus = singleLineCommand.Contains("+");
            singleLineCommand = Regex.Replace(singleLineCommand, @"\s+", " ");
            if (hasEquals)
            {
                string[] words = singleLineCommand.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }
                String firstWord = words[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (words[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (words[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(words[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (words[1].ToLower().Equals("hypotenus"))
                    {
                        if (hypotenus == int.Parse(words[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (words[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(words[3]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopnumber = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand1 = textBox.Lines[j];
                            oneLineCommand1 = oneLineCommand1.Trim();
                            if (!oneLineCommand1.Equals(""))
                            {
                                RunCommand(oneLineCommand1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("If Statement is false");
                    }
                }
                else
                {
                    string[] words2 = singleLineCommand.Split('=');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("hypotenus"))
                    {
                        hypotenus = int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("counter"))
                    {
                        counter = int.Parse(words2[1]);
                    }
                }
            }
            else if (hasplus)
            {
                singleLineCommand = System.Text.RegularExpressions.Regex.Replace(singleLineCommand, @"\s+", " ");
                string[] words = singleLineCommand.Split(' ');
                if (words[0].ToLower().Equals("repeat"))
                {
                    counter = int.Parse(words[1]);
                    if (words[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = GetSize(singleLineCommand);
                        radius = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            IShape sh = sf.GetShape("circle");
                            sh.GetValue(0, 0, 0, radius);
                            sh.Draw(graph, 0, 0);
                            radius += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = GetSize(singleLineCommand);
                        dsize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            IShape shc = sf.GetShape("rectangle");
                            shc.GetValue(dsize, dsize, 0, 0);
                            shc.Draw(graph, 0, 0);
                            dsize += increaseValue;
                        }
                    }
                    else if (words[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = GetSize(singleLineCommand);
                        dsize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            IShape shp = sf.GetShape("triangle");
                            shp.GetValue(dsize, dsize, dsize, 0);
                            shp.Draw(graph, 0, 0);
                            dsize += increaseValue;
                        }
                    }

                }
                else
                {
                    string[] words2 = singleLineCommand.Split('+');
                    for (int j = 0; j < words2.Length; j++)
                    {
                        words2[j] = words2[j].Trim();
                    }
                    if (words2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(words2[1]);
                    }
                    else if (words2[0].ToLower().Equals("hypotenus"))
                    {
                        hypotenus += int.Parse(words2[1]);
                    }
                }
            }
            else
            {
                Shapecommand(singleLineCommand);
            }
        }
        /// <summary>
        /// Get the size for loop functions. 
        /// </summary>
        /// <param name="lineCommand"></param>
        /// <returns></returns>
        private int GetSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }

        /// <summary>
        /// Commandline
        /// </summary>
        /// <param name="textcmd"></param>
        public void Shapecommand(string textcmd)
        {
            try
            {
                textcmd = Regex.Replace(textcmd, @"\s+", " ");
                string[] words = textcmd.Split(' ');
                //helps to remove white lines between words.
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }
                String firstWord = words[0].ToLower();
                Boolean firstWordCommand = command.Contains(firstWord);
                Boolean firstWordShape = shapes.Contains(firstWord);

                ShapeFactory sf = new ShapeFactory();
                if (firstWordShape)
                {
                    if (firstWord.ToLower().Equals("circle"))
                    {
                        Boolean secondWordIsVariable = variables.Contains(words[1].ToLower());
                        if (secondWordIsVariable)
                        {
                            if (words[1].ToLower().Equals("radius"))
                            {
                                IShape sh = sf.GetShape("circle");
                                sh.GetValue(0, 0, 0, radius);
                                sh.Draw(graph, 0, 0);
                            }
                        }
                        else
                        {
                            IShape sh = sf.GetShape("circle");
                            sh.GetValue(0, 0, 0, float.Parse(words[1]));
                            sh.Draw(graph, 0, 0);
                        }
                    }

                    else if (firstWord.ToLower().Equals("rectangle"))
                    {
                        String cmdargs = textcmd.Substring(9, (textcmd.Length - 9));
                        String[] parameters = cmdargs.Split(',');
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i] = parameters[i].Trim();
                        }
                        Boolean secondWordIsVariable = variables.Contains(parameters[0].ToLower());
                        Boolean thirdWordIsVariable = variables.Contains(parameters[1].ToLower());
                        if (secondWordIsVariable)
                        {
                            if (thirdWordIsVariable)
                            {
                                IShape shc = sf.GetShape("rectangle");
                                shc.GetValue(width, height, 0, 0);
                                shc.Draw(graph, 0, 0);
                            }
                            else
                            {
                                IShape shc = sf.GetShape("rectangle");
                                shc.GetValue(float.Parse(parameters[0]), float.Parse(parameters[1]), 0, 0);
                                shc.Draw(graph, 0, 0);
                            }
                        }
                        else
                        {
                            if (thirdWordIsVariable)
                            {

                                IShape shc = sf.GetShape("rectangle");
                                shc.GetValue(width, height, 0, 0);
                                shc.Draw(graph, 0, 0);
                            }
                            else
                            {
                                IShape shc = sf.GetShape("rectangle");
                                shc.GetValue(float.Parse(parameters[0]), float.Parse(parameters[1]), 0, 0);
                                shc.Draw(graph, 0, 0);
                            }
                        }

                    }
                    else if (firstWord.ToLower().Equals("triangle"))
                    {
                        String cmdargs = textcmd.Substring(8, (textcmd.Length - 8));
                        String[] parameters = cmdargs.Split(',');
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i] = parameters[i].Trim();
                        }
                        IShape shp = sf.GetShape("triangle");
                        shp.GetValue(float.Parse(parameters[0]), float.Parse(parameters[1]), float.Parse(parameters[2]), 0);
                        shp.Draw(graph, 0, 0);
                    }
                }
                else if (firstWordCommand)
                {
                    if (firstWord.ToLower().Equals("moveto"))
                    {
                        String cmdargs = textcmd.Substring(6, (textcmd.Length - 6));
                        String[] parameters = cmdargs.Split(',');
                        for (int l = 0; l < parameters.Length; l++)
                        {
                            parameters[l] = parameters[l].Trim();
                        }
                        mouseX = int.Parse(parameters[0]);
                        mouseY = int.Parse(parameters[1]);
                        graph.TranslateTransform(mouseX, mouseY);
                    }

                    else if (firstWord.ToLower().Equals("drawto"))
                    {
                        String cmdargs = textcmd.Substring(6, (textcmd.Length - 6));
                        String[] parameters = cmdargs.Split(',');
                        for (int m = 0; m < parameters.Length; m++)
                        {
                            parameters[m] = parameters[m].Trim();
                        }
                        xax = int.Parse(parameters[0]);
                        yax = int.Parse(parameters[1]);
                        graph.TranslateTransform(xax, yax);
                    }
                    else if (firstWord.ToLower().Equals("clear"))
                    {
                        pnldraw.Refresh();
                    }
                    else if (firstWord.ToLower().Equals("reset"))
                    {
                        graph.ResetTransform();
                    }
                    else if (firstWord.ToLower().Equals("loop"))
                    {
                        counter = int.Parse(words[1]);
                        int loopStartLine = (GetLoopStartLineNumber());
                        int loopEndLine = (GetLoopEndLineNumber() - 1);
                        loopnumber = loopEndLine;
                        for (int i = 0; i < counter; i++)
                        {
                            for (int j = loopStartLine; j <= loopEndLine; j++)
                            {
                                String oneLineCommand = textBox.Lines[j];
                                oneLineCommand = oneLineCommand.Trim();
                                if (!oneLineCommand.Equals(""))
                                {
                                    RunCommand(oneLineCommand);
                                }
                            }
                        }
                    }
                    else if (firstWord.ToLower().Equals("if"))
                    {
                        Boolean loop = false;
                        if (words[1].ToLower().Equals("radius"))
                        {
                            if (radius == int.Parse(words[1]))
                            {
                                loop = true;
                            }
                        }
                        else if (words[1].ToLower().Equals("width"))
                        {
                            if (width == int.Parse(words[1]))
                            {
                                loop = true;
                            }
                        }
                        else if (words[1].ToLower().Equals("height"))
                        {
                            if (height == int.Parse(words[1]))
                            {
                                loop = true;
                            }

                        }
                        else if (words[1].ToLower().Equals("hypotenus"))
                        {
                            if (hypotenus == int.Parse(words[1]))
                            {
                                loop = true;
                            }

                        }
                        else if (words[1].ToLower().Equals("counter"))
                        {
                            if (counter == int.Parse(words[1]))
                            {
                                loop = true;
                            }
                        }
                        int ifStartLine = (GetIfStartLineNumber());
                        int ifEndLine = (GetEndifEndLineNumber() - 1);
                        loopnumber = ifEndLine;
                        if (loop)
                        {
                            for (int j = ifStartLine; j <= ifEndLine; j++)
                            {
                                String oneLineCommand = textBox.Lines[j];
                                oneLineCommand = oneLineCommand.Trim();
                                if (!oneLineCommand.Equals(""))
                                {
                                    RunCommand(oneLineCommand);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int GetEndifEndLineNumber()
        {
            int numberOfLines = textBox.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = textBox.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetIfStartLineNumber()
        {
            int numberOfLines = textBox.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = textBox.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');

                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                String firstWord = words[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.ToLower().Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
        private int GetLoopEndLineNumber()
        {
            int numberOfLines = textBox.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = textBox.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endloop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetLoopStartLineNumber()
        {
            int numberOfLines = textBox.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = textBox.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');
                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                String firstWord = words[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.ToLower().Equals("loop"))
                {
                    lineNum = i + 1;
                }
            }
            return lineNum;
        }
    }
}

