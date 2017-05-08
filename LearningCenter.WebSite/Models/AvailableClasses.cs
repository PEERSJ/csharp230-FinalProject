using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.WebSite.Models
{
    public class AvailableClasses
    {
        public UserModel User { get; set; }
        public ClassModel[] Classes { get; set; }
    }
}



