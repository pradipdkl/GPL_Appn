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
    public partial class GPL_Appn : Form 
    {
        Graphics G;
        public GPL_Appn()
        {
            InitializeComponent();
            G = pictureBox1.CreateGraphics(); 
        }
        private void btnrun_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && !textBox1.Text.Equals(""))
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
                else if (!Val.isSyntaxValid)
                {
                    txtErrorResult.Text += "\r\n Command Line Syntax Erros.";
                }
                else if (!Val.IsParameterValid)
                {
                    txtErrorResult.Text += "\r\n Invalid Parameters Error.";
                }
                else
                {
                    txtErrorResult.Text += "\r\n Some Error Occured";
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
            Guide GD = new Guide();
            GD.ShowDialog();
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
    }
}
