using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LB6_VA
{
    internal class LB7_FirstEX
    {
        private double EXP;
        private double point;
        private int countNum;
        private double H;
        public LB7_FirstEX(double _EXP, double _point, int _countNum)
        {
            EXP = _EXP;
            point = _point;
            countNum = _countNum;
            H = Math.Pow(10.0, -countNum);
        }
        public double Func(double x) => Math.Pow(EXP, x) - x * x + 1.7;
        public double diffFunc() => Math.Pow(EXP, point) - 2 * point;
        public string[] StrDiff()
        {
            string[] arr = new string[4];
            arr[0] = "f(x)=e^x-x^2+1.7";
            arr[1] = $"x={point}";
            arr[2] = "f'(x)=e^x-2*x";
            arr[3] = $"f'({point})={Math.Round(diffFunc(),countNum)}";
            return arr;
        }
        public double ForwardDiff() => (Func(point + H) - Func(point)) / H;
        public double DownDiff() => (Func(point) - Func(point - H)) / H;
        public double TwoDiff() => (Func(point + H) - Func(point - H)) / (2 * H);
    }
}
