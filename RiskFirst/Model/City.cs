using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskFirst.Model
{
    public class City
    {
        public string Name { get; set; }
        public List<string> Addresses { get; set; } = new List<string>();

        public City()
        {

        }

        public City(string name, List<string> addresses)
        {
            Name = name;
            Addresses = addresses;
        }
    }
}
