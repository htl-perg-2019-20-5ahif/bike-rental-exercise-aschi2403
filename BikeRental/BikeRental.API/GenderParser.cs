using BikeRental.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.API
{
    public class GenderParser
    {
        public Gender Parse(string gender)
        {
            switch (gender)
            {
                case "Male": return Gender.Male;
                case "Female": return Gender.Female;
                default: return Gender.Unknown;
            }
        }
    }
}
