using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public abstract class Grade
    {
        public int GradeId { get; set; }
        public MarkEnum Mark { get; set; }        
        public DateTime Date { get; set; }

        public Grade()
        {

        }
    }
}