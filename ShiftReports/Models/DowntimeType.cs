using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftReports.Models
{
    public class DowntimeType
    {
        public int DowntimeTypeID { get; set; }
        public int PlantID { get; set; }
        public string Name { get; set; }

        public virtual Plant Plant { get; set; }
    }
}