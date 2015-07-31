using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalVaccination.Models
{
    public class ChildVaccinInfo
    {

        public int ChildId { set; get; }
        public int ID { set; get; }
        public string VaccinName { set; get; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime LastNotificationSent { get; set; }
        public string Status { get; set; }



    }
}