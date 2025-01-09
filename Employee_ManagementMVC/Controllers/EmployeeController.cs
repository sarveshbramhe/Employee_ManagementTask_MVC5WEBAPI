using Employee_ManagementMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Employee_ManagementMVC.Controllers
{
    
    public class EmployeeController : Controller
    {

        string apiUrl = string.Empty;

        public EmployeeController()
        {
            apiUrl = ConfigurationManager.AppSettings["apiUrl"]?.ToString();
        }

        public ActionResult Home()
        {
            return View();
        }
        // GET: Employee
        //public ActionResult Index()
        //{
        //    List<Employee> employees = new List<Employee>();

        //    // loading all books from api
        //    // api url http://localhost:56327/api/Book

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(apiUrl);

        //    HttpResponseMessage response = client.GetAsync("employee").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string responseContent =
        //            response.Content.ReadAsStringAsync().Result; //json content

        //        //deserialize
        //        employees = JsonConvert.DeserializeObject<List<Employee>>(responseContent);

        //    }
        //    else
        //    {
        //        ViewBag.Message = "Employees not available";
        //    }

        //    return View(employees);
        //}

        public ActionResult Index(string query)
        {
            List<Employee> employees = new List<Employee>();

            // Define your API URL
            string apiUrl = "http://localhost:56327/api/Employee";

            // Create an HttpClient instance to call the API
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);

            // Call the API to get the list of employees
            HttpResponseMessage response = client.GetAsync("employee").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result; // JSON content

                // Deserialize the response into a list of employees
                employees = JsonConvert.DeserializeObject<List<Employee>>(responseContent);

                // Filter employees based on the search query
                if (!string.IsNullOrEmpty(query))
                {
                    query = query.ToLower(); // Make search case-insensitive

                    // Apply filtering on FirstName, LastName, and Age
                    employees = employees.Where(e => e.Firstname.ToLower().Contains(query) ||
                                                      e.Lastname.ToLower().Contains(query) ||
                                                      e.Age.ToString().Contains(query)).ToList();
                }
            }
            else
            {
                ViewBag.Message = "Employees not available";
            }

            // Return the filtered employee list in a partial view
            return View(employees);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {

            //call post api to create employees
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);

                string content = JsonConvert.SerializeObject(employee);

                StringContent json = new StringContent(content,
                    Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("employee", json).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            //get Employee by id
            Employee book = GetById(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);

                string content = JsonConvert.SerializeObject(employee);

                StringContent json =
                    new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    client.PutAsync($"employee/{employee.Id}", json).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(GetById(employee.Id));
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Employee employee = GetById(id);
            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Employee employee = GetById(id);
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Confirmed(int Id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);

            //HttpResponseMessage response =
            //    client.DeleteAsync($"category/{Id}").Result;

            Employee employee = GetById(Id);

            string content = JsonConvert.SerializeObject(employee);

            StringContent json = new StringContent(content,
                Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                client.DeleteAsync($"employee/{Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [NonAction]
        public Employee GetById(int? id)
        {
            Employee employee = new Employee();

            HttpClient client = new HttpClient();
            // client.BaseAddress = new Uri("http://localhost:56327/api/");
            client.BaseAddress = new Uri(apiUrl);

            HttpResponseMessage response =
                client.GetAsync($"employee/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = response.Content.ReadAsStringAsync().Result;

                employee =
                    JsonConvert.DeserializeObject<Employee>(jsonContent);
            }

            return employee;
        }
    }
}