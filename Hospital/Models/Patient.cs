using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 1 - Arrived
        /// 2 - Sick
        /// 3 - Healthy
        /// 
        /// TODO: change it to enum
        /// </summary>
        public Status.StatusEnum Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birthday")]
        public DateTime DayOfBirth { get; set; }

        /// <summary>
        /// Идентификационный код.
        /// </summary>
        [DisplayName("Identity code")]
        public string TaxCode { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}