using Employee_Management.Models;
using System.Collections.Generic;

public interface IEmployeeBL
{
    List<Employee> GetAll();
    Employee GetById(int id);
    void Create(Employee employee);
    void Update(Employee employee);
    void Delete(int id);
}