using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
namespace zad109
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Dictionary<string, string> fastaSequences = new Dictionary<string, string>();
        private void button1_Click(object sender, EventArgs e)
        {
            // Wyświetlenie okna dialogowego wyboru pliku CSV
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pliki fasta (*.fasta)|*.fasta|Wszystkie pliki (*.*)|*.*";
            openFileDialog1.Title = "Wybierz plik CSV do wczytania";
            openFileDialog1.ShowDialog();
            // Jeśli użytkownik wybierze plik i zatwierdzi, wczytaj dane z pliku CSV
            if (openFileDialog1.FileName != "")
            {

                string currentHeader = null;
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(">"))
                        {
                            currentHeader = line.Substring(1).Trim();
                            if (!fastaSequences.ContainsKey(currentHeader))
                            {
                                fastaSequences[currentHeader] = "";
                            }
                        }
                        else if (currentHeader != null)
                        {
                            fastaSequences[currentHeader] += line.Trim();
                        }
                    }
                }
            }
            analize();


        }
        List<Dictionary<string, string>> sekwencjeidane = new List<Dictionary<string, string>>();
        void analize()
        {
            int i = 0;
            foreach (var entry in fastaSequences)
            {
                sekwencjeidane.Add(new Dictionary<string, string>());
                sekwencjeidane[i]["nazwa"] = entry.Key;
                sekwencjeidane[i]["wartosc"] = entry.Value;

                int[] tab = new int[5];
                for (int j = 0; j < entry.Value.Length; j++)
                {
                    if (entry.Value.ToUpper()[j] == 'A') tab[0]++;
                    else if (entry.Value.ToUpper()[j] == 'T') tab[1]++;
                    else if (entry.Value.ToUpper()[j] == 'G') tab[2]++;
                    else if (entry.Value.ToUpper()[j] == 'C') tab[3]++;
                    else tab[4]++;
                }
                
                
                float var = (((float)(tab[2]) + (float)tab[3])) / (float)entry.Value.Length;
                sekwencjeidane[i]["gc"] = var.ToString();
                
                sekwencjeidane[i]["kondony"] = (entry.Value.Length / 3).ToString();
                sekwencjeidane[i]["A"] = tab[0].ToString();
                sekwencjeidane[i]["T"] = tab[1].ToString();
                sekwencjeidane[i]["G"] = tab[2].ToString();
                sekwencjeidane[i]["C"] = tab[3].ToString();
                sekwencjeidane[i]["N"] = tab[4].ToString();
                System.Windows.Forms.Label mylab = new System.Windows.Forms.Label();
                mylab.Text = sekwencjeidane[i]["nazwa"];
                mylab.Location = new Point(10, 10+i*20);
                mylab.AutoSize = true;
                this.Controls.Add(mylab);
                mylab.Click += klik;
                i++;
            }
        }

        private void klik(object sender, System.EventArgs e)
        {

            System.Windows.Forms.Label temp = (System.Windows.Forms.Label)sender;
            for (int i = 0; i < sekwencjeidane.Count; i++)
            {
                if (sekwencjeidane[i]["nazwa"] == temp.Text)
                {
                    //Form2 test = new Form2();
                    Form2 test = new Form2(sekwencjeidane[i]);
                    test.Show();
                }
            }
            
            
          
        }



        private void ExportToCSV( string filePath)
        {
            // Tworzenie nagłówka pliku CSV
            string csvContent = "nazwa,wartosc,gc,kondony,A,T,G,C,N" + Environment.NewLine;
            // Dodawanie danych z DataGridView
            foreach (Dictionary<string, string> row in sekwencjeidane)
            {
               
                    csvContent += string.Join(",", Array.ConvertAll(row.ToArray(), c => c.Value)) + Environment.NewLine;
               
               
            }
            // Zapisanie zawartości do pliku CSV
            File.WriteAllText(filePath, csvContent);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            saveFileDialog1.Title = "Wybierz lokalizację zapisu pliku CSV";
            saveFileDialog1.ShowDialog();
            // Jeśli użytkownik wybierze lokalizację i zatwierdzi, zapisz plik CSV
            if (saveFileDialog1.FileName != "")
            {
                // Użyj metody ExportToCSV i podaj obiekt DataGridView oraz ścieżkę do pliku CSV
                ExportToCSV( saveFileDialog1.FileName);
            }
        }

        
    }
}
