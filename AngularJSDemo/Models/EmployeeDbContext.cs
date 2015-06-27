using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
            : base()
        {
            Database.SetInitializer<EmployeeDbContext>(new EmployeeDbContextInitializer());
        }

        public DbSet<Employee> Employees { get; set; }
    }

    public class EmployeeDbContextInitializer : DropCreateDatabaseIfModelChanges<EmployeeDbContext>
    {
        protected override void Seed(EmployeeDbContext context)
        {
            var list = new List<Employee>
            {
                new Employee { FirstName = "Rohit", LastName = "Kesharwani", Description = "Rohit Kesharwani", DateofBirth = DateTime.Now.AddYears(-23), Country = "IN", State="UP", Salary = 99999, IsActive = true },
                new Employee { FirstName = "Rahul", LastName = "Singh", Description = "Rahul Singh", DateofBirth = DateTime.Now.AddYears(-25), Country = "IN", State="MP", Salary = 49999.28f, IsActive = true }
            };

            list.ForEach(m =>
            {
                context.Employees.Add(m);
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}