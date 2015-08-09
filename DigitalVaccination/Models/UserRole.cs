using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalVaccination.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string ParentRoleName { get; set; }
    }
}