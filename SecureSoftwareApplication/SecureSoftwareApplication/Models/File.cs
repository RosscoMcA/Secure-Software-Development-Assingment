using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Models
{
    /// <summary>
    /// Model Represents the details of the File Table
    /// </summary>
    public class File
    {
        [Key]
        public int FileID { get; set; }

        public string Contents { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime PubDate { get; set;}

        public string Source { get; set; }

        public string Folder { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime TimeStamp { get; set; }
    
    }
}