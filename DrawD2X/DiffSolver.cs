using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawD2X
{
    class DiffSolver
    {
        double xA = 0;
        double xB = Math.PI;

        double tA = 0;
        double tB = 4*Math.PI;

        public delegate double Vx0(double x);
        public delegate double Ux0(double x);
        public delegate double U0t(double x);
        public delegate double Ult(double x);
        public delegate double F(double x, double t);

        Vx0 vx0;
        Ux0 ux0;
        U0t u0t;
        Ult ult;
        F f;

        public DiffSolver(Vx0 vx0, Ux0 ux0, U0t u0t, Ult ult, F f)
        {
            this.vx0 = vx0;
            this.ux0 = ux0;
            this.u0t = u0t;
            this.ult = ult;
            this.f = f;
        }

        public Result Solve()
        {
            double h = 0.01 * Math.PI;
            double[] x = new double[(int)Math.Ceiling((xB - xA) / h)+1];
            int Nx = x.Length;
            for (int i = 0; i<x.Length; i++)
            {
                x[i] = xA + i * h;                
            }
            
            double tau = 0.01 * Math.PI;
            double[] t = new double[(int)Math.Ceiling((tB - tA) / tau)+1];
            int Nt = t.Length;
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = tA + i * tau;
            }

            double[,] u = new double[Nx, Nt];
            double[,] f = new double[Nx, Nt];

            //%Заносим 2 начальных условия по t
            double[] v0 = new double[Nx];
            for(int i = 0; i< Nx; i++)
            {
                v0[i] = vx0(x[i]);
            }

            for (int i = 0; i < Nx; i++)
            {
                u[i, 0] = ux0(x[i]);
            }

            //%Заносим 2 граничных условия по x
            for (int i = 0; i < Nt; i++)
            {
                u[0, i] = u0t(t[i]);
            }

            for (int i = 0; i < Nt; i++)
            {
                u[Nx - 1, i] = ult(t[i]);
            }

            //%находим "u" на 2-м
            //%используя левую конечную разность
            //u(:,2)=u(:,1)+tau* v(:,1);
            for (int i = 0; i < Nx; i++)
            {
                u[i, 1] = u[i, 0] + tau*v0[i];
            }

            for(int k = 1; k<Nt-1; k++)
            {
                for (int i = 1; i < Nx-1; i++)
                {
                    u[i, k + 1] = 2 * u[i, k] - u[i, k - 1] + (tau / h) * (tau / h) * (u[i - 1, k] - 2 * u[i, k] + u[i + 1, k]) + tau *tau * f[i, k];
                }
            }

            Result result = new Result(x, t, u);
            return result;
        }

        static public double DefaultVx0(double x)
        {
            return 0;
        }

        static public double DefaultUx0(double x)
        {
            return Math.Sin(x)*x;
        }

        static public double DefaultUlt(double x)
        {
            return 0;
        }

        static public double DefaultU0t(double x)
        {
            return 0;
        }

        static public double DefaultF(double x, double t)
        {
            return 0;
        }
    }
}
