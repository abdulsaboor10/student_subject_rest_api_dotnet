﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DL.DbModels
{
    public class StudentDbDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RollNumber { get; set; }
        public String PhoneNumber { get; set; }
        public double? GPA { get; set; }

        public ICollection<StudentSubjectDbDto> StudentSubjects { get; set; } = new List<StudentSubjectDbDto>();

    }
}
