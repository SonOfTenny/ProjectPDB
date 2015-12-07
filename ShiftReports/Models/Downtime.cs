using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ShiftReports.Models
{
   
    public class Downtime
    {
        public int DowntimeID { get; set; }
        public int UserID { get; set; }
        public int ShiftID { get; set; }
        public int PlantID { get; set; }
        public int DowntimeTypeID { get; set; }
        public string DowntimeType { get; set; }
        [MaxLength(255)]
        public string Reason { get; set; }
        [MaxLength(255)]
        public string Action { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual Plant Plant { get; set; }
       // public virtual ICollection<DowntimeType> DowntimeType { get; set; }
        //public virtual DowntimeType DowntimeType { get; set; }


    }
}