using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB8_VA
{
    internal class Solution
    {
        private double Down;
        private double Up;
        private double step;
        private const double EXP = 2.71;
        private double[] arrayX;
        private double[] arrayY;
        private int Size;
        public Solution(double _Up,double _Down,double _step,int _Size)
        {
            Down = _Down;
            Up = _Up;
            step = _step;
            Size = _Size;
        }
        private double F(double x, double y) => 3 * y + Math.Pow(EXP, -Math.Pow(x,2));
        public (double[],double[]) Solve_Eylor()
        {
            arrayX = new double[Size+1];
            arrayY = new double[Size+1];
            arrayY[0] = -1;
            arrayX[0] = Down;
            for(int i = 1; i < Size+1; i++)
            {
                arrayX[i] = arrayX[i - 1] + step;
                arrayY[i] = arrayY[i - 1] + step * F(arrayX[i - 1], arrayY[i - 1]);
            }
            return (arrayX, arrayY);
        }
    }
}
