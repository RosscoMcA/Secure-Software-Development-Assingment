using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Models
{
    public class FileViewModel
    {

        public string Contents { get; set; }

       

        public string Source { get; set; }

        public string Folder { get; set; }

        public string Name { get; set; }

       
        public int job { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}