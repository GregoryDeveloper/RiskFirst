using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RiskFirst.Model;
using RiskFirst.Helper;
using Microsoft.AspNetCore.Hosting;

namespace RiskFirst.Controllers
{
    [Route("Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        // GET api/values
        [Route("GroupByCity")]
        public IEnumerable<City> Get()
        {
            List<Person> persons = ReadPeopleFile();

            return GetAddressesByCity(persons);
        }

        private List<Person> ReadPeopleFile()
        {
            var jsonString = System.IO.File.ReadAllText(PathHelper.ProjectDirectoryPath + Constants.FILE_PATH);
            return JsonConvert.DeserializeObject<List<Person>>(jsonString);
        }

        private IEnumerable<City> GetAddressesByCity(List<Person> persons)
        {
            var result = persons.GroupBy(
               p => p.City,
               p => p.StreetAddress,
               (c, a) => new { City = c, Addresses = a.ToList() },
               StringComparer.InvariantCultureIgnoreCase);

            foreach (var item in result)
            {
                yield return new City(item.City, item.Addresses);
            }
        }
    }
}
