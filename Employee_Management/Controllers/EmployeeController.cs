using Employee_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Employee_Management.Controllers
{
    public class EmployeeController : ApiController
    {
        IEmployeeBL _employeeBL;

        public EmployeeController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        [HttpGet]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetAll()
        {
            try
            {
                var employees = _employeeBL.GetAll();
                if(employees != null && employees.Count > 0)
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [ResponseType (typeof(Employee))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Employee employee = _employeeBL.GetById(id);

                    if (employee != null)
                    {
                        return Ok(employee);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest("Employee Id cannot be negative");
                }
                
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(Employee employee)
        {
            try
            {
                if(employee != null)
                {
                    _employeeBL.Create(employee);
                    return Ok(employee);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(int id ,Employee employee)
        {
            try
            {
                if (id > 0 && employee != null)
                {
                    if (id == employee.Id)
                    {
                        _employeeBL.Update(employee);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    Employee book = _employeeBL.GetById(id);
                    if (book != null)
                    {
                        _employeeBL.Delete(id);
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
