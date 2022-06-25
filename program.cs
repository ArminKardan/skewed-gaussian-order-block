using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{



    internal class Program
    {

        //special thanks to John D. Cook, PhD, President (https://www.johndcook.com/blog/csharp_erf/)
        static double erf(double x)
        {
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }



        static double gaussian_pdf(double x, double mean, double sigma)
        {
            return Math.Exp(-0.5 * Math.Pow((x - mean) / sigma, 2)) / (sigma*2.5);
        }

        static double gaussian_cdf(double x, double mean, double sigma)
        {
            return 0.5 * (1 + erf((x - mean) / (sigma * Math.Sqrt(2))));
        }

        static double skewnormal(double x, double mean, double sigma, double alpha)
        {
            return 2 * gaussian_pdf(x,mean, sigma) * gaussian_cdf(alpha * x, mean, sigma);
        }


        static void Main(string[] args)
        {

            var xes = Enumerable.Range(-5, 10 + 1);
            double sigma = 10;

            //var yes = xes.Select(x => gaussian_cdf(x,0, sigma) - 0.5);
            //var yes = xes.Select(x => Math.Sign(x) * (1 / gaussian_pdf(x, 0, sigma) - sigma * 2.5));
            var yes = xes.Select(x => skewnormal(x, 0, sigma, 2) - skewnormal(0, 0, sigma, 2));

            Console.WriteLine(string.Join("\n", yes));
            Console.ReadLine();
        }
    }
}

//-0.0287989777271358
//- 0.0243546299502605
//- 0.0190251805146296
//- 0.0129795840391448
//- 0.00650865089615105
//0
//0.00610964915156563
//0.0113954778236852
//0.0155049789812776
//0.0182039375811913
//0.0193987298539034
