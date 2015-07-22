using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalVaccination.Models
{
    public class ChildInfo
    {
        public int ID { set; get; }
        public int ParentID { set; get; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { set; get; }

        public string BirthCertificateID{ get; set; }
        public string FatherName { set; get; }
        public string MotherName { set; get; }
 

    }
}