using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Models
{
    public class ChildInfo
    {
        public int ID { set; get; }
        public int ParentID { set; get; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { set; get; }

        public string BirthCertificateID{ get; set; }
        public virtual UserInfo Parent { get; set; }

 

    }
}