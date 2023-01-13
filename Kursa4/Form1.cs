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
        Triangle[] triangles;
        public Form1()
        {
            do
            {
                line = sr.ReadLine();
                ls.Add(line);
            } while (line != null);
            sr.Close();
            triangles = new Triangle[ls.Count - 1];
            for (int i = 0; i < triangles.Length; i++)
            {
                char[] seps = { ' ', ':', ',', ';', '=' };
                string[] ms = ls[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);

                triangles[i] = new Triangle() {
                    first = new Point(int.Parse(ms[3]), int.Parse(ms[5])),
                    second = new Point(int.Parse(ms[7]), int.Parse(ms[9])),
                    third = new Point(int.Parse(ms[11]), int.Parse(ms[13])),
                };
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
            public Pen pen = new Pen(Color.Black,3);
            private int lengthSquare(Point p1, Point p2)
            {
                int xDiff = p1.X - p2.X;
                int yDiff = p1.Y - p2.Y;
                return xDiff * xDiff + yDiff * yDiff;
            }
            public float[] Angles()
            {

                
                int a2 = lengthSquare(second, third);
                int b2 = lengthSquare(first, third);
                int c2 = lengthSquare(first, second);

                
                float a = (float)Math.Sqrt(a2);
                float b = (float)Math.Sqrt(b2);
                float c = (float)Math.Sqrt(c2);

                
                float alpha = (float)Math.Acos((b2 + c2 - a2) /
                                                   (2 * b * c));
                float betta = (float)Math.Acos((a2 + c2 - b2) /
                                                   (2 * a * c));
                float gamma = (float)Math.Acos((a2 + b2 - c2) /
                                                   (2 * a * b));

                
                alpha = (float)(alpha * 180 / Math.PI);
                betta = (float)(betta * 180 / Math.PI);
                gamma = (float)(gamma * 180 / Math.PI);


                float[] angles = {alpha,betta,gamma};

                Array.Sort(angles);
                angles = new float[]{angles[0],angles[2]};
                
                return angles;
            }
        }
        private void AcuteAngles(Triangle[] tris)
        {
            Triangle temp;
            Pen greenPen = new Pen(Color.Green,2);

            for (int i = 0; i < tris.Length - 1; i++)
            {
                for (int j = i + 1; j < tris.Length; j++)
                {
                    if (tris[i].Angles()[0] > tris[j].Angles()[0])
                    {
                        temp = tris[i];
                        tris[i] = tris[j];
                        tris[j] = temp;
                    }
                }
            }
            tris[0].pen = greenPen;
            tris[1].pen = greenPen;
            MessageBox.Show("Angle_Accute_1: " + tris[tris.Length - 1].Angles()[0]);
            MessageBox.Show("Angle_Accute_2: " + tris[tris.Length - 2].Angles()[0]);
        }
        private void ObtuseAngles(Triangle[] tris)
        {
            Triangle temp;
            Pen redPen = new Pen(Color.Red,2);

            for (int i = 0; i < tris.Length - 1; i++)
            {
                for (int j = i + 1; j < tris.Length; j++)
                {
                    if (tris[i].Angles()[1] > tris[j].Angles()[1])
                    {
                        temp = tris[i];
                        tris[i] = tris[j];
                        tris[j] = temp;
                    }
                }
            }

            tris[tris.Length - 1].pen = redPen;
            tris[tris.Length - 2].pen = redPen;
            MessageBox.Show("Angle_Obtuse_1: " + tris[tris.Length - 1].Angles()[1]);
            MessageBox.Show("Angle_Obtuse_2: " + tris[tris.Length - 2].Angles()[1]);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = panel1.CreateGraphics();
            
            AcuteAngles(triangles);
            ObtuseAngles(triangles);

            foreach (Triangle t in triangles)
            {
                //Console.WriteLine($"x={t.x}, y={t.y}");
                //finalLine += "Point: X1: "+t.first.X+" Y1: "+t.first.Y+ " X2: " + t.second.X + " Y2: " + t.second.Y + " X3: " + t.third.X + " Y3: " + t.third.Y + "\n";
                
                Point[] coordinates = { t.first, t.second, t.third };
                gr.DrawPolygon(t.pen, coordinates);
            }
        }

    }
}
