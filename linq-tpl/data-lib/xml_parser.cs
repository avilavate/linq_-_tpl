using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace data_lib
{
    class xml_parser
    {

        //Model Year,Division,Carline,Eng Displ,# Cyl,City FE,Hwy FE,Comb FE
        public static List<car> CreateCarXML(string path = @"./fuel.csv")
        {
            //To Test TPL
            //  Thread.Sleep(3000);
            var lines = File.ReadAllLines(path);
            var xml = new XDocument();

            XNamespace ns = "https://avil.com/cars";
            XNamespace xs = "https://avil.com/cars/car";
            var Cars = new XElement(ns + "Cars", lines.
                    Skip(1).
                    Where(line => line.Length > 0).
                    Select(line =>
                    {
                        var record = line.Split(',');
                        return new XElement(xs + "Car",
                            new XAttribute("Name", record[1]),
                            new XAttribute("Year", record[0]),
                            new XAttribute("CarLine", record[2]),
                            new XAttribute("Engin_Displacement", record[3]),
                            new XAttribute("Cly", int.Parse(record[4])),
                            new XAttribute("City_FE", int.Parse(record[5])),
                            new XAttribute("Highway_FE", int.Parse(record[6])),
                            new XAttribute("Combined_FE", int.Parse(record[7]))
                            );
                    }));

            xml.Add(Cars);
            xml.Save("cars.xml");

            return null;
        }

        //public static List<manufacturer> ParseManufacturer(string path = @"./manufacturers.csv")
        //{
        //    var lines = File.ReadAllLines(path);

        //    var q = lines.
        //        Where(l => l.Length > 0).
        //        Select((l) =>
        //        {
        //            var record = l.Split(',');
        //            return (new manufacturer()
        //            {
        //                Manufacturer = record[0],
        //                Country = record[1],
        //                Year = int.Parse(record[2])
        //            });
        //        });

        //    return q.ToList();
        //}
    }
}
