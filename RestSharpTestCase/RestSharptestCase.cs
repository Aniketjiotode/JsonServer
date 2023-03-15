using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Assert.AreEqual(11, dataResponse.Count);
        }
        [Test]
        public void givenempolyee_onpost_shouldreturnaddedempolyee()
        {
            RestRequest request = new RestRequest("/employees", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            JObject jobjectbody = new JObject();
            jobjectbody.Add("name", "adi");
            jobjectbody.Add("salary", "43000");
            var bodyy=JsonConvert.SerializeObject(jobjectbody);
            request.AddBody(bodyy,"application/json");
            RestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("adi", dataresponse.name);
            Assert.AreEqual("43000", dataresponse.salary);
        }
        [Test]
        public void givenempolyee_onUpdate_shouldreturnUpdatedempolyee()
        {
            RestRequest request = new RestRequest("/employees/5", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            JObject jobjectbody = new JObject();
            jobjectbody.Add("name", "Kapil");
            jobjectbody.Add("salary", "41000");
            var bodyy=JsonConvert.SerializeObject(jobjectbody);
            request.AddBody(bodyy,"application/json");
            RestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Kapil", dataresponse.name);
            Assert.AreEqual("41000", dataresponse.salary);
        }
        [Test]
        public void givenempolyee_onDelete_ShouldReturnSuccessStatus()
        {
            RestRequest request = new RestRequest("employees/10", Method.Delete);
            RestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}