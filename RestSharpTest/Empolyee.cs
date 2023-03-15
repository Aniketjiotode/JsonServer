using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RestSharpTestCase
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

           
    }
}
