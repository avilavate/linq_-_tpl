using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_lib
{
    class Program
    {
        static void Main(string[] args)
        {

            linq_filters ops = new linq_filters();
            var q = ops.TopNFuelEff();

            foreach (var car in q)
            {
                Console.WriteLine(car.Name + " : " + car.Combined_FE);
            }

            Console.WriteLine(ops.GetTotal());

            Console.Read();
        }
    }
}
