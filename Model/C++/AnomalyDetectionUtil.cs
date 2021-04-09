using OxyPlot;
using SimolatorDesktopApp_1;
using System;
using System.Collections.Generic;

namespace SimolatorDesktopApp_1
{
    public class Line
    {
        public double a, b;
        public Line()
        {
            a = 0;
            b = 0;
        }
        public Line(double newA, double newB)
        {
            a = newA;
            b = newB;

        }
        public double f(double x)
        {
            return a * x + b;
        }

    }
}
static class AnomalyDetectionUtil
    {
        static double Avg(double[] x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        static double Var(double[] x, int size)
        {
            double av = Avg(x, size);
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        static double Cov(double[] x, double[] y, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;

            return sum - Avg(x, size) * Avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        public static double Pearson(double[] x, double[] y, int size)
        {
            double s = Math.Sqrt(Var(x, size)) * Math.Sqrt(Var(y, size));
            if (s == 0) return 0;
            return Cov(x, y, size) / s;
        }

    public static Line LinearReg(double[] x, double[] y, int size)
    {
        double a = Cov(x, y, size) / Var(x, size);
        double b = Avg(y, size) - a * (Avg(x, size));

        return new Line(a, b);
    }
}


