using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB6_VA
{
    internal class LB7_SecondEx
    {
        private const double UP = 2.0;
        private const double DOWN = 0.0;
        private double n;
        private double h = (UP - DOWN);
        private int Tochn;
        public LB7_SecondEx(double _n,double tochn) {
            n = _n;
            h /= n;
            Tochn = -1*(int)Math.Log10(tochn);
        }
        public double F(double x) => 1 / Math.Sqrt(10 - Math.Pow(x, 3));
        public double LeftRectang()
        {
            double sum = 0.0;
            for (int i = 0; i < n; i++)
                sum += F(DOWN + i * h);
            return sum * h;
        }
        public double RightRectang()
        {
            double sum = 0.0;
            for (int j = 1; j <= n; j++)
                sum += F(DOWN + j * h);
            return sum * h;
        }
        public double MiddleRectang()
        {
            double sum = 0.0;
            for (int z = 0; z < n; z++)
                sum += F(DOWN + (z + 0.5) * h);
            return sum * h;
        }

        private double MethodTrap()
        {
            double sum = (F(UP) + F(DOWN)) / 2;
            for (int i = 1; i < n; i++)
                sum += F(DOWN + i * h);
            return sum * h;
        }

        private double MethodSimpson()
        {
            double h1 = (UP - DOWN) / (2 * n);
            double sum = F(UP) + F(DOWN);
            double sumOdd = 0.0;
            double sumEven = 0.0;
            for(int i = 1; i < 2 * n; i++)
            {
                if (i % 2 == 1)
                    sumOdd += F(DOWN + i * h1);
                else sumEven += F(DOWN + i * h1);
            }
            return h1 / 3 * (sum+4 * sumOdd + 2 * sumEven);
        }

        public double Method_Runge(double result, double K) => 1 / K * (Math.Pow(result, h / 2) - Math.Pow(result, h));
        
        public double[] ResArr()
        {
            double[] resultArr = new double[5];
            resultArr[0] = Math.Round(LeftRectang(), Tochn);
            resultArr[1] = Math.Round(RightRectang(), Tochn);
            resultArr[2] = Math.Round(MiddleRectang(), Tochn);
            resultArr[3] = Math.Round(MethodTrap(), Tochn);
            resultArr[4] = Math.Round(MethodSimpson(), Tochn);
            return resultArr;
        }

        public string[] ResMethod()
        {
            string[] arr = new string[5];
            arr[0] = Method_Runge(Math.Round(LeftRectang(), 3), Tochn).ToString();
            arr[1] = Method_Runge(Math.Round(MiddleRectang(), 3), Tochn).ToString();
            arr[2] = Method_Runge(Math.Round(RightRectang(), 3), Tochn).ToString();
            arr[3] = Method_Runge(Math.Round(MethodTrap(), 3), Tochn).ToString();
            arr[4] = Method_Runge(Math.Round(MethodSimpson(), 15),Tochn).ToString();
            return arr;
        }
    }
}
