using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        [JsonIgnore]
        public virtual ICollection<Lecture> Lectures { get; set; }

        public Lesson()
        {
            Lectures = new List<Lecture>();
        }
    }
}