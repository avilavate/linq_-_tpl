using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_lib;

namespace data_lib_sql
{
    class CarsEntity : DbContext
    {
        public CarsEntity()
        {
            // this.Database.Log = Console.WriteLine;
            //  Database.SetInitializer<CarsEntity>(new DropCreateDatabaseAlways<CarsEntity>());
        }
        public DbSet<data_lib_sql.car> Cars { get; set; }

        public void SqlCreateCarsFromCsv()
        {
            var cars = csv_parser.ParseCars();

            var q = from car in cars
                    select new data_lib_sql.car
                    {
                        CarLine = car.CarLine,
                        City_FE = car.City_FE,
                        Cly = car.Cly,
                        Combined_FE = car.Combined_FE,
                        Engin_Displacement = car.Engin_Displacement,
                        Highway_FE = car.Highway_FE,
                        Name = car.Name,
                        Year = car.Year
                    };

            foreach (var car in q)
            {
                this.Cars.Add(car);
            }

            Console.WriteLine($"{this.SaveChanges(),-20} records added");
        }
    }
}
