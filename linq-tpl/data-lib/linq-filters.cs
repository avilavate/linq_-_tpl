using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_lib
{
  public  class linq_filters
    {
        private List<car> _cars;

        public linq_filters(string path = @"./fuel.csv")
        {
            this._cars = csv_parser.ParseCars(@"./fuel.csv");
        }

        public List<car> SearchByName(string name)
        {
            var q = from car in this._cars
                    where car.Name == name
                    orderby car.Combined_FE
                    select car;

            return q.ToList<car>();
        }

        public List<car> SearchByFuelEff(string name)
        {
            var q = this._cars.
                    Where(c => c.Name == name).
                    OrderBy(c => c.Combined_FE).
                    ThenBy(c => c.Name);

            return q.ToList<car>();
        }

        public List<car> TopNFuelEff(int n = 5)
        {
            var q = (from car in this._cars
                      select car
                     ).OrderByDescending(c => c.Combined_FE).Take(n);

            return q.ToList<car>();
        }

        public int GetTotal()
        {
            return this._cars.Count();
        }
    }

}
