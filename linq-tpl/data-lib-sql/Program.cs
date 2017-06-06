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
            CarsEntity cars = new CarsEntity();
            cars.SqlCreateCarsFromCsv();
            Console.Read();
        }
    }
}
