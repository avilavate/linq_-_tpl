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
                //       Console.WriteLine(car.Name + " : " + car.Combined_FE);
            }
            //var q1 = ops.PerformanceGroupByManufacturer();
            //foreach (var perfByM in q1)
            //{
            //    Console.WriteLine($"{perfByM.Manufacturer} : Avg: {perfByM.Avg} Min: {perfByM.Min} Max: {perfByM.Max}" );

            //}

            //var q1 = ops.JoinManufacturerCar();
            //foreach (var j in q1)
            //{
            //    Console.WriteLine($"{j.Manufacturer} from {j.Country} Model {j.Name} Gives {j.Economy} Average");
            //}
            var q0 = ops.SearchByName("BMW");

            var q1 = ops.Aggregate();

            var q2 = ops.GetFuelStatistics();

            // xml_parser.CreateCarXML();
            //var cars = xml_parser.ParseCarXML();
            //var cars1 = ops.TopNFuelEff(10);
            //foreach (var car in cars)
            //{
            //    Console.WriteLine(car.Name + " : " + car.Combined_FE);
            //}
            //Console.WriteLine("-----------------------------------------------");
            //foreach (var car in cars1)
            //{
            //    Console.WriteLine(car.Name + " : " + car.Combined_FE);
            //}

            var cars = ops.SearchMostEfficientCarByName("BMW");
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Name,-10} : {car.CarLine,-30} : {car.Combined_FE,-20}");
            }
            Console.WriteLine("-----------------------------------------------");
            Parallel.ForEach(q0, car =>
            {
                Console.WriteLine($"{car.Name,-10} : {car.CarLine,-30} : {car.Combined_FE,-20}");
            });
            {

            }

            Console.WriteLine($"Max: {q1.Max} Min: {q1.Min} Avg: {q1.Avg}");
            Console.WriteLine($"Max: {q2.Max} Min: {q2.Min} Avg: {q2.Avg}");
            //    Console.WriteLine(ops.GetTotal());

            Console.Read();
        }
    }
}
