using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_lib
{
    public static class csv_parser
    {
        //Model Year,Division,Carline,Eng Displ,# Cyl,City FE,Hwy FE,Comb FE
        public static List<car> ParseCars(string path)
        {
            var lines = File.ReadAllLines(path);
            var q = lines.
                    Skip(1).
                    Where(l => l.Length > 0).
                    Select(l =>
                    {
                        var record = l.Split(',');
                        return new car()
                         {
                             Year = record[0],
                             Name = record[1],
                             CarLine = record[2],
                             Engin_Displacement = record[3],
                             Cly = int.Parse(record[4]),
                             City_FE = int.Parse(record[5]),
                             Highway_FE = int.Parse(record[6]),
                             Combined_FE = int.Parse(record[7])
                         };
                    });
            return q.ToList<car>();
        }
    }
}
