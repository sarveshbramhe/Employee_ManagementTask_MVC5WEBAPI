using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Management.Models.BAL
{
    public class EmployeeBL : IEmployeeBL
    {

        IEmployeeRepository _employeeRepository;

        public EmployeeBL(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Create(Employee employee)
        {
            _employeeRepository.Create(employee);
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }

        public List<Employee> GetAll()
        {
           return _employeeRepository.GetAll();
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }
    }
}