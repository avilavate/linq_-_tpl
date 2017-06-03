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
            var cars = csv_parser.ParseCars(@"./fuel.csv");

            var q = from car in cars
                    where car.Name == "BMW"
                    orderby car.Combined_FE
                    select car;

            var q1 = cars.
                    Where(c => c.Name == "BMW").
                    OrderBy(c => c.Combined_FE).
                    ThenBy(c => c.Name);

            //Best fuel efficient cars in all

            var q2 = (from car in cars
                      select car
                     ).OrderByDescending(c => c.Combined_FE).Take(5);


            foreach (var car in q)
            {
                Console.WriteLine(car.Name + " : " + car.Combined_FE);
            }
            Console.WriteLine("====================================");
            foreach (var car in q1)
            {
                Console.WriteLine(car.Name + " : " + car.Combined_FE);
            }
            Console.WriteLine("====================================");
            foreach (var car in q2)
            {
                Console.WriteLine(car.Name + " : " + car.Combined_FE);
            }
            Console.Read();
        }
    }
}
