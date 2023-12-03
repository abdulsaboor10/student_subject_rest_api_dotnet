using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DbModels

{
    public class StudentSubjectDbDto
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("StudentDbDto")]
        public int StudentId { get; set;}
        public StudentDbDto StudentDbDto { get; set; }
        public SubjectDbDto SubjectDbDto { get; set; }

        [ForeignKey("SubjectDbDto")]
        public int SubjectId { get; set; }
        public double Marks { get; set; }

    }
}
