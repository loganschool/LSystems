using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSystemsGo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Print(string s)
        {
            tbxOutput.AppendText(s + "\r\n");
        }

        void Clear()
        {
            tbxOutput.Clear();
        }
        Hashtable rules;
        Hashtable[] hashLevels;

        void PopulateHashtables(int depth)
        {
            hashLevels = new Hashtable[depth];
            for (int i = 0; i < depth; i++)
            {
                hashLevels[i] = new Hashtable();
            }
        }

        ulong AxiomLength(string axiom, int depth)
        {
            Hashtable memo = new Hashtable();
            PopulateHashtables(depth);

            ulong sum = 0;
            foreach (char c in axiom)
            {
                if (hashLevels[depth-1].ContainsKey(c))
                {
                    sum += (ulong)hashLevels[depth-1][c];
                }
                else
                {
                    ulong length = characterLength(depth-1, c);
                    sum += length;
                    hashLevels[depth-1].Add(c, length);
                }
                
            }
            return sum;
        }

        ulong characterLength(int depth, char c)
        {
            if (depth == 0)
            {
                return 1;
            }



            string replace = (string)rules[c];

            ulong sum = 0;

            foreach (char current in replace)
            {
                if (hashLevels[depth - 1].Contains(current))
                {
                    sum += (ulong)hashLevels[depth - 1][current];
                }
                else
                {
                    ulong length = characterLength(depth - 1, current);
                    sum += length;
                    hashLevels[depth - 1].Add(current, length);
                }
            }

            return sum;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\s299128\Desktop\DATA21.txt";
            string[] data = System.IO.File.ReadAllLines(path);

            int counter = 0;
            for (int Case = 0; Case < 10; Case++)
            {

                string[] firstLine = data[counter].Split(' ');
                int numRules = Convert.ToInt32(firstLine[0]);
                int numIterations = Convert.ToInt32(firstLine[1]);


                string axiom = firstLine[2];

                rules = new Hashtable();

                for (int i = 1; i < numRules + 1; i++)
                {
                    string[] line = data[counter + i].Split(' ');

                    char c = line[0][0];

                    string rule = line[1];

                    rules.Add(c, rule);
                }

                ulong answer = AxiomLength(axiom, numIterations);

                Print(Convert.ToString(answer));
                
                counter += 1 + numRules;
            }

        }
    }
}
