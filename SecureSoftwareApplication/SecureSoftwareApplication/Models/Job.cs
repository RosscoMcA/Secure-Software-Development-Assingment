using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Models
{

    /// <summary>
    /// Model represents the details of the Job Table
    /// </summary>
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }

        public JobType Type { get; set; }

        public State state { get; set; }
        [Display(Name ="Authorised state")]
        public bool authorised { get; set; }

        [Display(Name ="Public Access")]
        public bool isPublic { get; set; }

        public bool closed { get; set; }

        public Destination Destination {get;set;}

        public virtual Account Author { get; set; }

        public virtual ICollection<JobTransaction> Files { get; set; }



    }

    public enum JobType { Continous, Once, Periodical}

    public enum State { Sleeping, Paused, Failed, Hanged, Stopped, Active}

    public enum Destination { NY, Tokyo, HongKong, Singapore}


}