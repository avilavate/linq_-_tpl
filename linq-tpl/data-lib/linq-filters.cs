using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_lib
{
    class linq_filters
    {
        private List<car> _cars;

        public linq_filters()
        {
            this._cars = csv_parser.ParseCars(@"./fuel.csv");
        }

        public static List<car> SearchByName(string name)
        {
            return null;
        }


    }

}
