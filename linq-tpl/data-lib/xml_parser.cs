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
        public void CreateCarXML(string path = @"./fuel.csv")
        {
            //To Test TPL
            //  Thread.Sleep(3000);
            var lines = File.ReadAllLines(path);
            var xml = new XDocument();

            XNamespace ns = "https://avil.com/cars";
            XNamespace xs = "https://avil.com/cars/car";
            var Cars = new XElement("Cars", lines.
                    Skip(1).
                    Where(line => line.Length > 0).
                    Select(line =>
                    {
                        var record = line.Split(',');
                        return new XElement("Car",
                            new XElement("Name", record[1]),
                            new XElement("Year", record[0]),
                            new XElement("CarLine", record[2]),
                            new XElement("Engin_Displacement", record[3]),
                            new XElement("Cly", int.Parse(record[4])),
                            new XElement("City_FE", int.Parse(record[5])),
                            new XElement("Highway_FE", int.Parse(record[6])),
                            new XElement("Combined_FE", int.Parse(record[7]))
                            );
                    }));

            xml.Add(Cars);
            xml.Save("cars.xml");
        }

        public static List<car> ParseCarXML(string path = @"cars.xml")
        {
            var Cars = XDocument.Load(path);
            var q = Cars.Element("Cars")?.
                  Elements("Car")?.
                  OrderByDescending(x=>int.Parse(x.Element("Combined_FE").Value)).
                  Take(10).
                  Select(x =>
                  {
                      return new car()
                      {
                          Name = x.Element("Name").Value,
                          Year = x.Element("Year").Value,
                          CarLine = x.Element("CarLine").Value,
                          Engin_Displacement = x.Element("Engin_Displacement").Value,
                          Cly = int.Parse(x.Element("Cly").Value),
                          City_FE = int.Parse(x.Element("City_FE").Value),
                          Highway_FE = int.Parse(x.Element("Highway_FE").Value),
                          Combined_FE = int.Parse(x.Element("Combined_FE").Value)
                      };
                  });

            return q.ToList();
        }
    }
}
