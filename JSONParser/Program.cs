using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    class Program
    {

        class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        static void Main(string[] args)
        {

            //serializacja danych
            List<Employee> empList = new List<Employee>();
            empList.Add(new Employee() { Id = 1, Name = "Jan koawlski" });
            empList.Add(new Employee() { Id = 2, Name = "Adam Noawai" });

            string s = JsonConvert.SerializeObject(empList);

            File.WriteAllText(@"c:\tmp\data_zk.json",s);

            empList.Clear();

            s = File.ReadAllText(@"c:\tmp\data_zk.json");
            //Deserializacja
            empList = JsonConvert.DeserializeObject<List<Employee>>(s);



            
            string url = "http://dummy.restapiexample.com/api/v1/employees";
            WebClient webClient = new WebClient();

            string json = webClient.DownloadString(url);

            JObject jsonData = JObject.Parse(json);
            Console.WriteLine($"Status dokumentu = {jsonData["status"]}");

            foreach (JToken item in jsonData["data"])
            {
                int _id = Convert.ToInt32(item["id"]);
                string _name = item["employee_name"].ToString();
                double _salary = Convert.ToDouble(item["employee_salary"]);
                int _age = Convert.ToInt32(item["employee_age"]);
                string _image = item["profile_image"].ToString();
                Console.WriteLine($"{ _id,10}| { _name,-10}| {_salary,-10 }|{ _age, -10}| { _image}" );
            }

            Console.ReadKey();

        }
    }
}
