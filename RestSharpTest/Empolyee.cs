using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace RestSharpTest
{
    internal class Employee
    {
        RestClient client;

        public Employee()
        {
            client = new RestClient("http://localhost:3000");
        }
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
        
        public void GetEmpolyee()
        {
            RestRequest request = new RestRequest("/employees", Method.Get);

            RestResponse response = client.Execute(request);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            foreach (Employee employee in dataResponse)
            {
                Console.WriteLine($"Id: {employee.id} Name: {employee.name} salary: {employee.salary}");
            }
     
        }

        public void AddEmpolyee()
        {
            RestRequest request = new RestRequest("/employees", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            JObject jobjectbody = new JObject();
            jobjectbody.Add("name", "sahil");
            jobjectbody.Add("salary", "35000");
            var bodyy = JsonConvert.SerializeObject(jobjectbody);
            request.AddBody(bodyy, "application/json");
            RestResponse response = client.Execute(request);
            var output=response.Content;
            Console.WriteLine(output);
        }
        public void UpdateEmpolyee()
        {
            RestRequest request = new RestRequest("/employees/5", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            JObject jobjectbody = new JObject();
            jobjectbody.Add("name", "Kapil");
            jobjectbody.Add("salary", "50000");
            var bodyy = JsonConvert.SerializeObject(jobjectbody);
            request.AddBody(bodyy, "application/json");
            RestResponse response = client.Execute(request);
            var output = response.Content;
            Console.WriteLine(output);
        }
        public void DeleteEmployeeData()
        {
            RestRequest request = new RestRequest("employees/11", Method.Delete);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
           
    }
}
