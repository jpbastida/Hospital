using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class PatientDoctorsVM
    {
        public Patient Patient { get; set; }
        public List<Doctor> AllDoctors { get; set; }
    }
}