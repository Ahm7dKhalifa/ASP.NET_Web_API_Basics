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
    }
}
