using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Models
{
    public class FileViewModel
    {
        [Required]
        public string Contents { get; set; }




        [Required]
        public string Folder { get; set; }

        [Required]
        public string Name { get; set; }

        
        public int job { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}