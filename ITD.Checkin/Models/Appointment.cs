using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITD.Checkin.Models
{
    public class Appointment
    {
        public int id { get; set; }

        [Display(Name = "Test ID")]
        public int test_id { get; set; }

        [Display(Name = "NID")]
        public String nid { get; set; }

        [Display(Name = "ISO")]
        public string iso { get; set; }

        [Display(Name = "First Name")]
        public String first_name { get; set; }

        [Display(Name = "Last Name")]
        public String last_name { get; set; }

        public DateTime created_datetime { get; set; }

        public DateTime scheduled_datetime { get; set; }

        public Nullable<DateTime> accepted_datetime { get; set; }
    }
}