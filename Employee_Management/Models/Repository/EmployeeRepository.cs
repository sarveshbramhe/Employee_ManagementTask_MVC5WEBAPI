using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Employee_Management.Models.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        EmployeeDbContext _dbcontext;

        public EmployeeRepository(EmployeeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(Employee employee)
        {
            
            _dbcontext.Employees.Add(employee);
            _dbcontext.SaveChanges();
        }

        public void Delete(int id)
        {
            Employee employee = _dbcontext.Employees.Find(id);

            _dbcontext.Employees.Remove(employee);
            _dbcontext.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = _dbcontext.Employees.ToList();
            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = _dbcontext.Employees.Find(id);
            return employee;
        }

        public void Update(Employee employee)
        {
            _dbcontext.Employees.Attach(employee);
            _dbcontext.Entry(employee).State = EntityState.Modified;
            _dbcontext.SaveChanges();
        }
    }
}