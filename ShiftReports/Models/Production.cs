using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftReports.Models
{
  
    public class Production
    {

        public int ProductionID { get; set; }
        public int UserID { get; set; }
        public int ShiftID { get; set; }
        public int PlantID { get; set; }
        //public Plant? Plant { get; set; }
        //public Shift? Shift { get; set; }
        /*
         * DO I REALLY NEED THIS?
         * [MaxLength(5),MinLength(1)]
        public int TargetMix { get; set; } */
         [MaxLength(5), MinLength(1)]
        public int ActualMix { get; set; }

        /* public float Efficieny { get { return ((float)ActualMix / (float)Plant.MixRatePerHour); } }
         [MaxLength(5), MinLength(1)] */
        public int CrumbWaste { get; set; }
         [MaxLength(5), MinLength(1)]
        public int Cmp_Waste { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        // magic calculation of efficency
        public string efficency
        {
            get { return (ActualMix / Plant.MixRatePerHour * 100).ToString(String.Format("Value: {0:%%}.")); }
        }

        // super duper totaller function
        public int TotalWaste
        {
            get { return (CrumbWaste + Cmp_Waste); }
        }

        // all the fancy foreign keys
        public virtual User User { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual Plant Plant { get; set; }
    

        
    }
}