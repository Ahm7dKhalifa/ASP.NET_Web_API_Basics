using app1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace app1.Controllers
{
    public class EmployeesController : ApiController
    {
        //Create database object
        EmployeesEntities db = new EmployeesEntities();

        //action for get all employess 
        public IEnumerable<Employee> Get()
        {
            return db.Employees.ToList();
        }

        //action for get only employee from his id
        public Employee Get(int id)
        {
            Employee e = db.Employees.SqlQuery("SELECT * FROM Employees WHERE EmpID = @id", new SqlParameter("id", id)).Single();
            return e;
        }

        
       
        /*########################################################
         *   Post
         * #######################################################*/
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeesEntities entities = new EmployeesEntities())
                {
                    //1.add new employee to database
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                    //2.create responce message
                    //2.1 status code
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    //2.2 location
                    message.Headers.Location = new Uri(Request.RequestUri +
                        employee.EmpID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /*#####################################################
         * delete
         *####################################################*/
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeesEntities entities = new EmployeesEntities())
                {
                    //1.get the employee which user want to delete
                    var entity = entities.Employees.FirstOrDefault(e => e.EmpID == id);
                    //2.if the employee is not found (wrong id) => 404
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    //3.if the employee already exist in the database  => (200 ok)
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /*##########################################################
         * put
         *##########################################################*/
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeesEntities entities = new EmployeesEntities())
                {
                    //1.get the employee from database
                    var entity = entities.Employees.FirstOrDefault(e => e.EmpID == id);
                    //2.if the employee is not found (wrong id) => 404
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with Id " + id.ToString() + " not found to update");
                    }
                    //3.if the employee already exist in the database  => (200 ok)
                    else
                    {
                        entity.EmpID = employee.EmpID;
                        entity.Name = employee.Name;
                        entity.Salary = employee.Salary;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
