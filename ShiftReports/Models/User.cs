using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftReports.Models
{
    public class User
    {

        public int UserID { get; set; }
        [StringLength(25, MinimumLength=1, ErrorMessage = "Your Last Name is too long, needs to be less than 25 characters!")]
        public string LastName { get; set; }
        [StringLength(25, MinimumLength =1, ErrorMessage = "Your First Name needs to be less than 25 characters!")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}",  ApplyFormatInEditMode = true)]
        public DateTime joined { get; set; }

        public string FullName
        {
            get { return FirstMidName + " " + LastName; }
        }

        public virtual ICollection<Production> ProductionData { get; set; }
        public virtual ICollection<Downtime> DowntimeData { get; set; }
    }
}