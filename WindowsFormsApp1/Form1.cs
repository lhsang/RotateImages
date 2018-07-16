using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string imageName = "";
        string newName = "";
        static string typeImage = "";

        string[] filename;
        string [] dir;

        string fileSelected;
        public Form1()
        {
            InitializeComponent();
            //listBox1.BackColor = Color.Gray;
            txtAngle.Text = "90";
            comboBox1.Items.Add("jpg");
            comboBox1.Items.Add("PNG");
            comboBox1.Items.Add("BMP");
            comboBox1.SelectedIndex = 0;
        }
        public bool check(string path)
        {
            string temp = "";
            for (int i = path.Length - 1; i >= 0; i--)
                if (path[i] != '.')
                    temp = path[i] + temp;
                else
                {
                    if (temp == "jpg" || temp == "PNG" || temp == "BMP")
                        return true;
                    else return false;
                }
            return false;
        }
        public Bitmap RotateImage1(Bitmap b, float angle)
        {
            if (angle > 0)
            {
                int l = b.Width;
                int h = b.Height;
                double an = angle * Math.PI / 180;
                double cos = Math.Abs(Math.Cos(an));
                double sin = Math.Abs(Math.Sin(an));
                int nl = (int)(l * cos + h * sin);
                int nh = (int)(l * sin + h * cos);
                Bitmap returnBitmap = new Bitmap(nl, nh);

                Graphics g = Graphics.FromImage(returnBitmap);
                g.TranslateTransform((float)(nl - l) / 2, (float)(nh - h) / 2);
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                g.DrawImage(b, new Point(0, 0));
                return returnBitmap;
            }
            else return b;
        }
      
        public string SplitName(string path)
        {
            string ans = "";int index = 0;
            for(int i=0;i<path.Length;i++)
                if (path[i] == '\\')
                    index = i;
            for (int i = index + 1; i < path.Length; i++)
                if (path[i] == '.')
                    break;
                else ans += path[i];
            return ans;
        }
        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                typeImage = '.' + comboBox1.SelectedItem.ToString();

                string angle = txtAngle.Text.ToString();
                float _angle = float.Parse(angle);

                string pre = txtPreName.Text.ToString();

                if (filename == null)
                    MessageBox.Show("Please select files");
                else
                {
                    if (typeImage == ".")
                        MessageBox.Show("Please choose format");
                    else
                    {
                        foreach (string name in filename)
                        {
                            if(check(name))
                            {
                                Image image = Image.FromFile(name, true);
                                Bitmap image1 = (Bitmap)image;
                                imageName = SplitName(name);

                                string temp = "";
                                if (pre != "" || pre != " ")
                                    temp = imageName + pre + typeImage;
                                else temp = imageName + typeImage;
                                Image ans = RotateImage1(image1, _angle);
                                ans.Save(temp);
                            }
                        }
                        MessageBox.Show("Rotated !");
                    }

                }
            }else MessageBox.Show("Please choose format");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = null;
                filename = Directory.GetFiles(folder.SelectedPath);
            }


            pushToListbox();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog fileChoose = new OpenFileDialog();
            fileChoose.Filter = "All Files (*.*)|*.*";
            fileChoose.FilterIndex = 1;
            fileChoose.Multiselect = true;
            if (fileChoose.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = fileChoose.FileNames;
                pushToListbox();
            }
        }
        public void pushToListbox()
        {
            listBox1.Items.Clear();
            if (filename != null)
                foreach (string file in filename)
                    listBox1.Items.Add(file);
        }
    }
}
