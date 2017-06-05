using System.Collections.Generic;
using System.Linq;

namespace data_lib
{
    public class linq_filters
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

        public FuelStatistics GetFuelStatistics()
        {
            var q = new FuelStatistics()
            {
                Min = this._cars.Min(c => c.Combined_FE),
                Max = this._cars.Max(c => { return c.Combined_FE; }),
                Avg = this._cars.Average(c => c.Combined_FE)
            };
            return q;
        }

        public List<PerformancebyManufacurer> PerformanceGroupByManufacturer()
        {
            var q = this._cars.
                 GroupBy(c => c.Name)
                 .Select(
                 g =>
                 {
                     return new PerformancebyManufacurer
                     {
                         Manufacturer = g.Key,
                         Avg = g.Average(c => c.Combined_FE),
                         Min = g.Min(c => c.Combined_FE),
                         Max = g.Max(c => c.Combined_FE)

                     };
                 }
                 );

            return q.OrderByDescending(perfByManu=>perfByManu.Max).ToList();
        }
    }

    public class FuelStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public double Avg { get; set; }

        public FuelStatistics()
        {
            this.Max = this.Min = 0;
            this.Avg = 0.0;
        }
    }

    public class PerformancebyManufacurer
    {
        public string Manufacturer { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public double Avg { get; set; }


    }
}
