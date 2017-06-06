using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_lib_sql
{
    class linq_sql_filters
    {
        public static List<car> GetMostFuelEfficientNCars(int n=10)
        {
            var cars = new CarsEntity().Cars;

            var q = (from car in cars
                     orderby car.Combined_FE descending
                     select car).Take(n);

            return q.ToList();
        }
    }
}
