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
        public double[,] uA;

        public double xMax;
        public double xMin;

        public double uMax;
        public double uMin;

        public Result(double[] x, double[] t, double[,] u, double[,] uA)
        {
            this.x = x;
            this.t = t;
            this.u = u;
            this.uA = uA;
            xMax = this.x.Max();
            xMin = this.x.Min();
            uMax = GetUMax();
            uMin = GetUMin();
        }

        private double GetUMax()
        {
            double result = -999999999;

            for(int i = 0;i<u.GetLength(0); i++)
            {
                for (int j = 0; j < u.GetLength(0); j++)
                {
                    if (result < u[i,j])
                    {
                        result = u[i, j];
                    }
                }
            }

            return result;
        }

        private double GetUMin()
        {
            double result = 999999999;

            for (int i = 0; i < u.GetLength(0); i++)
            {
                for (int j = 0; j < u.GetLength(0); j++)
                {
                    if (result > u[i, j])
                    {
                        result = u[i, j];
                    }
                }
            }

            return result;
        }
    }
}
