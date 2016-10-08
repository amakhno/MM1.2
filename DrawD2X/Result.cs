using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawD2X
{
    class Result
    {
        public double[] x;
        public double[] t;
        public double[,] u;

        public Result(double[] x, double[] t, double[,] u)
        {
            this.x = x;
            this.t = t;
            this.u = u;
        }
    }
}
