using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursa4
{
    internal static class Program
    {
       static string line;
       static StreamReader sr = new StreamReader(@"E:\My1.txt");
       static List<string> ls = new List<string>();

[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
                Console.WriteLine($"x={t.x}, y={t.y}");

        }
    }
class Tochka
{
    public int x { get; set; }
    public int y { get; set; }
}
}
