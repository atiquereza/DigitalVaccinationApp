using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSDemo.Models;

namespace AngularJSDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDbContext db = new EmployeeDbContext();

        // GET api/employee
        [ActionName("get"), HttpGet]
        public IEnumerable<Employee> Emps()
        {
            return db.Employees.ToList();
        }

        // GET api/employee/5
        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        // POST api/employee
        public HttpResponseMessage Post(Employee model)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(model);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/employee/5
        public HttpResponseMessage Put(Employee model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/employee/5
        public HttpResponseMessage Delete(int id)
        {
            Employee emp = db.Employees.Find(id);
            if (emp == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            db.Employees.Remove(emp);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, emp);
        }
    }
}
