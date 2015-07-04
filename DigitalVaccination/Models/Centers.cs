using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalVaccination.Models
{
    public class Centers
    {
        public int ID { get; set; }
        public int DistrictsID { get; set; }
        public string DistrictsName { get; set; }
        public int ThanasID { get; set; }
        public string ThanasName { get; set; }
        public string CentersName { get; set; }
        public string CentersAddress { set; get; }


    }
}