using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftReports.Models
{
    public class Plant
    {
        
        public int PlantID { get; set; }
        public string Name { get; set; }
        public int MixRatePerHour { get; set; }
       
    }
}