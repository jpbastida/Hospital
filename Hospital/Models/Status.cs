using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Status
    {
        public enum StatusEnum
        {
            Arrived,
            Sick,
            Healthy
        }
    }
}