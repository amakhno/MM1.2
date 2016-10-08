using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawD2X
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            chart1.Series.Add("Новая");
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
        }

        int t = -1;
        Result result;

        private void button1_Click(object sender, EventArgs e)
        {
            t = -1;
            DiffSolver solver = new DiffSolver(DiffSolver.DefaultVx0, DiffSolver.DefaultUx0, DiffSolver.DefaultU0t, DiffSolver.DefaultUlt, DiffSolver.DefaultF, 3*Math.PI, 4*Math.PI, 0, 0);
            result = solver.Solve();
            chart1.ChartAreas[0].Axes[0].Minimum = result.xMin;
            chart1.ChartAreas[0].Axes[0].Maximum = result.xMax;
            chart1.ChartAreas[0].Axes[1].Minimum = result.uMin;
            chart1.ChartAreas[0].Axes[1].Maximum = result.uMax;            
            timer1.Interval = 17;
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t++;
            if(t == result.t.Length)
            {
                timer1.Enabled = false;
                return;
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i<result.x.Length; i++)
            {
                chart1.Series[0].Points.AddXY(result.x[i], result.u[i, t]);
                chart1.Series[1].Points.AddXY(result.x[i], result.uA[i, t]);
            }
            
        }
    }
}
