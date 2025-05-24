using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace zad109
{
    public partial class Form2 : Form
    {
        string name = "myData";
        List<string> x = new List<string>();
        List<double> y = new List<double>();
        public Form2()
        {
            InitializeComponent();
            string[] charttype = new string[] { "Area", "Bar", "Bubble", "Column", "Doughnut", "Line", "Point", "Funnel",
        "RangeBar", "Spline", "SplineArea", "StepLine" };
           
            for (int i = 0; i < 21; i++)
            {
                x.Add(i.ToString());
                y.Add(Math.Pow((i - 10), 2));
            }
        }
        List<string> labelX = new List<string>();
        public Form2(Dictionary<string, string> temp)
        {
            InitializeComponent();
            string[] charttype = new string[] { "Area", "Bar", "Bubble", "Column", "Doughnut", "Line", "Point", "Funnel",
        "RangeBar", "Spline", "SplineArea", "StepLine" };
         
           
            int i = 0;
            int j = 0;
            name = temp["nazwa"];
            label1.MaximumSize = new Size(400, 0);
            label1.AutoSize = true;
            foreach (var entry in temp)
            {
                label1.Text += entry.Key + " " + entry.Value + "\n";
                if (i > 3)
                {
                    //x.Add(j);
                    x.Add(entry.Key);
                    labelX.Add(entry.Key);
                    y.Add(Int32.Parse(entry.Value));
                    j++;
                }
                i++;
            }
            rys();



        }

        void rys()
        {
            chart1.BackColor = Color.LightBlue;
            chart1.Series.Add(name);
            chart1.Series[name].Color = Color.DarkSlateGray;
            // chart1.Series[name].Label = "Y = #"+Y_Label+"\nX = #"+X_Label;
            // chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            // chart1.Series[name].Label = "Y = #" + Y_Label + "\nX = #" + X_Label;
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Gold;

            chart1.Series[name].Points.DataBindXY(x, y);
            chart1.Series[name].ChartArea = "ChartArea1";
            chart1.Series[name].SetDefault(true);
            chart1.Series[name].Enabled = true;
            chart1.Visible = true;
            chart1.Show();
        }
        private void chart1_Click(object sender, EventArgs e)
        {
            
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
