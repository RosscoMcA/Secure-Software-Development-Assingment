using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Models
{
    /// <summary>
    /// Model represents the details of the job transaction table
    /// </summary>
    public class JobTransaction
    {
        public virtual Job Job { get; set; }

        public virtual File File { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime TimeStamp { get; set; }



    }
}