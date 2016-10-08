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
        }

        int t = -1;
        Result result;

        private void button1_Click(object sender, EventArgs e)
        {
            t = -1;
            DiffSolver solver = new DiffSolver(DiffSolver.DefaultVx0, DiffSolver.DefaultUx0, DiffSolver.DefaultU0t, DiffSolver.DefaultUlt, DiffSolver.DefaultF);
            result = solver.Solve();
            chart1.ChartAreas[0].Axes[0].Minimum = result.x.Min();
            chart1.ChartAreas[0].Axes[0].Maximum = result.x.Max();
            chart1.ChartAreas[0].Axes[1].Minimum = -1;
            chart1.ChartAreas[0].Axes[1].Maximum = 1;
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
            for(int i = 0; i<result.x.Length; i++)
            {
                chart1.Series[0].Points.AddXY(result.x[i], result.u[i, t]);
            }
            
        }
    }
}
