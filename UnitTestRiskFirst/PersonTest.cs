using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using RiskFirst;
using System.Threading.Tasks;
using RiskFirst.Model;
using System.Collections.Generic;
using System.Net;

namespace UnitTestProject1
{
    [TestClass]
    public class PersonTest
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public async Task GetPerson()
        {
            //expected
            List<City> expected = ExpectedCities();

            // actual
            var response = await _client.GetAsync("/Home/GroupByCity");
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            List<City> actual = JsonConvert.DeserializeObject<List<City>>(jsonResult);

            //assert

            Assert.AreEqual(expected[0].Addresses[0], actual[0].Addresses[0]);
            Assert.AreEqual(expected[0].Addresses[1], actual[0].Addresses[1]);
            Assert.AreEqual(expected[1].Addresses[0], actual[1].Addresses[0]);

        }

        [TestMethod]
        public async Task GetPersonWrongURL()
        {

            var response = await _client.GetAsync("/Home/GroupByCit");
            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

        private List<City> ExpectedCities()
        {

            return new List<City>
            {
                new City
                {
                    Name = "London",
                    Addresses =  new List<string>
                    {
                        "Test St 1",
                        "Test St 2"
                    }
                },
                new City
                {
                    Name = "New York",
                    Addresses =  new List<string>
                    {
                        "Test St 3"
                    }
                }
            };
        }
       
    }
}
