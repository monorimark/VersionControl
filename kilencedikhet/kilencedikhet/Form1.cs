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
            Population = PLoad(txtFile.Text);
            BirthProbabilities = BPLoad(@"C:\Temp\születés.csv");
            DeathProbabilities = DPLoad(@"C:\Temp\halál.csv");
        }

        private void Simulation()
        {
            for (int year = 2005; year <= numericMaxYear.Value; year++) //évek
            {
                for (int i = 0; i < Population.Count; i++) //népesség öszes egyede
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsALive
                                  select x).Count();

                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsALive
                                    select x).Count();

                //Console.WriteLine(string.Format("Év: {0}, Férfi: {1} Nő: {2}", year, nbrOfMales, nbrOfFemales));
            }
        }

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        Random rng = new Random(1234);

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
        public void SimStep(int year, Person person)
        {
            if (!person.IsALive) return; //él-e még

            byte age = (byte)(year - person.BirthYear); //életkor

            double pD = (from x in DeathProbabilities           //halálozás valószínűsége
                         where x.Gender == person.Gender && x.Age == age
                         select x.P).FirstOrDefault();

            if (rng.NextDouble() <= pD)  //elhalálozás
            {
                person.IsALive = false;
            }

            if (person.IsALive && person.Gender == Gender.Female) //ha él és nő
            {
                double pB = (from x in BirthProbabilities   //szülés valószínűsége
                             where x.Age == age
                             select x.P).FirstOrDefault();

                if (rng.NextDouble() <= pB)
                {
                    Person gyermek = new Person();
                    gyermek.BirthYear = year;
                    gyermek.NbrOfChildren = 0;
                    gyermek.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(gyermek);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Simulation();
            DisplayResult();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
            }
        }

        private void DisplayResult()
        {
            for (int year = 2005; year <= numericMaxYear.Value; year++)
            {
                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsALive
                                  select x).Count();

                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsALive
                                    select x).Count();
                richTextBox1.Text +=
                    string.Format("Szimulációs év: {0}\n \tFérfi: {1}\n \tNő: {2}\n", year, nbrOfMales, nbrOfFemales);
            }
        }
    }
}
