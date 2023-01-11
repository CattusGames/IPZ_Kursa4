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

namespace Kursa4
{
    public partial class Form1 : Form
    {
        string line;
        StreamReader sr = new StreamReader(@"C:\Users\ASUS\Desktop\Git\Lab_Web\IPZ_Kursa4\coords.txt");
        List<string> ls = new List<string>();
        string finalLine;
        
        public Form1()
        {
            Graphics gr = CreateGraphics();

            do
            {
                line = sr.ReadLine();
                ls.Add(line);
            } while (line != null);
            sr.Close();
            Triangle[] mt = new Triangle[ls.Count - 1];
            for (int i = 0; i < mt.Length; i++)
            {
                char[] seps = { ' ', ':', ',', ';', '=' };
                string[] ms = ls[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);
                
                mt[i] = new Triangle() {
                    first = new Point(int.Parse(ms[3]), int.Parse(ms[5])),
                    second = new Point(int.Parse(ms[7]), int.Parse(ms[9])),
                    third = new Point(int.Parse(ms[11]), int.Parse(ms[13])),
                };
            }
            foreach (Triangle t in mt)
            {
                //Console.WriteLine($"x={t.x}, y={t.y}");
                finalLine += "Point: X1: "+t.first.X+" Y1: "+t.first.Y+ " X2: " + t.second.X + " Y2: " + t.second.Y + " X3: " + t.third.X + " Y3: " + t.third.Y + "\n";
            }
            
            InitializeComponent();
        }
        
        public void textBox1_TextChanged(object sender, EventArgs e)
        {

            textBox1.Text = finalLine;
        }
        public class Triangle
        {
            public Point first { get; set; }
            public Point second { get; set; }
            public Point third { get; set; }
        }

    }
}
