using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace GPL_Appn
{
    /// <summary>
    /// Main Class
    /// </summary>

    public partial class GPL_Appn : Form 
    {
        Graphics G;


        Pen pen;
        Color maincolor;
        private int size;
        /// <summary>
        /// 
        /// </summary>
        public GPL_Appn()
        {
            InitializeComponent();
            G = pictureBox1.CreateGraphics();
            pen = new Pen(maincolor, size);

        }
        private void btnrun_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && !textBox1.Equals(""))
            {
                Validation Val = new Validation(textBox1);
                if (!Val.IsSomethingInvalid)
                {
                    try
                    {
                        Command cmd = new Command();
                        cmd.loadCommand(textBox1,G,pictureBox1);
                    }
                    catch (Exception ex)
                    {
                        txtErrorResult.Text += "\r\n" + ex.ToString();
                    }
                }
                else if (!Val.IsSyntaxValid)
                {
                    txtErrorResult.Text += "\r\n Command Line Syntax Erros.";
                }
                else if (!Val.IsParameterValid)
                {
                    txtErrorResult.Text += "\r\n Invalid Parameters Error.";
                }
                else
                {
                    txtErrorResult.Text += "\r\n Some Error Occured. Click Help Options for checking commands and syntax.";
                }
            }
            else
            {
                txtErrorResult.Text += ("Oops-Please Fill In The Required Command Fields: ");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.WriteLine(textBox1.Text);
                write.Close();
                MessageBox.Show("File Saved Successfully");
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse file from specified folder";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "DOCX files (*.docx)|*.docx|All files (*.*)|*.*";

            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //Browse .txt file from computer
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                //displays the text inside the file on TextBox named as txtInput
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);

            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            txtErrorResult.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            pictureBox1.Refresh();
            txtErrorResult.Text = "";
            G.ResetTransform();
        }
        private void hintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void textBoxResultOutput_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtErrorResult_TextChanged(object sender, EventArgs e)
        {
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void GPL_Appn_Load(object sender, EventArgs e)
        {

        }


        private void btnCircle_Click(object sender, EventArgs e)
        {
            Pen blackPen = new Pen(maincolor, size);

            // Create Circle for ellipse.
            RectangleF rect = new RectangleF(100, 100, 100, 100);

            // Draw ellipse to screen.
            G.DrawEllipse(blackPen, rect);
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                btnCircle.BackColor = colorDialog1.Color;
                maincolor = colorDialog1.Color;
            }
               pen = new Pen(maincolor, size);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            pen = new Pen(maincolor, size);
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtErrorResult.Text = "";
                string Aboutus = "\n Graphical Programming Language Application:" +
                    "\r\n It Provides us with a rich set of namespaces, classes, methods and events for developing " +
                    "applications with graphical capabilities. With the help of its Graphics class, the System.Drawing" +
                    " namespace provides functionality for drawing shapes, printing texts on to the form, and much more." +
                    "\r\n \r\n GPL Application Version v1.1 ";

                txtErrorResult.Text = Aboutus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hintToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                txtErrorResult.Text = "";
                string Guide = "HINT For GPL Commands :" +
                    "\r\n moveto X,Y  =>(Move Postion) " +
                    "\r\n drawto X,Y =>(Move Position)" +
                    "\r\n clear =>(Clear drawing area) " +
                    "\r\n \r\nDraw Commands :\r\n( Circle radius )\r\n( Rectangle Width,height )" +
                    "\r\n(Triangle width,height,hypotenus)" +
                    "\r\n \r\nFor looping: "+
                    "\r\n loop 1"+
                    "\r\n circle 50"+
                    "\r\n endloop"+
                    "\r\n \r\n For if statement:" +
                    "\r\n counter = 1" +
                    "\r\n if counter = 1 then" +
                    "\r\n circle 100" +
                    "\r\n endif";
                txtErrorResult.Text = Guide;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
