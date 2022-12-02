using kilencedikhet.Entities;
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

namespace kilencedikhet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Population = PLoad(@"C:\Temp\nép.csv");
            BirthProbabilities = BPLoad(@"C:\Temp\születés.csv");
            DeathProbabilities = DPLoad(@"C:\Temp\halál.csv");

            //dataGridView1.DataSource = Population;
        }

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        public List<Person> PLoad(string file)
        {
            List<Person> population = new List<Person>();

            StreamReader f = new StreamReader(file, Encoding.Default);
            while (!f.EndOfStream)
            {
                var sor = f.ReadLine().Split(';');
                Person p = new Person
                {
                    BirthYear = int.Parse(sor[0]),
                    Gender = (Gender)Enum.Parse(typeof(Gender), sor[1]),
                    NbrOfChildren = int.Parse(sor[2])
                };
                population.Add(p);
            }

            f.Close();
            return population;
        }
        public List<BirthProbability> BPLoad(string file)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            StreamReader f = new StreamReader(file, Encoding.Default);
            while (!f.EndOfStream)
            {
                var sor = f.ReadLine().Split(';');
                BirthProbability bp = new BirthProbability
                {
                    Age = int.Parse(sor[0]),
                    NbrOfChildren = int.Parse(sor[1]),
                    P = double.Parse(sor[2])
                };
                birthProbabilities.Add(bp);
            }

            f.Close();
            return birthProbabilities;
        }
        public List<DeathProbability> DPLoad(string file)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            StreamReader f = new StreamReader(file, Encoding.Default);
            while (!f.EndOfStream)
            {
                var sor = f.ReadLine().Split(';');
                DeathProbability p = new DeathProbability
                {
                    Gender = (Gender)Enum.Parse(typeof(Gender), sor[0]),
                    Age = int.Parse(sor[1]),
                    P = double.Parse(sor[2])
                };
                deathProbabilities.Add(p);
            }

            f.Close();
            return deathProbabilities;
        }
    }
}
