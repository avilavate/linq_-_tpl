using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace data_lib_sql
{
    class Program
    {
        private static List<car> cars;

        static void Main(string[] args)
        {

            Task t_cars = Task.Factory.StartNew(() =>
            {
                cars = linq_sql_filters.GetMostFuelEfficientNCars(5);
            });
            t_cars.ContinueWith((a) =>
            {
                cars.ForEach((c) =>
                {
                    Console.WriteLine($"{c.Name,-20} {c.CarLine,-20}: {c.Combined_FE}");
                });

            });

        }
    }
}
