using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace data_lib
{
    public class linq_filters
    {
        private List<car> _cars;
        private List<manufacturer> _manufacturer;
        public linq_filters(string path = @"./fuel.csv")
        {
            this._cars = csv_parser.ParseCars(@"./fuel.csv");
            this._manufacturer = csv_parser.ParseManufacturer();
        }

        public List<car> SearchByName(string name)
        {
            var q = from car in this._cars
                    where car.Name == name
                    orderby car.Combined_FE
                    select car;

            return q.ToList<car>();
        }

        public FuelStatistics Aggregate()
        {
            var result = new FuelStatistics();
            var q= this._cars.Aggregate(new FuelStatistics(), (a, b) =>
            {
                

                result.Avg += b.Combined_FE;
                result.Max = Math.Max(b.Combined_FE, a.Max);
                result.Min = Math.Min(b.Combined_FE, a.Min);

                return result;
            },
            (a) =>
            {
                a.Avg = result.Avg / this._cars.Count();
                a.Max = result.Max;
                a.Min = result.Min;
                return a;
            }
            );
            return q;
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

            return q.OrderByDescending(perfByManu => perfByManu.Max).ToList();
        }

        public List<CarManufacturer> JoinManufacturerCar()
        {
            var q = from car in this._cars
                    join m in this._manufacturer on car.Name equals m.Manufacturer
                    select new CarManufacturer
                    {
                        Manufacturer = m.Manufacturer,
                        Country = m.Country,
                        Name = car.CarLine + "" + car.Engin_Displacement,
                        Economy = car.Combined_FE
                    };

            var q1 = this._manufacturer.Join(this._cars,
                manufacturer => manufacturer.Manufacturer,
                c => c.Name,
                (m, c) => new CarManufacturer
                {
                    Country = m.Country,
                    Manufacturer = m.Manufacturer,
                    Name = c.CarLine + " " + c.Engin_Displacement,
                    Economy = c.Combined_FE
                });
            return q1.ToList();
        }

        public List<car> SearchMostEfficientCarByName(string name,string path = @"cars.xml")
        {
            var Cars = XDocument.Load(path);
            var q = from car in Cars.Element("Cars").Elements("Car")
                    where car.Element("Name").Value == name
                    orderby int.Parse(car.Element("Combined_FE").Value) descending
                    select new car
                    {
                        Name = car.Element("Name").Value,
                        Year = car.Element("Year").Value,
                        CarLine = car.Element("CarLine").Value,
                        Engin_Displacement = car.Element("Engin_Displacement").Value,
                        Cly = int.Parse(car.Element("Cly").Value),
                        City_FE = int.Parse(car.Element("City_FE").Value),
                        Highway_FE = int.Parse(car.Element("Highway_FE").Value),
                        Combined_FE = int.Parse(car.Element("Combined_FE").Value)
                    };

            return q.ToList();
        }
    }

    public class FuelStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public double Avg { get; set; }

        public FuelStatistics()
        {
            this.Max = int.MinValue;
            this.Min = int.MaxValue;
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
