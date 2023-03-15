using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace RestSharpTestCase
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
    }

        public class RestSharptestCase
        {
            RestClient client;
            [SetUp]
            public void Setup()
            {
                client = new RestClient("http://localhost:3000");
            }
            private RestResponse getEmpolyees()
            {
                RestRequest request = new RestRequest("/employees", Method.Get);

                RestResponse response = client.Execute(request);
                return response;
            }

            [Test]
            public void OnCallingGetApi_ReturnEmpolyeeList()
            {
                RestResponse response = getEmpolyees();
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                Assert.AreEqual(5, dataResponse.Count);
            }
        }
    }