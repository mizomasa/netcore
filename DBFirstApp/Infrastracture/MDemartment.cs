﻿using System;
using System.Collections.Generic;

namespace DBFirstApp.Models
{
    public partial class MDemartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
