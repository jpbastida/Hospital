using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Specialization { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}