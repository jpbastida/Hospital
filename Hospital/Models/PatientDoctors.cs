using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class PatientDoctors
    {
        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Doctor_Id { get; set; }
    }
}