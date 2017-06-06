using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_lib_sql
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarsEntity cars = new CarsEntity();
            //cars.SqlCreateCarsFromCsv();
            var cars = linq_sql_filters.GetMostFuelEfficientNCars(5);

            cars.ForEach((c) =>
            {
                Console.WriteLine($"{c.Name,-20} {c.CarLine,-20}: {c.Combined_FE}");
            });


            Console.Read();
        }
    }
}
