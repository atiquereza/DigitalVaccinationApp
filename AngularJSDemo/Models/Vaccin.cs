using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSDemo.Models
{
    public class Vaccin
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
        public string Description { get; set; }
    }
}