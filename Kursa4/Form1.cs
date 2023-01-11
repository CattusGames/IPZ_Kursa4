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
        StreamReader sr = new StreamReader(@"C:\Users\taras\OneDrive\Документы\GitHub\IPZ_Kursa4\coords.txt");
        List<string> ls = new List<string>();
        string finalLine;
        public Form1()
        {
            do
            {
                line = sr.ReadLine();
                ls.Add(line);
            } while (line != null);
            sr.Close();
            Tochka[] mt = new Tochka[ls.Count - 1];
            for (int i = 0; i < mt.Length; i++)
            {
                char[] seps = { ' ', ':', ',', ';', '=' };
                string[] ms = ls[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);
                mt[i] = new Tochka() { x = int.Parse(ms[3]), y = int.Parse(ms[5]) };
            }
            foreach (Tochka t in mt)
            {
                //Console.WriteLine($"x={t.x}, y={t.y}");
                finalLine += "Point: X: "+t.x+" Y: "+t.y+" \n";
            }
            InitializeComponent();
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = finalLine;
        }
        class Tochka
        {
            public int x { get; set; }
            public int y { get; set; }
        }
    }
}
