using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string BirthCertificateID { get; set; }
        public virtual ICollection<ChildInfo> Childs { get; set; }

    }
}